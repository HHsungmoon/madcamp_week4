using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipingLeft : MonoBehaviour
{
    private Quaternion[] rotations = new Quaternion[]
     {
        Quaternion.Euler(0, 0, -130),
        Quaternion.Euler(-35, 0, -130),
        //Quaternion.Euler(-35, 17, 110),
        Quaternion.Euler(-70, 92, -205),
        //Quaternion.Euler(-70, -92, 182),
        Quaternion.Euler(0, 0, -110),
        Quaternion.Euler(0, 0, -130)
     };

    // Start is called before the first frame update
    void Start()
    {
    }

    private float rotationTime = 0.3f;
    private bool isRotating = false; // ȸ�� ������ Ȯ���ϴ� �÷���

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !isRotating)
        {
            StartCoroutine(SmoothRotateSequence());
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !isRotating)
            StartCoroutine(SmoothRotateSequence());
    }

    IEnumerator SmoothRotateSequence()
    {
        isRotating = true;

        // �θ� ������Ʈ�� ���� ȸ�� �� ��������
        Quaternion parentRotation = transform.parent.rotation;

        for (int i = 0; i < rotations.Length; i++)
        {
            Quaternion startRotation = transform.rotation;
            // �θ��� ȸ���� ����Ͽ� ��ǥ ȸ�� �� ���
            Quaternion endRotation = parentRotation * rotations[i];
            float elapsedTime = 0;

            float currentRotationTime = i == 1 ? rotationTime * 3 : rotationTime;

            while (elapsedTime < currentRotationTime)
            {
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / currentRotationTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = endRotation;
        }

        isRotating = false;
    }
}
