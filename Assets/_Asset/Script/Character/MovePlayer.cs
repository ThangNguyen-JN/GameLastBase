using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        MovementPlayer();
    }

    public void MovementPlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}
