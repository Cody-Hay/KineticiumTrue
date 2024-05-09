using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class CameraControls : MonoBehaviour
{
    public float SenseX;
    public float SenseY;

    public Transform Orientation;
    public Transform CamHolder;
    public Camera Camera;

    float xRotation;
    float yRotation;

    public float HighFOV;
    public float LowFOV;
    public float PeakFOV;
    [SerializeField] private float FovChangeSpeed;

    public bool Highspeed;
    public bool PeakSpeed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SenseX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SenseY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        if (Highspeed&!PeakSpeed)
        {
            Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, HighFOV, FovChangeSpeed);
        }

        if (PeakSpeed & !Highspeed)
        {
            Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, PeakFOV, FovChangeSpeed);
        }

        if(!Highspeed&!PeakSpeed)
        {
            Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, LowFOV, FovChangeSpeed);
        }

    }

    public void FOVTilt(float ZTilt)
    {
        transform.DOLocalRotate(new Vector3(0, yRotation, ZTilt), 0.25f);
    }
}
