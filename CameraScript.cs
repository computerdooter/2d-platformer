using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform player;

    //public float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerScript22>().isAlive)
        {
            Vector3 temp = transform.position;
            temp.x = player.position.x;
            transform.position = temp;
        }
    }
}
