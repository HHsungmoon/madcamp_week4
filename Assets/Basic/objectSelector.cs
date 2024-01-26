using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public GameObject redCirclePrefab;
    public float rotationSpeed = 300.0f; // Adjust as needed (degrees per second)
    public float moveSpeed = 50.0f;
    public bool isMoving = false;
    private Dictionary<GameObject, GameObject> shipRedCircleMap = new Dictionary<GameObject, GameObject>();
    private Coroutine smoothRotateAndMoveCoroutine; // Variable to track the active coroutine

    private static List<ObjectSelector> allSelectors = new List<ObjectSelector>();

    private void Awake()
    {
        allSelectors.Add(this);
    }

    private void OnDestroy()
    {
        allSelectors.Remove(this);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HandleCharacterClick();
        }

        UpdateRedCircleTransforms(); // Update the positions and rotations in every frame
    }

    private void HandleCharacterClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Debug.Log(gameObject.name);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("JS_ship"))
            {
                GameObject clickedShip = hit.collider.gameObject;

                Debug.Log("Clicked Ship: " + clickedShip.name);

                // Destroy red circles for all previous ships
                foreach (var ship in shipRedCircleMap.Keys)
                {
                    if (ship != clickedShip)
                    {
                        Debug.Log("Destroying Red Circle for previous ship: " + ship.name);
                        Destroy(shipRedCircleMap[ship]);
                        shipRedCircleMap.Remove(ship); // Remove from the dictionary
                        break;
                    }
                }

                // Check if the ship is already selected
                if (shipRedCircleMap.ContainsKey(clickedShip))
                {
                    Debug.Log("Same ship clicked again: " + clickedShip.name);
                    // You can add additional logic here if needed
                    return; // Do nothing if the same ship is clicked again
                }

                // Instantiate a new red circle for the clicked ship
                GameObject redCircle = Instantiate(redCirclePrefab);
                redCircle.transform.position = clickedShip.transform.position;
                redCircle.transform.rotation = clickedShip.transform.rotation;
                redCircle.transform.parent = clickedShip.transform; // Set the clicked ship as the parent

                // Store the pair of ship and red circle in the dictionary
                shipRedCircleMap[clickedShip] = redCircle;

                Debug.Log("New Red Circle created for " + clickedShip.name);
            }
            else if (hit.collider.CompareTag("Ocean") && !isMoving)
            {
                Debug.Log("clicked ocean");
                if (shipRedCircleMap.ContainsKey(gameObject))
                {
                    Debug.Log("ship to move");
                    MoveSelectedShipToPosition(gameObject, hit.point);
                }


                else
                {
                    Debug.Log("ship not to move");
                }
            }
            else if (hit.collider.CompareTag("Ocean") && isMoving)
            {
                if (shipRedCircleMap.ContainsKey(gameObject))
                {
                    Debug.Log("ship to move");


                }
                else
                {
                    Debug.Log("ship not to move");
                }
            }
        }
    }
    public void StopSmoothRotateAndMoveCoroutine()
    {
        if (smoothRotateAndMoveCoroutine != null)
        {
            StopCoroutine(smoothRotateAndMoveCoroutine);
        }
    }
    private void MoveSelectedShipToPosition(GameObject ship, Vector3 targetPosition)
    {
        if (smoothRotateAndMoveCoroutine != null)
        {
            StopCoroutine(smoothRotateAndMoveCoroutine);
        }
        smoothRotateAndMoveCoroutine = StartCoroutine(SmoothRotateAndMove(ship.transform, targetPosition));
    }
    private IEnumerator SmoothRotateAndMove(Transform shipTransform, Vector3 targetPosition)
    {
        Quaternion targetRotation = Quaternion.LookRotation(-targetPosition + shipTransform.position);

        while (Quaternion.Angle(shipTransform.rotation, targetRotation) > 0.1f)
        {
            if (shipTransform.name != "float")
            {
                shipTransform.rotation = Quaternion.RotateTowards(shipTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Rotate each child (excluding excludedChild) up to the child's child depth
            foreach (Transform child in shipTransform)
            {
                if (child.name != "float")
                {
                    child.rotation = Quaternion.RotateTowards(child.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
                foreach (Transform grandchild in child)
                {
                    grandchild.rotation = Quaternion.RotateTowards(grandchild.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
            yield return null;
        }

        shipTransform.rotation = targetRotation; // Ensure the final rotation is exactly the target rotation

        // Smooth move
        float distance = Vector3.Distance(shipTransform.position, targetPosition);
        float duration = distance / moveSpeed;
        float elapsedTime = 0f;
        Vector3 initialPosition = shipTransform.position;

        while (elapsedTime < duration)
        {
            shipTransform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        shipTransform.position = targetPosition; // Ensure the final position is exactly the target position
    }
    private void UpdateRedCircleTransforms()
    {
        foreach (var pair in shipRedCircleMap)
        {
            GameObject ship = pair.Key;
            GameObject redCircle = pair.Value;

            // Update the red circle position and rotation to match the ship's current position and rotation
            if (redCircle != null && ship != null)
            {
                redCircle.transform.position = ship.transform.position;
                redCircle.transform.rotation = ship.transform.rotation;
            }
        }
    }
    public void HandleSink(GameObject ship)
    {
        RemoveShipFromAllSelectors(ship);
    }

    private static void RemoveShipFromAllSelectors(GameObject ship)
    {
        foreach (var selector in allSelectors)
        {
            selector.RemoveShipFromDictionary(ship);
        }
    }

    private void RemoveShipFromDictionary(GameObject ship)
    {
        if (shipRedCircleMap.ContainsKey(ship))
        {
            Debug.Log("sinked ship" + ship.name);
            Destroy(shipRedCircleMap[ship]);
            shipRedCircleMap.Remove(ship);
        }
    }
    public Dictionary<GameObject, GameObject> GetShipRedCircleMap()
    {
        return shipRedCircleMap;
    }

}
