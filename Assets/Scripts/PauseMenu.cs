using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    public bool isShown = false;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isShown = !isShown;
        }

        pauseMenuUI.SetActive(isShown);
    }
}
