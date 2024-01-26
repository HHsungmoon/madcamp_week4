using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Angle : MonoBehaviour
{
    public GameObject cannonRight;
    public GameObject cannonLeft;
    public GameObject cannonFront;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) // 포각 올리기
        {
            AngleUP();
        }
        if (Input.GetKeyDown(KeyCode.N)) // 포각 내리기
        {
            AngleDN();
        }
    }

    public void AngleUP()
    {
        AdjustAngle(-5);
    }

    public void AngleDN()
    {
        AdjustAngle(5);
    }

    void AdjustAngle(float adjustment)
    {
        AdjustCannonAngle(cannonRight, adjustment);
        AdjustCannonAngle(cannonLeft, adjustment);
        AdjustCannonAngle(cannonFront, adjustment);
    }

    void AdjustCannonAngle(GameObject cannon, float adjustment)
    {
        if (cannon != null)
        {
            Vector3 currentRotation = cannon.transform.localEulerAngles;
            float newXRotation = currentRotation.x + adjustment;

            if (newXRotation > 180) newXRotation -= 360; 
            newXRotation = Mathf.Clamp(newXRotation, -5, 10);

            cannon.transform.localEulerAngles = new Vector3(newXRotation, currentRotation.y, currentRotation.z);
        }
    }
}
