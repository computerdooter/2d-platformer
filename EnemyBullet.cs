using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (target.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if (target.gameObject.tag == "deadly")
        {
            Destroy(gameObject);
        }
        if (target.gameObject.tag == "damage")
        {
            Destroy(gameObject);
        }
    }
    
}
