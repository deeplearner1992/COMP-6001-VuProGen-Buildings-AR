using TMPro;
using UnityEngine;

public class SliderNumbers : MonoBehaviour
{
    [SerializeField]
    private int lengthOfNumbers;

    [SerializeField]
    private float maxValue = 20;

    [SerializeField]
    private float minValue = 1;

    [SerializeField]
    private float step = 3;

    [SerializeField]
    private float scale = 32;

    [SerializeField]
    private float numberMultipler = 1;

    public GameObject[] numbers;

    private void Start()
    {
        float tempNumber = minValue;
        lengthOfNumbers = 1;

        if ((maxValue - minValue) % step != 0)
        {
            tempNumber += (maxValue - minValue) % step;
            lengthOfNumbers++;
        }

        while (tempNumber < maxValue)
        {
            tempNumber += step;
            lengthOfNumbers++;
        }

        for (int i = 0; i < lengthOfNumbers; i++)
        {
            GameObject temp = new GameObject($"Number-{i}");
            temp.transform.parent = transform;
            temp.AddComponent<TextMeshProUGUI>();
            temp.GetComponent<TextMeshProUGUI>().fontSize = 12;
            temp.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            numbers[i] = temp;
        }

        numbers[0].GetComponent<TextMeshProUGUI>().text = $"{minValue * numberMultipler}";
        numbers[0].transform.localPosition = new Vector3(0, 0, 0);

        numbers[lengthOfNumbers - 1].GetComponent<TextMeshProUGUI>().text = $"{maxValue * numberMultipler}";
        numbers[lengthOfNumbers - 1].transform.localPosition = new Vector3(100, 0, 0);

        float currentValue = minValue;
        float percentage;

        for (int i = 1; i < lengthOfNumbers - 1; i++)
        {
            if (i == 1 && (maxValue - minValue) % step != 0) 
            {
                currentValue = currentValue + (maxValue - minValue) % step;
                percentage = (currentValue - minValue) / (maxValue - minValue) * 100;

                numbers[i].GetComponent<TextMeshProUGUI>().text = $"{currentValue * numberMultipler}";
                numbers[i].transform.localPosition = new Vector3(percentage, 0, 0);
            } 
            else
            {
                currentValue = currentValue + step;
                percentage = (currentValue - minValue) / (maxValue - minValue) * 100;

                numbers[i].GetComponent<TextMeshProUGUI>().text = $"{currentValue * numberMultipler}";
                numbers[i].transform.localPosition = new Vector3(percentage, 0, 0);
            }
        }

        transform.localScale = new Vector3(scale, scale, scale);
    }
}