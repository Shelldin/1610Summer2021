using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject foodPrefab;
    
    private Vector3 leftBoundary;
    private Vector3 rightBoundary;
    
    private float horizontalInput;

    public float speed = 10f;
    public float xRange = 10f;

    void Start()
    {
        leftBoundary = new Vector3(-xRange, transform.position.y, transform.position.z);
        rightBoundary = new Vector3(xRange, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * (horizontalInput * Time.deltaTime * speed));
       
        if (transform.position.x < -xRange)
        {
            transform.position = leftBoundary;
        }

        if (transform.position.x > xRange)
        {
            transform.position = rightBoundary;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(foodPrefab, transform.position, foodPrefab.transform.rotation);
        }
        
    }
}
