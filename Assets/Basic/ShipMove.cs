using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public float maxSpeed = 40f;
    public float rotateSpeed = 60f;
    public float accelerationTime = 20f; 
    public float decelerationTime = 30f; 

    private float currentSpeed = 0f;
    private bool upDelayCompleted = false;
    private bool downDelayCompleted = false;
    private bool isMovingUp = false;
    private bool isMovingDown = false;

    private ObjectSelector objectSelector; // Reference to ObjectSelector script
    private void Start()
    {
        // Assuming both scripts are on the same GameObject, otherwise find the ObjectSelector script
        objectSelector = GetComponent<ObjectSelector>();
    }
    void Update()
    {
        //LimitVerticalPosition();
        if (objectSelector != null)
        {
            Dictionary<GameObject, GameObject> shipRedCircleMap = objectSelector.GetShipRedCircleMap();
            if (shipRedCircleMap.ContainsKey(gameObject))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    HandleUpArrow();
                }
                else if (Input.GetKeyUp(KeyCode.UpArrow) && isMovingUp)
                {
                    StartCoroutine(DecelerateMovement(-Vector3.forward));
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    HandleDownArrow();
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow) && isMovingDown)
                {
                    StartCoroutine(DecelerateMovement(Vector3.forward));
                }

                HandleRotation();
            }
        }
    }

    
    public void HandleUpArrow()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !upDelayCompleted)
        {
            StartCoroutine(DelayedAction(1f, () => upDelayCompleted = true));
        }
        if (upDelayCompleted)
        {
            Accelerate(-Vector3.forward);
        }
        objectSelector.StopSmoothRotateAndMoveCoroutine();
    }

    void HandleDownArrow()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !downDelayCompleted)
        {
            StartCoroutine(DelayedAction(1f, () => downDelayCompleted = true));
        }
        if (downDelayCompleted)
        {
            Accelerate(Vector3.forward);
        }
        objectSelector.StopSmoothRotateAndMoveCoroutine();
    }

    void Accelerate(Vector3 direction)
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += maxSpeed / accelerationTime * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }
        transform.Translate(direction * currentSpeed * Time.deltaTime);
        isMovingUp = isMovingDown = true;
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            objectSelector.StopSmoothRotateAndMoveCoroutine();
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

            // Rotate all child objects
            foreach (Transform child in transform)
            {
                if (child.name != "GameObject")
                {
                    child.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
                }

                // Rotate child's children
                foreach (Transform grandchild in child)
                {
                    grandchild.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
                }
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            objectSelector.StopSmoothRotateAndMoveCoroutine();
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);

            // Rotate all child objects
            foreach (Transform child in transform)
            {
                if (child.name != "GameObject")
                {
                    child.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
                }

                // Rotate child's children
                foreach (Transform grandchild in child)
                {
                    grandchild.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
                }
            }
        }

    }

    IEnumerator DelayedAction(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    IEnumerator DecelerateMovement(Vector3 direction)
    {
        while (currentSpeed > 0f)
        {
            currentSpeed -= maxSpeed / decelerationTime * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0f);
            transform.Translate(direction * currentSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void LimitVerticalPosition()
    {
        if (transform.position.y < -1.7f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y, -1.7f), transform.position.z);
        }
        if (transform.position.y > -0.4f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Min(transform.position.y, -0.4f), transform.position.z);
        }

    }
}
