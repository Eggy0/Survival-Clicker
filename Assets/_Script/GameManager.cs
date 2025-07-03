using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Manager manager => Manager.instance;

    public void ReturnToMenu()
    {
        manager.SwitchScene("MainMenu");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Assert(manager != null, "There is no manager!");
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
    }
}
