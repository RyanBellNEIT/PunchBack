using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerMovement.SetHealth(0);
        }
    }
}
