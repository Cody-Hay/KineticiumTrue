using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float HealthPoints;
    public TMP_Text HealthTracker;
    public Slider HealthSlider;

    void Start()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthPoints <=0f)
        {
            HealthPoints = 0f;
            SceneManager.LoadScene(0);
        }

        //Health Display
        HealthTracker.text = HealthPoints.ToString();
        HealthSlider.value = HealthPoints/100;
    }

    //Trigger the damage
    public void TakeDamage(float Damage)
    {
        HealthPoints -= Damage;
    }
}
