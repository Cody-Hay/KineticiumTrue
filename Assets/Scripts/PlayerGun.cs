using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerGun : MonoBehaviour
{
    [Header("Shooting")]

    public float PlayerDamage;
    public float BaseDamage;
    public float range;
    public float CurrentCooldown;
    public float WeaponCooldown;

    public Transform FirePoint;
    public TrailRenderer BulletTrail;

    [Header("Camera")]

    public Camera cam;

    [Header("References")]

    public PlayerMovement playerMovement;

    [Header("Gun Swapping")]

    public bool HasBasicWeapon;
    public bool HasSMG;
    public bool HasSniper;
    public bool HasSword;
 

    // Start is called before the first frame update
    void Start()
    {
        CurrentCooldown = WeaponCooldown;
        playerMovement =this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        PlayerDamage = (BaseDamage * playerMovement.DamageMultiplier);
        CurrentCooldown = CurrentCooldown + Time.deltaTime;
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
                     enemyHealth.TakeDamage(PlayerDamage);
                     TrailRenderer trail = Instantiate(BulletTrail, FirePoint.transform.position, Quaternion.identity);
                     StartCoroutine(SpawnTrail(trail, PlayerHit));
                     print("PLAYA");
                }

                else
                {
                    CurrentCooldown = 0;
                    TrailRenderer trail = Instantiate(BulletTrail, FirePoint.transform.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, PlayerHit));
                }
                
                
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
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = Hit.point;
        Destroy(Trail.gameObject, Trail.time);
    }
}

