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

    public float FOV;
    [SerializeField] private float FovChangeSpeed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FOV = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        FOVChange(100);
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SenseX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SenseY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        CamHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, yRotation,0);
    }
    public void FOVChange(float dFOV )
    {
        FOV = Mathf.Lerp(Camera.fieldOfView, dFOV, Time.deltaTime * FovChangeSpeed);

        Camera.fieldOfView = FOV;
    }

    public void FOVTilt(float ZTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, ZTilt), 0.25f);
    }
}
