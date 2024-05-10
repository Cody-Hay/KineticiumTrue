using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelTransition : MonoBehaviour
{
    public int SceneNumber;

    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneNumber);
    }


    public void OnLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void OnLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
}


