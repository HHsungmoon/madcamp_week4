using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Effect : MonoBehaviour
{
    void Update()
    {
        // 1~4 Ű�� ���� �� ������ �ڽ� ������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ
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
        // ������ �ε����� �ڽ� ������Ʈ�� ã�� Ȱ��ȭ
        if (transform.childCount > index)
        {
            Transform child = transform.GetChild(index);
            child.gameObject.SetActive(true);

            // AudioSource ������Ʈ�� �ִٸ� ���
            AudioSource audio = child.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }

            // ����� ���� ������ ��� (��: AudioSource�� clip ���̸�ŭ)
            yield return new WaitForSeconds(audio != null ? audio.clip.length : 0);

            // �ٽ� ��Ȱ��ȭ
            child.gameObject.SetActive(false);
        }
    }
}
