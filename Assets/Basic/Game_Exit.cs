using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Exit : MonoBehaviour
{
    void Update()
    {
        // ESC 키 입력을 감지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 게임 종료
            Application.Quit();

            // 유니티 에디터에서 실행 중이라면 에디터를 멈추는 코드
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}