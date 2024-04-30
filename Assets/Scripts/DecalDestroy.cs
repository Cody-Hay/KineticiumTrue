using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalDestroy : MonoBehaviour
{
    //Delayed destruction for decals
    private void Awake()
    {
        Destroy(gameObject,5);
    }
}
