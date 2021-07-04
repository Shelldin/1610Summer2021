using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPropeller : MonoBehaviour
{
    private Vector3 rotation = new Vector3(0,0,1500);
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * (Time.deltaTime));
    }
}
