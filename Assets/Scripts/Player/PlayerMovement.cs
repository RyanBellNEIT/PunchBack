using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    #region Variables
    [Header("Movement Vars")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float health = 100f;

    [Header("Gameobjects")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject fist;

    [Header("Scripts")]
    [SerializeField] private PunchController punchController;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PauseMenu pauseMenu;

    [Header("Audio")]
    [SerializeField] private AudioSource playerSFX;
    [SerializeField] private AudioClip birdDamage;
    [SerializeField] private AudioClip gameOver;

    //Internal Variables
    private bool canTakeDamage;
    private bool deathPlaying;
    private Animator anim;
    #endregion

    #region Unity Functions
    void Start()
    {
        anim = GetComponent<Animator>();
        health = 100f;
        canTakeDamage = true;
        deathPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        { 
            float inputX = Input.GetAxis("Horizontal");

            Vector3 movement = new Vector3(speed * inputX, 0 , 0);

            movement *= Time.deltaTime;

            if (inputX == 0)
            {
                anim.SetBool("Running", false);
            }
            else
            {
                anim.SetBool("Running", true);
            }

            transform.Translate(movement);

            if (Input.GetKeyDown(KeyCode.A))
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }


            if (Input.GetMouseButtonDown(0))
            {
                arrow.SetActive(true);
            }

            else if (Input.GetMouseButtonUp(0))
            {
                arrow.SetActive(false);
                fist.SetActive(true);
                StartCoroutine("FistCooldown");
            }
        }
        else if (health <= 0 && deathPlaying == false)
        {
            deathPlaying = true;
            anim.SetBool("Running", false);
            anim.SetBool("Jumping", false);
            anim.SetBool("Sliding", false);
            health = 0;
            StartCoroutine(DeathSound());
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.Pause();
        }

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            float bounce = 1200f;
            GetComponent<Rigidbody2D>().AddForce(col.contacts[0].normal * bounce);
            if (canTakeDamage == true)
            {
                playerSFX.PlayOneShot(birdDamage);
                health -= 34f;
                healthBar.SetHealth(health);
                StartCoroutine("TookDamage");
            }
        }

        if (col.gameObject.tag == "Snowball")
        {
            float bounce = 800f;
            GetComponent<Rigidbody2D>().AddForce(col.contacts[0].normal * bounce);
            Debug.Log("Snowball Hit");
            if (canTakeDamage == true)
            {
                health -= 10f;
                healthBar.SetHealth(health);
            }
        }

        else if (col.gameObject.tag == "Ground")
        {
            punchController.SetIsGrounded(true);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            if(Input.GetKey(KeyCode.A))
            {
                ContactPoint2D contact = col.contacts[0];
                Vector2 pos = contact.point;
                if (pos.x < transform.position.x && GetComponent<SpriteRenderer>().flipX && Input.GetKey(KeyCode.A))
                {
                    anim.SetBool("Sliding", true);
                    GetComponent<Rigidbody2D>().gravityScale = 0.8f;
                }
                else
                {
                    anim.SetBool("Sliding", false);
                    GetComponent<Rigidbody2D>().gravityScale = 5f;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                ContactPoint2D contact2 = col.contacts[0];
                Vector2 pos2 = contact2.point;
                if (pos2.x > transform.position.x && !GetComponent<SpriteRenderer>().flipX && Input.GetKey(KeyCode.D))
                {
                    anim.SetBool("Sliding", true);
                    GetComponent<Rigidbody2D>().gravityScale = 0.8f;
                }
                else
                {
                    anim.SetBool("Sliding", false);
                    GetComponent<Rigidbody2D>().gravityScale = 5f;
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().gravityScale = 5f;
            }
        }
        else
        {
            anim.SetBool("Sliding", false);
        }
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            anim.SetBool("Sliding", false);
            GetComponent<Rigidbody2D>().gravityScale = 5f;
        }
    }

    #endregion

    #region Functions
    IEnumerator FistCooldown()
    {
        yield return new WaitForSeconds(.25f);
        fist.SetActive(false);
    }

    IEnumerator DeathSound()
    {
        playerSFX.volume = 0.5f;
        playerSFX.PlayOneShot(gameOver);
        yield return new WaitForSeconds(1f);
        playerSFX.volume = 1f;
        deathPlaying = false;
        SceneManager.LoadScene("Game Over");
    }

    IEnumerator TookDamage()
    {
        canTakeDamage = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(.5f);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(.5f);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(.5f);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(.5f);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(.5f);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(.1f);
        canTakeDamage = true;
    }
    #endregion

    #region Setters/Getters
    public void SetHealth(int health)
    {
        this.health = health;
        healthBar.SetHealth(health);
    }

    public float GetHealth()
    {
        return health;
    }
    #endregion
}
