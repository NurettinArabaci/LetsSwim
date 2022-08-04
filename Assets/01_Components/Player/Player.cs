using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform mT;
    Animator mAnim;
    Rigidbody rb;
    int speed = 10;

    public static bool isActiveGame;

    public static bool upMovement;

    private void Awake()
    {
        mT = transform;
        mAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.zero;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mT.position += Vector3.down / 2;
            upMovement = false;
            mAnim.SetTrigger("swim");
        }
        else if (Input.GetMouseButton(0))
        {
            mT.position += (Vector3.forward+Vector3.down/4) * Time.deltaTime * speed;
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            upMovement = true;
      
        }

        if (upMovement)
        {
            mT.position += (Vector3.forward + Vector3.up / 4) * Time.deltaTime * speed;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {

            upMovement = false;
            mAnim.SetTrigger("idle");
        }
    }
    
}
