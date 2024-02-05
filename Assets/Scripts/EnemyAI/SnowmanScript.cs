using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanScript : MonoBehaviour
{
    [SerializeField] private GameObject snowballPrefab;
    [SerializeField] private float attackRange = 15f;
    private GameObject player;
    private Animator snowmanAnim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        snowmanAnim = GetComponent<Animator>();
        attackRange = 15f;
        GetComponent<CircleCollider2D>().radius = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (this.transform.position.x < player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        snowmanAnim.SetBool("inRange", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        snowmanAnim.SetBool("inRange", false);
    }

    public void alertObservers(string message)
    {
        if (message.Equals("Snowball Throw"))
        {
            var snowball = Instantiate(snowballPrefab, gameObject.transform.position, gameObject.transform.rotation);

            snowball.transform.SetParent(this.transform);
        }
    }

}
