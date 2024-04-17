using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float HealthPoints;
    [SerializeField] private MeshRenderer HitShow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthPoints <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float Damage)
    {
        HealthPoints -= Damage;
        print(HealthPoints);
        StartCoroutine(DamageFlash());
    }


    IEnumerator DamageFlash()
    {
        HitShow.material.color = Color.white;

        yield return new WaitForSeconds(0.15f);

        HitShow.material.color = Color.red;
    }
}
