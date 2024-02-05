using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VelocityCounter : MonoBehaviour
{

    private TextMeshProUGUI velocityText;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        velocityText = GetComponent<TextMeshProUGUI>();
        velocityText.text = "Velocity: ";
    }

    // Update is called once per frame
    void Update()
    {
        velocityText.text = "Velocity: " + Mathf.Abs(Mathf.Round(player.GetComponent<Rigidbody2D>().velocity.magnitude));
    }
}
