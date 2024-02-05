using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfDestroyed : MonoBehaviour
{
    [SerializeField] private FadeToBlack black;
    [SerializeField] private Animator playerAnim;

    private void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        playerAnim.SetBool("LastBirdKilled", true);
        black.destroyed = 1;
    }
}
