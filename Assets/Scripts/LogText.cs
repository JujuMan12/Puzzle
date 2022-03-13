using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour
{
    [HideInInspector] private Text logText;
    [HideInInspector] private float delayTime;
    [HideInInspector] private Color textColor;

    [Header("Fading")]
    [SerializeField] private float fadingDelay = 2f;
    [SerializeField] private float fadingSpeed = 2.5f;

    private void Start()
    {
        logText = GetComponent<Text>();
        textColor = logText.color;
    }

    private void Update()
    {
        if (textColor.a > 0f)
        {
            HandleFading();
        }
    }

    private void HandleFading()
    {
        if (delayTime <= 0f)
        {
            textColor.a = Mathf.Lerp(textColor.a, 0f, fadingSpeed * Time.deltaTime);
        }
        else
        {
            delayTime -= Time.deltaTime;
        }

        logText.color = textColor;
    }

    public void SetText(string text)
    {
        logText.text = text;
        textColor.a = 1f;
        delayTime = fadingDelay;
    }

    public void RemoveDelay()
    {
        delayTime = 0f;
    }
}
