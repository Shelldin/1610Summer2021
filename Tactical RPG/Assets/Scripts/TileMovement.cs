using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Transform movementPoint;
    
    private Vector3 movePositionVector3 = new Vector3(0f, 0f, 0f);

    private void Start()
    {
        movementPoint.parent = null;
    }

    private void Update()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, movementPoint.position, 
                movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movementPoint.position)<=.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) ==1f)
            {
                movePositionVector3.x = Input.GetAxisRaw("Horizontal");
                movePositionVector3.y = 0f;
                movementPoint.position += movePositionVector3;
            }

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical"))==1f)
            {
                movePositionVector3.y = Input.GetAxisRaw("Vertical");
                movePositionVector3.x = 0f;
                movementPoint.position += movePositionVector3;
            }
            {
                
            }
        }
    }
}
