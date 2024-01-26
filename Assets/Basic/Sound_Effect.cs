using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Effect : MonoBehaviour
{
    void Update()
    {
        // 1~4 키를 누를 때 각각의 자식 오브젝트를 활성화/비활성화
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(ToggleChild(0));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(ToggleChild(1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(ToggleChild(2));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(ToggleChild(3));
        }
    }

    IEnumerator ToggleChild(int index)
    {
        // 지정된 인덱스의 자식 오브젝트를 찾아 활성화
        if (transform.childCount > index)
        {
            Transform child = transform.GetChild(index);
            child.gameObject.SetActive(true);

            // AudioSource 컴포넌트가 있다면 재생
            AudioSource audio = child.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }

            // 재생이 끝날 때까지 대기 (예: AudioSource의 clip 길이만큼)
            yield return new WaitForSeconds(audio != null ? audio.clip.length : 0);

            // 다시 비활성화
            child.gameObject.SetActive(false);
        }
    }
}
