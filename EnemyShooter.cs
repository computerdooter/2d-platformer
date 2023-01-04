using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        Instantiate(bullet, transform.position, Quaternion.identity);
        StartCoroutine(Attack());
    }
}
