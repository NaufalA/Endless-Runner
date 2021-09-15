using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public Button buttonPause;
    public Button buttonResume;
    public Button buttonRestart;
    public Button buttonExit;
    void Start()
    {
        buttonPause.onClick.AddListener(PauseGame);
        buttonResume.onClick.AddListener(ResumeGame);
        buttonRestart.onClick.AddListener(RestartGame);
        buttonExit.onClick.AddListener(ExitToMenu);
        pauseMenuPanel.gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.gameObject.SetActive(true);
        buttonPause.gameObject.SetActive(false);
    }
    
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.gameObject.SetActive(false);
        buttonPause.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitToMenu()
    {
        // TODO: Implement me
        Debug.Log("Method not implemented");
    }
}
