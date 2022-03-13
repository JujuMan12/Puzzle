using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] private AudioSource ButtonSoundEffect;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StartNewGame()
    {
        ButtonSoundEffect.Play();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        ButtonSoundEffect.Play();
        Application.Quit();
    }
}
