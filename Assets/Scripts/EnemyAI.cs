using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Look at Player")]

    public Transform Target;
    public float DetectionRange;
    public float RotationSpeed;

    [Header("Pathfinding")]

    public Vector3 Playerrange;
    public NavMeshAgent EnemyAgent;
    public float Chaserange;

    [Header("Shooting")]

    public Transform FirePoint;
    bool canFire;
    [SerializeField] float cooldown;
    [SerializeField] float currentCoolDown;
    [SerializeField] float Damage;
    [SerializeField] private TrailRenderer BulletTrail;

    [Header("Sounds")]
    public AudioSource EnemySource;
    public AudioClip EGunshot;
    public AudioClip Alert;

    private bool Alerted;
    public bool StopRepeat;


    // Start is called before the first frame update
    void Start()
    {
        EnemyAgent = GetComponent<NavMeshAgent>();
        EnemySource = GetComponent<AudioSource>();
        StopRepeat= false;
    }

    // Update is called once per frame
    void Update()
    {
        //Shooting check and looking towards player
        if (Vector3.Distance(Target.position, transform.position) < DetectionRange)
        {
            Vector3 TargetDirection = Target.position - transform.position;

            float SingleStep = RotationSpeed * Time.deltaTime;

            Vector3 NewDirection = Vector3.RotateTowards(transform.forward, TargetDirection, SingleStep, 0.0f);
            //NewDirection.y = 0; this will lock the y axis rotation.
            transform.rotation = Quaternion.LookRotation(NewDirection);
            Shooting();
        }

        //Chase player check
        if(Vector3.Distance(Target.position, transform.position) < Chaserange)
        {
            EnemyAgent.SetDestination(Target.position);
            Alerted = true;
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
        if(Alerted&!StopRepeat)
        {
            EnemySource.PlayOneShot(Alert);
            StopRepeat = true;
        }
        else
        {
            return;
        }
    }

    //Shoot Method
    public void Shooting()
    {
        RaycastHit hit;

       if( Physics.Raycast(FirePoint.position, transform.TransformDirection(Vector3.forward), out hit, DetectionRange))
       {
            Debug.DrawRay(FirePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            

            HealthScript healthScript = hit.transform.GetComponent<HealthScript>();
            PlayerMovement Playermove = hit.transform.GetComponent<PlayerMovement>();


           if ((hit.transform.tag == "Player") &&(canFire))
           {
                canFire = false;
                healthScript.TakeDamage(Mathf.Round(Damage/Playermove.DamageReduction));
                TrailRenderer trail = Instantiate(BulletTrail,FirePoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit));

                EnemySource.PlayOneShot(EGunshot);
           }
       }
    }

    //Trail Rendering coroutine
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
