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
        float duration = 5f; // 회전에 걸리는 시간: 5초
        float startRotation = transform.eulerAngles.y; // 현재 오브젝트의 시작 회전 각도
        float endRotation = startRotation + 180f; // 종료 회전 각도: 현재 각도에서 180도 추가
        float timeElapsed = 0f; // 경과 시간

        // 'float' 자식 오브젝트의 'Panok_ship_(Panokseon)____' 자식 오브젝트 찾기
        Transform panokShipTransform = transform.Find("float/Panok_ship_(Panokseon)____");

        while (timeElapsed < duration)
        {
            float yRotation = Mathf.Lerp(startRotation, endRotation, timeElapsed / duration) % 360f; // 360으로 나누어 각도를 0~359 범위로 유지
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z); // 현재 오브젝트의 y축 회전 적용

            // Panok_ship_(Panokseon)____ 오브젝트에도 동일한 회전 적용
            if (panokShipTransform != null)
            {
                panokShipTransform.eulerAngles = new Vector3(panokShipTransform.eulerAngles.x, yRotation, panokShipTransform.eulerAngles.z);
            }

            timeElapsed += Time.deltaTime; // 경과 시간 업데이트
            yield return null; // 다음 프레임까지 대기
        }

        // 마지막 회전 각도를 정확히 180도 회전된 각도로 설정
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, endRotation % 360f, transform.eulerAngles.z);
        if (panokShipTransform != null)
        {
            panokShipTransform.eulerAngles = new Vector3(panokShipTransform.eulerAngles.x, endRotation % 360f, panokShipTransform.eulerAngles.z);
        }
    }
    public void Rotation_Half()
    {
        StartCoroutine(RotateHalf()); // 코루틴 시작
    }
}
