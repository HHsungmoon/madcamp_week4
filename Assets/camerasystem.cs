using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerasystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();
        ZoomCamera();
        TiltCamera();
    }

    void MoveCamera()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

    if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
    if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
    if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
    if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

    // Disregard the vertical (Y) component when moving
    Vector3 cameraForward = transform.forward;
    cameraForward.y = 0;

    Vector3 cameraRight = transform.right;
    cameraRight.y = 0;

    Vector3 moveDir = cameraForward.normalized * inputDir.z + cameraRight.normalized * inputDir.x;
    float moveSpeed = 100f;
    transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void RotateCamera()
    {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = -1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = 1f;

        float rotateSpeed = 60f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }

    void ZoomCamera()
    {
        float zoomSpeed = 200f;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoomAmount = scroll * zoomSpeed * transform.forward * Time.deltaTime;

        transform.Translate(zoomAmount, Space.World);
    }

    void TiltCamera()
    {
        float rotateSpeed = 60f;

        float tiltAngle = transform.eulerAngles.x;

        // Set the minimum and maximum tilt angles (adjust as needed)
        float minTiltAngle = 10f; // Minimum tilt angle in degrees
        float maxTiltAngle = 80f; // Maximum tilt angle in degrees

        if (Input.GetKey(KeyCode.C))
        {
            tiltAngle = Mathf.Clamp(tiltAngle + rotateSpeed * Time.deltaTime, minTiltAngle, maxTiltAngle);
            transform.rotation = Quaternion.Euler(tiltAngle, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        if (Input.GetKey(KeyCode.V))
        {
            tiltAngle = Mathf.Clamp(tiltAngle - rotateSpeed * Time.deltaTime, minTiltAngle, maxTiltAngle);
            transform.rotation = Quaternion.Euler(tiltAngle, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
