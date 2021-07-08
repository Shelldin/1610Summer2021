using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    //bonus challenge 9 solution is from a comments section of the challenge. My solution involved coroutines and was not as short or simple
    public GameObject dogPrefab;
    public float spawnCooldown = 3f;
    private float timePassed = 0f;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= timePassed)
        {
            timePassed = Time.time + spawnCooldown;
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}
