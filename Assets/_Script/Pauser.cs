using UnityEngine;

public class Pauser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void OnEnable()
    {
        Time.timeScale = 0f;
    }
}
