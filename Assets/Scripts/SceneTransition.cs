using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [HideInInspector] private Color targetColor = Color.clear;
    [HideInInspector] private int? newScene;

    [Header("Fading")]
    [SerializeField] private Image fadingScreen;
    [SerializeField] private float fadingSpeed = 10f;

    private void Update()
    {
        fadingScreen.color = Color.Lerp(fadingScreen.color, targetColor, fadingSpeed * Time.deltaTime);

        if (fadingScreen.color == targetColor)
        {
            if (newScene != null)
            {
                SceneManager.LoadScene((int)newScene);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void FadeToScene(int scene)
    {
        gameObject.SetActive(true);
        targetColor = Color.black;
        newScene = scene;
    }
}
