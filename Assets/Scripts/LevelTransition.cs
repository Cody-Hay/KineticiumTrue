using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelTransition : MonoBehaviour
{
    public int SceneNumber;
    public bool EnemiesKilled;
    public int EnemyCount;



    private void Update()
    {
        if(EnemyCount == 0)
        {
            EnemiesKilled = true;
        }
        else
        {
            EnemiesKilled=false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (EnemiesKilled)
        {
           SceneManager.LoadScene(SceneNumber);
        }
        else
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
        
    }

    public void EnemyDown()
    {
        EnemyCount--;
    }
}


