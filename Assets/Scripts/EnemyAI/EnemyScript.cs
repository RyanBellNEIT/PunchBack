using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float length = 2;
    [SerializeField] private float speed = 2;
    private float minLength = 2f;
    private float oldPosition;

    void Start()
    {
        minLength = transform.position.x;
        oldPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, length) + minLength, 
            transform.position.y, transform.position.z);

        if (transform.position.x < oldPosition)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (transform.position.x > oldPosition)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        oldPosition = transform.position.x;
    }

}
