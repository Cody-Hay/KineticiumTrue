using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalDestroy : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject,5);
    }
}
