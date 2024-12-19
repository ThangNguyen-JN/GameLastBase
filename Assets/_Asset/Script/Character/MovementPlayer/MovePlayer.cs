using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public VariableJoystick joystick;
    public CharacterController controller;
    public float movementSpeed;
    public float rotationSpeed;

    public Canvas inputCanvas;
    public bool isJoystick;

    public Animator animator;
   

    private void Start()
    {
        EnableJoystickInput();
    }

    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }

    void Update()
    {
        MovementPlayer();
    }

    public void MovementPlayer()
    {
        if(isJoystick)
        {
            Vector3 movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            controller.SimpleMove(movementDirection * movementSpeed);

            if (movementDirection.sqrMagnitude <=0)
            {
                animator.SetBool("isRunning", false);
                return;

            }

            animator.SetBool("isRunning", true);

            var targetDirection = Vector3.RotateTowards(controller.transform.forward, movementDirection, rotationSpeed * Time.deltaTime, 0.0f);

            controller.transform.rotation = Quaternion.LookRotation(targetDirection);
        }

    }
}
