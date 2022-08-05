using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform mT;
    Animator mAnim;
    Rigidbody rb;
    float limitY = -10;

    int speed = 10;

    public static bool isActiveGame;
    public static bool upMovement;
    public static bool isSwim;

    private void Awake()
    {
        mT = transform;
        mAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.zero;
        isSwim = true;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mT.position += Vector3.down / 2;
            upMovement = false;
            if (isSwim)
            {
                mAnim.SetTrigger("swim");
            }
            isSwim = false;
            
        }
        else if (Input.GetMouseButton(0))
        {
            
            mT.position += (Vector3.forward+Vector3.down/4) * Time.deltaTime * speed;
            mT.position = new Vector3(0, Mathf.Clamp(mT.position.y, -10, 2), mT.position.z);
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            upMovement = true;
      
        }

        if (upMovement)
        {
            mT.position += (Vector3.forward + Vector3.up) * Time.deltaTime * speed;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {

            upMovement = false;
            mAnim.SetTrigger("idle");
            isSwim = true;
        }
    }
    
}
