using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAutoRotate : MonoBehaviour
{

    private float targetAngle;
    private float currentAngle;
    private float speedRotate = 2f;

    private void Start()
    {
        targetAngle = Random.Range(10f, 50f);
        
    }

    private IEnumerator RotateTurretAuto()
    {
        currentAngle = transform.eulerAngles.y;

        yield return new WaitForSeconds(targetAngle);

        while (currentAngle <= targetAngle)
        {

        }
    }


}
