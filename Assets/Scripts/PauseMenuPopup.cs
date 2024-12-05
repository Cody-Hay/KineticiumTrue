using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuPopup : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool IsPaused;
    void Start()
    {
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Unpause();
            }
            else
            {
                Paused();
            }
        }

    }

    public void Paused()
    {
        IsPaused= true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        IsPaused= false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
