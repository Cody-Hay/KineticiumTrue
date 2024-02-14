using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform cameraLocation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraLocation.position;
    }
}
