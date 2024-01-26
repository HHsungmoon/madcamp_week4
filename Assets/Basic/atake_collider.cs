using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atake_collider : MonoBehaviour
{
    private Rigidbody rb;
    private ShipMove shipMoveScript; // ShipMove 스크립트를 저장할 변수
    private ataka_status shipStatusScript; // ship_status 스크립트를 저장할 변수

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 이 GameObject에서 ShipMove 스크립트의 인스턴스를 찾아 저장합니다.
        shipMoveScript = GetComponent<ShipMove>();
        // 이 GameObject에서 ship_status 스크립트의 인스턴스를 찾아 저장합니다.
        shipStatusScript = GetComponent<ataka_status>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider is BoxCollider)
        {
            // 충돌 시 ShipMove 스크립트를 비활성화합니다.
            if (shipMoveScript != null)
            {
                shipMoveScript.enabled = false;
            }
            
            StartCoroutine(HandleCollisionEffect());
        }
    }

    IEnumerator HandleCollisionEffect()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;

        yield return new WaitForSeconds(3f);

        // 3초 후, Rigidbody의 위치를 조정하고 물리 시뮬레이션에 다시 참여시킵니다.
        rb.position = new Vector3(rb.position.x, 0, rb.position.z);
        rb.isKinematic = true;

        // ShipMove 스크립트를 다시 활성화합니다.
        if (shipMoveScript != null)
        {
            shipMoveScript.enabled = true;
            rb.isKinematic = false;
        }
    }
}
