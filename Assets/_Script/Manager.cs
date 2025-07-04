using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Text volumeText;
    public float globalVolume = 0.5f;
    public AudioSource audioSource;

    public static Manager instance;
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeVolume()
    {
        globalVolume = volumeSlider.value;
        ApplyVolume();
    }

    public void ApplyVolume()
    {
        volumeText.text = $"Volume: {Mathf.Floor(globalVolume*100)}%";
        audioSource.volume = globalVolume;
    }

    public void ActivePanel(GameObject panel)
    {
        panel.SetActive(true);
    } 
    public void DisablePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    private void Awake()
    {
        Manager duplicate = (Manager)FindFirstObjectByType(typeof(Manager));
        if (duplicate != null && instance != null)
        {
            Debug.Log($"Found dupe: {duplicate} (original: {instance}");
            instance = duplicate;
            Destroy(duplicate);
        }
        else if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
        volumeSlider.value = globalVolume;
        ApplyVolume();
    }
}
