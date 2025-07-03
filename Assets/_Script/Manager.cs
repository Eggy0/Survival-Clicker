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
    public static AudioSource audioSourceInstance;

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
        
        if (instance == null)
        {
            instance = this;
        }

        if (audioSourceInstance == null)
        {
            audioSourceInstance = audioSource;
        }

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(audioSourceInstance);
        volumeSlider.value = globalVolume;
        ApplyVolume();
    }
}
