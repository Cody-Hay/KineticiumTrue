using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Look at Player")]

    public Transform Target;
    public float DetectionRange;
    public float RotationSpeed;

    [Header("Shooting")]

    public Transform FirePoint;
    bool canFire;
    [SerializeField] float cooldown;
    [SerializeField] float currentCoolDown;
    [SerializeField] float Damage;
    [SerializeField] private TrailRenderer BulletTrail;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Look Rotation


        if (Vector3.Distance(Target.position, transform.position) < DetectionRange)
        {
            Vector3 TargetDirection = Target.position - transform.position;

            float SingleStep = RotationSpeed * Time.deltaTime;

            Vector3 NewDirection = Vector3.RotateTowards(transform.forward, TargetDirection, SingleStep, 0.0f);
            //NewDirection.y = 0; this will lock the y axis rotation.
            transform.rotation = Quaternion.LookRotation(NewDirection);

            Shooting();
        }
        else
        {
            return;
        }

        if (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
            
            return;
        }
        canFire = true;
        currentCoolDown = cooldown;

       

        //Shooting Raycast

    }

    public void Shooting()
    {
        RaycastHit hit;

       if( Physics.Raycast(FirePoint.position, transform.TransformDirection(Vector3.forward), out hit, DetectionRange))
       {
            Debug.DrawRay(FirePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            

            HealthScript healthScript = hit.transform.GetComponent<HealthScript>();


           if ((hit.transform.tag == "Player") &&(canFire))
           {
                canFire = false;
                print("WAADASHJDHAKFH");
                healthScript.TakeDamage(Damage);
                TrailRenderer trail = Instantiate(BulletTrail,FirePoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit));
           }
       }
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;

        Vector3 StartPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(StartPosition, Hit.point, time);
            time += Time.deltaTime/Trail.time;

            yield return null;
        }
        Trail.transform.position = Hit.point;
        Destroy(Trail.gameObject, Trail.time);
    }
}
