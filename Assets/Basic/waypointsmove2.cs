using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointsmove2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] waypoints;
    public float speed = 5f;
    private int currentWaypointIndex = 0;

    void Start(){
        RotateTowardsWaypoint();
    }
    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            // 목표 지점으로 이동
            transform.Translate((waypoints[currentWaypointIndex].position - transform.position).normalized * speed * Time.deltaTime);

            // 목표 지점에 도달했는지 확인
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex < waypoints.Length)
                {
                    RotateTowardsWaypoint();
                }
            }
            else if (currentWaypointIndex == waypoints.Length - 1)
            {
                // 마지막 웨이포인트에 도달한 경우 더 이상 회전하지 않도록 함
                transform.rotation = Quaternion.LookRotation((waypoints[currentWaypointIndex].position - transform.position).normalized);
            }
        }

    }
    void RotateTowardsWaypoint()
    {
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // 회전 속도 조절
        foreach (Transform child in transform)
        {
            if(child.name == "float"){

                foreach (Transform grandchild in child)
                {
                    grandchild.rotation = lookRotation;
                }
            }
        }
    }
}
