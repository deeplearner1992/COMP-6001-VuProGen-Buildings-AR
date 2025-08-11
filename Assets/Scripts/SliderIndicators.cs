using UnityEngine;
using UnityEngine.UI;

public class SliderIndicators : MonoBehaviour
{
    [SerializeField]
    private Texture texture;

    [SerializeField]
    private int lengthOfNumbers;

    [SerializeField]
    private float maxValue = 20;

    [SerializeField]
    private float minValue = 1;

    [SerializeField]
    private float scaleX = 4.3f;

    [SerializeField]
    private float scaleY = 1.5f;

    [SerializeField]
    private float scaleZ = 3f;

    [SerializeField]
    private float colorX = 92f;

    [SerializeField]
    private float colorY = 113f;

    [SerializeField]
    private float colorZ = 214f;

    [SerializeField]
    private float colorT = 255f;

    [SerializeField]
    private float imageWidth = 40f;

    public GameObject[] numbers;

    private void Awake()
    {
        Clear();
        if (numbers.Length > 0) Render();
    }

    private void Render()
    {
        transform.localScale = new Vector3(1, 1, 1);
        lengthOfNumbers = (int)(maxValue + 1);

        this.numbers = new GameObject[lengthOfNumbers];

        for (int i = 0; i < lengthOfNumbers; i++)
        {
            GameObject temp = new GameObject($"Indicator-{i}");
            temp.transform.parent = transform;
            temp.AddComponent<RectTransform>();
            temp.AddComponent<CanvasRenderer>();
            temp.AddComponent<RawImage>();
            numbers[i] = temp;
        }

        float currentValue = minValue;
        float percentage;

        for (int i = 0; i < lengthOfNumbers; i++)
        {
            currentValue = i;
            percentage = currentValue / maxValue * 100;
            numbers[i].GetComponent<RawImage>().texture = this.texture;
            numbers[i].GetComponent<RawImage>().color = new Color(colorX / 256, colorY / 256, colorZ / 256, colorT / 256);
            numbers[i].GetComponent<RectTransform>().sizeDelta = new Vector2(imageWidth, numbers[i].GetComponent<RectTransform>().sizeDelta.y);
            numbers[i].transform.localPosition = new Vector3(percentage * scaleX, 0, 0);
        }

        transform.localScale = new Vector3(1, scaleY, scaleZ);
    }

    public void ReRender(float maxValue)
    {
        this.maxValue = maxValue;
        Clear();
        Render();
    }

    private void Clear()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}