using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector] public bool isShown;

    [Header("General")]
    [SerializeField] private GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isShown = !isShown;
        }

        pauseMenuUI.SetActive(isShown);
    }
}
