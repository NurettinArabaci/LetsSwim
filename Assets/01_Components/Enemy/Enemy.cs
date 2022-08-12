using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 target;
    int speed;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        speed = 2;
    }

    private void LateUpdate()
    {
        target=Vector3.Slerp(transform.position,PlayerMovement.currentPose+ Vector3.back*3+Vector3.up,speed*Time.deltaTime);
        rb.MovePosition(target);
        transform.LookAt(PlayerMovement.currentPose+Vector3.up);

    }

}
