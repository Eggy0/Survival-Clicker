using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    public static AudioSourceManager audioInstance;
    public float globalVolume = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (audioInstance)
        {
            Destroy(gameObject);
        }
        else
        {
            audioInstance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}
