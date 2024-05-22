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

    public GameObject WarningText;
    public GameObject BlockingWall;

    private void Start()
    {
        WarningText.SetActive(false);
        BlockingWall.SetActive(true);
    }

    private void Update()
    {
        if(EnemyCount == 0)
        {
            EnemiesKilled = true;
            BlockingWall.SetActive(false);
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
            print("EVERYBODY MUST DIE");
             StartCoroutine(WarningFlash());
        }
        
    }

    public void EnemyDown()
    {
        EnemyCount--;
    }

    IEnumerator WarningFlash()
    {
        WarningText.SetActive(true);
        yield return new WaitForSeconds(3);
        WarningText.SetActive(false);
    }
}


