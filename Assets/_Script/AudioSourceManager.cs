using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    public static AudioSourceManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        AudioSourceManager duplicate = (AudioSourceManager)FindFirstObjectByType(typeof(AudioSourceManager));
        if (duplicate != null && instance != null)
        {
            Debug.Log($"Found dupe: {duplicate} (original: {instance})");
            instance = duplicate;
            Destroy(duplicate);
        }
        if (instance == null && duplicate == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }
}
