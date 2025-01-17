using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public VariableJoystick joystick;
    public CharacterController controller;
    public float movementSpeed;
    public float rotationSpeed;
    public MoveInZoneResource moveInZoneResource;

    public Canvas inputCanvas;
    public bool isJoystick;

    public Animator animator;

    private Vector3 lastMovementDirection = Vector3.zero;


    private void Start()
    {
        moveInZoneResource.PlayerInZoneResource += PayerMoveZoneResource;
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

            if (movementDirection.sqrMagnitude > 0.01f)
            {
                lastMovementDirection = movementDirection.normalized;
                animator.SetBool("isRunning", true);
                

                RotateTowardsMovementDirection(movementDirection);
                controller.SimpleMove(lastMovementDirection * movementSpeed);

            }
            else
            {
                animator.SetBool("isRunning", false);
                lastMovementDirection = Vector3.zero;
                controller.SimpleMove(Vector3.zero);
            }

            
        }

    }

    private void RotateTowardsMovementDirection(Vector3 movementDirection)
    {
        if (movementDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public bool IsMoving()
    {
        return joystick.Direction.sqrMagnitude > 0.01f;
    }

    public void PayerMoveZoneResource(bool playerInZoneResource)
    {
        moveInZoneResource.IsZoneResource = playerInZoneResource;
    }
}
