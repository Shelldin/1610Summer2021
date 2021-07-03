using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    public float turnSpeed;
    private float horizontalInput;
    private float forwardInput;

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        
       transform.Translate(Vector3.forward * (Time.deltaTime * speed* forwardInput)); 
       transform.Rotate(Vector3.up,Time.deltaTime * turnSpeed * horizontalInput);
    }
    
}
