using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void Change()
    {
        SceneManager.LoadScene("Hansan_Scene");
    }
}
