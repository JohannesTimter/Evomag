using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public static PanelController instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public GameObject EvolutionPanel;
    public GameObject EvolutionPanel2;
    public GameObject TutorialPanel;
    public GameObject DeathPanel;


    private void Start()
    {
        Time.timeScale = 0.0f;
        EvolutionPanel.transform.gameObject.SetActive(false);
        EvolutionPanel2.transform.gameObject.SetActive(false);
        TutorialPanel.transform.gameObject.SetActive(true);
    }

    public void OpenDeathPanel()
    {
        DeathPanel.transform.gameObject.SetActive(true);
    }

    public void restartGame()
    {
        DeathPanel.transform.gameObject.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void closeTutorialPanel()
    {
        TutorialPanel.transform.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OpenEvoPanel()
    {
        EvolutionPanel.transform.gameObject.SetActive(true);
        EvolutionPanel2.transform.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseEvoPanel()
    {
        EvolutionPanel.transform.gameObject.SetActive(false);
        EvolutionPanel2.transform.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

}
