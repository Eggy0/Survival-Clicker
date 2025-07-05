using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static Manager manager => Manager.instance;
    public static AudioSourceManager audioSource => AudioSourceManager.audioInstance;

    public Slider volumeSlider;
    public TMP_Text volumeText;

    public GameObject mainMenu;
    public GameObject options;
    public void ChangeVolume()
    {
        audioSource.globalVolume = volumeSlider.value;
        ApplyVolume();
    }

    public void ApplyVolume()
    {
        volumeText.text = $"Volume: {Mathf.Floor(audioSource.globalVolume * 100)}%";
        audioSource.GetComponent<AudioSource>().volume = audioSource.globalVolume;
    }

    public void PlayGame()
    {
        manager.SwitchScene("Gameplay");
    }
    public void ToggleOptions()
    {
        manager.TogglePanel(mainMenu);
        manager.TogglePanel(options);
    }
    public void Exit()
    {
        manager.ExitApp();
    }

    private void Awake()
    {
        volumeSlider.value = audioSource.globalVolume;
        ApplyVolume();
    }
}
