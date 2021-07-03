using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 5, -7);
    
    public GameObject player;

    private void LateUpdate()
    {
        transform.position = player.transform.position +offset;
    }
}
