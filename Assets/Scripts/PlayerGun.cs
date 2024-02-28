using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerGun : MonoBehaviour
{
    [Header("Shooting")]

    public Transform FirePoint;
    bool canFire;
    [SerializeField] float cooldown;
    [SerializeField] float currentCoolDown;
    [SerializeField] float Damage;
    [SerializeField] float Range;
    [SerializeField] private TrailRenderer BulletTrail;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("WAFFLES");
        }
    }
}

