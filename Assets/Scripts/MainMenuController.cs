using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject verticalUI;

    [SerializeField]
    private GameObject horizontalUI;

    void Update()
    {
        if (Screen.orientation != ScreenOrientation.AutoRotation)
        {
            //Debug.Log("Device orientation: " + Screen.orientation);

            switch (Screen.orientation)
            {
                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
                    this.verticalUI.SetActive(true);
                    this.horizontalUI.SetActive(false);
                    break;

                case ScreenOrientation.LandscapeRight:
                case ScreenOrientation.LandscapeLeft:
                    this.verticalUI.SetActive(false);
                    this.horizontalUI.SetActive(true);
                    break;
            }
        }
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
