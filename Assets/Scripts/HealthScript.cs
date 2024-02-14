using UnityEngine;
using TMPro;

public class HealthScript : MonoBehaviour
{
    public float HealthPoints;
    public TMP_Text HealthTracker;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HealthPoints <=0f)
        {
            HealthPoints = 0f;
        }

        //Health Display
        HealthTracker.text = HealthPoints.ToString();
    }

    //Trigger the damage
    public void TakeDamage(float Damage)
    {
        HealthPoints -= Damage;
    }
}
