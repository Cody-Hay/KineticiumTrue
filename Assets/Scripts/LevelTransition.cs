using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public int SceneNumber;

    private void OnTriggerEnter(Collider other)
    {
        print("SASAMI");
        SceneManager.LoadScene(SceneNumber);
    }
}
