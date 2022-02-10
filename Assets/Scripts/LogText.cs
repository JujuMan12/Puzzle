using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour
{
    private Text logText;

    [SerializeField] private float fadingSpeed = 1f;

    private float opacity = 0f;

    private void Start()
    {
        logText = GetComponent<Text>();
    }

    private void Update()
    {
        Color textColor = logText.color;

        textColor.a = Mathf.Lerp(opacity, 0f, fadingSpeed * Time.deltaTime);
        opacity = textColor.a;

        logText.color = textColor;
    }

    public void SetText(string text)
    {
        logText.text = text;
        opacity = 1f;
    }
}
