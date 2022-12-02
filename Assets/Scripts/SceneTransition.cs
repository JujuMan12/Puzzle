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
    [SerializeField] private float fadingSpeed = 1f;
    [SerializeField] private float fadingOffset = 0.01f;

    private void Start()
    {
        fadingScreen.color = Color.black;
    }

    private void Update()
    {
        fadingScreen.color = Color.Lerp(fadingScreen.color, targetColor, fadingSpeed * Time.deltaTime);

        if (Mathf.Abs(fadingScreen.color.a - targetColor.a) <= fadingOffset)
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
        fadingSpeed = 2f;
    }
}
