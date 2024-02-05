using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchController : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] private float speed = 100f;
    [SerializeField] private float velocity = 45f;
    [SerializeField] private float range = 2.75f;
    [SerializeField] private float enemyKillVelocity = 4f;

    [Header("Gameobjects")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fist;

    [Header("Particles")]
    [SerializeField] private GameObject blood;
    [SerializeField] private GameObject dirt;
    [SerializeField] private GameObject snow;

    [Header("Audio")]
    [SerializeField] private AudioClip snowPunch;
    [SerializeField] private AudioClip enemyPunch;

    //Internal Vars
    private Vector2 lookDir;
    private AudioSource SFXPlayer;
    private Image arrowImage;
    private bool particlePlaying = false;
    private bool touchingGround;
    private Animator playerAnim;
    #endregion

    private void Start()
    {
        playerAnim = this.transform.root.GetComponent<Animator>();
        lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SFXPlayer = GetComponent<AudioSource>();
        arrowImage = arrow.GetComponent<Image>();
    }

    private void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        var mouseDist = Vector2.Distance(worldPos, player.transform.position);
        arrowImage.color = Color.Lerp(Color.white, Color.red, mouseDist / 10f);
    }

    public void FixedUpdate()
    {

        lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D[] hits;

        hits = Physics2D.RaycastAll(transform.position, transform.TransformDirection(Vector2.right), range);

        if (touchingGround == true)
        {
            playerAnim.SetBool("Jumping", false);
        }

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];

            if (hit.collider.CompareTag("Snow"))
            {
                if (fist.activeInHierarchy == true)
                {
                    SFXPlayer.Play();
                    player.GetComponent<Rigidbody2D>().AddForce(lookDir * -1 * velocity, ForceMode2D.Force);
                    touchingGround = false;
                    playerAnim.SetBool("Jumping", true);
                    if (particlePlaying == false)
                    {
                        particlePlaying = true;
                        Instantiate(snow, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
                        StartCoroutine(WaitForFist());

                    }
                }
            }
            else if (hit.collider.CompareTag("Ground"))
            {
                if (fist.activeInHierarchy == true)
                {
                    SFXPlayer.Play();
                    player.GetComponent<Rigidbody2D>().AddForce(lookDir * -1 * velocity, ForceMode2D.Force);
                    touchingGround = false;
                    playerAnim.SetBool("Jumping", true);
                    if (particlePlaying == false)
                    {
                        particlePlaying = true;
                        Instantiate(dirt, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
                        StartCoroutine(WaitForFist());
                        
                    }
                }
            }
            else if (hit.collider.CompareTag("Enemy"))
            {
                if (fist.activeInHierarchy == true)
                {
                    SFXPlayer.PlayOneShot(enemyPunch);
                    player.GetComponent<Rigidbody2D>().AddForce(lookDir * enemyKillVelocity * velocity, ForceMode2D.Force);
                    Instantiate(blood, hit.transform.position, Quaternion.identity);
                    Destroy(hit.transform.gameObject);
                }
            }
            else if(hit.collider.CompareTag("Snowman") && hit.collider.isTrigger == false)
            {
                if (fist.activeInHierarchy == true)
                {
                    SFXPlayer.Play();
                    player.GetComponent<Rigidbody2D>().AddForce(lookDir * enemyKillVelocity * velocity, ForceMode2D.Force);
                    Instantiate(snow, hit.transform.position, Quaternion.identity);
                    Destroy(hit.transform.gameObject);
                }
            }
            else if(hit.collider.CompareTag("FinalEnemy"))
            {
                if (fist.activeInHierarchy == true)
                {
                    SFXPlayer.PlayOneShot(enemyPunch);
                    Instantiate(blood, hit.transform.position, Quaternion.identity);
                    Destroy(hit.transform.gameObject);
                }
            }
            else if (hit.collider.CompareTag("Snowball"))
            {
                if (fist.activeInHierarchy == true)
                {
                    SFXPlayer.Play();
                    player.GetComponent<Rigidbody2D>().AddForce(lookDir * enemyKillVelocity * velocity, ForceMode2D.Force);
                    Destroy(hit.transform.gameObject);
                    Instantiate(snow, hit.transform.position, Quaternion.identity);
                }
            }
            else
            {

            }
        }

        IEnumerator WaitForFist()
        {
            if (fist.activeInHierarchy == true)
            {
                yield return new WaitForSeconds(.25f);
                particlePlaying = false;
            }
        }

    }

    #region Setters/Getters

    public void SetIsGrounded(bool isGrounded)
    {
        this.touchingGround = isGrounded;
    }
    public bool IsGrounded()
    {
        return touchingGround;
    }
    #endregion

}
