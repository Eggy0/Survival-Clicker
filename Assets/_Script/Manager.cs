using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public static Manager instance;
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    } 


    public void ExitApp()
    {
        Application.Quit();
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SwitchScene("MainMenu");
    }
}
