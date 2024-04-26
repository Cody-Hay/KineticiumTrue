using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class PlayerGun : MonoBehaviour
{
    [Header("Shooting")]

    public float PlayerDamage;
    public float BaseDamage;
    public float range;
    public float FalloffRange;
    public float CurrentCooldown;
    public float WeaponCooldown;

    public Transform FirePoint;
    public TrailRenderer BulletTrail;
    [SerializeField] private GameObject BulletHoleDecal;

    [Header("Camera")]

    public Camera cam;
    public TMP_Text WeaponDisplay;

    [Header("References")]

    public PlayerMovement playerMovement;
    [SerializeField] private MeshRenderer GunMesh;

    [Header("Gun Swapping")]

    public bool HasBasicWeapon;
    public bool HasSMG;
    public bool HasSniper;

    // Start is called before the first frame update
    void Start()
    {
        HasBasicWeapon = true;
        CurrentCooldown = WeaponCooldown;
        playerMovement =this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        var WholeVelocity = Mathf.Round(playerMovement.CurrentVelocity);
        
        
        //Inputs
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        PlayerDamage = (BaseDamage * playerMovement.DamageMultiplier);
        CurrentCooldown = CurrentCooldown + Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (WholeVelocity<=0)
            {
               HasBasicWeapon = true;
               HasSMG = false;
               HasSniper = false;
               GunMesh.material.color = Color.black;
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
           if(WholeVelocity <=0)
           {
              HasSMG = true;
              HasSniper = false;
              HasBasicWeapon = false;
              GunMesh.material.color = Color.yellow;

           }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(WholeVelocity <=0)
            {
                HasSniper = true;
                HasSMG = false;
                HasBasicWeapon = false;
                GunMesh.material.color = Color.green;
            }
        }
        //Weapon Bools-Same object, different stats
        if(HasSMG)
        {
            WeaponDisplay.text = "SMG";
            WeaponCooldown = 0.2f;
            BaseDamage = 3;
            FalloffRange = 20;
        }

        if(HasBasicWeapon)
        {
            WeaponDisplay.text = "Basic";
            WeaponCooldown = 0.75f;
            BaseDamage = 7;
            FalloffRange = 40;
        }

        if (HasSniper)
        {
            WeaponDisplay.text = "Sniper";
            WeaponCooldown = 2.5f;
            BaseDamage = 25;
            FalloffRange = 120;
        }
    }

    //Shoot function
    public void Shoot()
    {
        RaycastHit PlayerHit;

        if(  Physics.Raycast(cam.transform.position,cam.transform.forward, out PlayerHit, range))
        {
            EnemyHealth enemyHealth = PlayerHit.transform.GetComponent<EnemyHealth>();
            Debug.DrawRay(cam.transform.position, transform.TransformDirection(Vector3.forward) * PlayerHit.distance, Color.yellow);

            if (CurrentCooldown > WeaponCooldown)
            {
                if (PlayerHit.transform.tag == "Enemy")
                {
                    CurrentCooldown = 0;
                    TrailRenderer trail = Instantiate(BulletTrail, FirePoint.transform.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, PlayerHit));

                    if(Vector3.Distance(transform.position, PlayerHit.transform.position)>FalloffRange)
                    {
                         enemyHealth.TakeDamage(PlayerDamage/5);
                    }
                    else
                    {
                        enemyHealth.TakeDamage(PlayerDamage);
                    }
                }

                else
                {
                    CurrentCooldown = 0;
                    TrailRenderer trail = Instantiate(BulletTrail, FirePoint.transform.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, PlayerHit));
                    GameObject obj = Instantiate(BulletHoleDecal, PlayerHit.point, Quaternion.identity, PlayerHit.transform);
                    obj.transform.rotation = Quaternion.LookRotation(PlayerHit.normal);
                }
            }
        }
    }

    //Rendering code for the bullet trails
    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;

        Vector3 StartPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(StartPosition, Hit.point, time);
            time += Trail.time;

            yield return null;
        }
        Trail.transform.position = Hit.point;
        Destroy(Trail.gameObject, Trail.time);
    }
}

