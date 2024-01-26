using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JP_Hakic_group : MonoBehaviour
{
    // Start is called before the first frame update
    public int JS_group=0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            Rotation_Half();
        }
    }
    public int get_JS_group()
    {
        return this.JS_group;
    }
    IEnumerator RotateHalf()
    {
        float duration = 5f; // ȸ���� �ɸ��� �ð�: 5��
        float startRotation = transform.eulerAngles.y; // ���� ������Ʈ�� ���� ȸ�� ����
        float endRotation = startRotation + 180f; // ���� ȸ�� ����: ���� �������� 180�� �߰�
        float timeElapsed = 0f; // ��� �ð�

        // 'float' �ڽ� ������Ʈ�� 'Panok_ship_(Panokseon)____' �ڽ� ������Ʈ ã��
        Transform panokShipTransform = transform.Find("float/Panok_ship_(Panokseon)____");

        while (timeElapsed < duration)
        {
            float yRotation = Mathf.Lerp(startRotation, endRotation, timeElapsed / duration) % 360f; // 360���� ������ ������ 0~359 ������ ����
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z); // ���� ������Ʈ�� y�� ȸ�� ����

            // Panok_ship_(Panokseon)____ ������Ʈ���� ������ ȸ�� ����
            if (panokShipTransform != null)
            {
                panokShipTransform.eulerAngles = new Vector3(panokShipTransform.eulerAngles.x, yRotation, panokShipTransform.eulerAngles.z);
            }

            timeElapsed += Time.deltaTime; // ��� �ð� ������Ʈ
            yield return null; // ���� �����ӱ��� ���
        }

        // ������ ȸ�� ������ ��Ȯ�� 180�� ȸ���� ������ ����
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, endRotation % 360f, transform.eulerAngles.z);
        if (panokShipTransform != null)
        {
            panokShipTransform.eulerAngles = new Vector3(panokShipTransform.eulerAngles.x, endRotation % 360f, panokShipTransform.eulerAngles.z);
        }
    }
    public void Rotation_Half()
    {
        StartCoroutine(RotateHalf()); // �ڷ�ƾ ����
    }
}
