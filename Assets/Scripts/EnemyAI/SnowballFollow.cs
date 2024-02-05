using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, playerPos) < speed * Time.deltaTime)
        {
            Destroy(this.gameObject);
        }
    }

}
