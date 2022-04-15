using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIController uiController;
    [SerializeField] private SceneTransition sceneTransition;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource ButtonSoundEffect;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; //TODO
    }

    public void ResumeGame()
    {
        uiController.ChangePauseMenuState(false);
    }

    public void StartNewGame()
    {
        ButtonSoundEffect.Play();
        sceneTransition.FadeToScene(1);
    }

    public void ReturnToMenu()
    {
        ButtonSoundEffect.Play();
        sceneTransition.FadeToScene(0);
    }

    public void QuitGame()
    {
        ButtonSoundEffect.Play();
        Application.Quit();
    }
}
