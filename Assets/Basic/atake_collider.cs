using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atake_collider : MonoBehaviour
{
    private Rigidbody rb;
    private ShipMove shipMoveScript; // ShipMove ��ũ��Ʈ�� ������ ����
    private ataka_status shipStatusScript; // ship_status ��ũ��Ʈ�� ������ ����

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // �� GameObject���� ShipMove ��ũ��Ʈ�� �ν��Ͻ��� ã�� �����մϴ�.
        shipMoveScript = GetComponent<ShipMove>();
        // �� GameObject���� ship_status ��ũ��Ʈ�� �ν��Ͻ��� ã�� �����մϴ�.
        shipStatusScript = GetComponent<ataka_status>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider is BoxCollider)
        {
            // �浹 �� ShipMove ��ũ��Ʈ�� ��Ȱ��ȭ�մϴ�.
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

        // 3�� ��, Rigidbody�� ��ġ�� �����ϰ� ���� �ùķ��̼ǿ� �ٽ� ������ŵ�ϴ�.
        rb.position = new Vector3(rb.position.x, 0, rb.position.z);
        rb.isKinematic = true;

        // ShipMove ��ũ��Ʈ�� �ٽ� Ȱ��ȭ�մϴ�.
        if (shipMoveScript != null)
        {
            shipMoveScript.enabled = true;
            rb.isKinematic = false;
        }
    }
}
