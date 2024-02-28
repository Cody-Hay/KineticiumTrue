using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float HealthPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthPoints <= 0f)
        {
            HealthPoints = 0f;
            Destroy(gameObject,2);
        }
    }

    public void TakeDamage(float Damage)
    {
        HealthPoints -= Damage;
    }
}
