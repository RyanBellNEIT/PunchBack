using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBirdFlying : MonoBehaviour
{

    [SerializeField] private GameObject bird;
    private Animator birdAnim;

    private void Start()
    {
        birdAnim = bird.GetComponent<Animator>();
    }

    private void Update()
    {
        if(birdAnim.GetBool("FlyAway"))
        {
            bird.transform.Translate(Vector2.right * 20 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        birdAnim.SetBool("FlyAway", true);
    }
}
