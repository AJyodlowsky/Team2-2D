using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

//Written by Jancy



public class PlayerController : MonoBehaviour
{
   
    private float horizontal;
    private float speed = 2f;
    private float jumpingPower = 5f;
    private bool isFacingRight = true;
    public Animator animator;
    private bool doubleJump;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private int berriesCollected;
    //[SerializeField] private TMP_Text berryScore;
    //[SerializeField] private TMP_Text darkBerryScore;
    //private AudioSource audioSource;
    //public AudioClip fruitSFX;
    //public AudioClip deathSFX;
    //public AudioClip jumpSFX;
    //public AudioClip goalSFX;
    //public AudioClip doubleJumpSFX;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        //berryScore.text = "Score: " + berriesCollected;
        //darkBerryScore.text = "Score: " + berriesCollected;
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            animator.SetBool("IsDoubleJump", false);
            animator.SetBool("IsJumping", false);
            doubleJump = false;
        }
       
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
            //audioSource.clip = jumpSFX;
            //audioSource.Play();
            if (IsGrounded() || doubleJump)
            {
                
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                doubleJump = !doubleJump;
                if (!doubleJump)
                {
                    //audioSource.clip = doubleJumpSFX;
                    //audioSource.Play();
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsDoubleJump", true);
                }
            }
           
            
        }
       


        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            
        }
        Flip();
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
       
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Berry")
        {
            other.gameObject.SetActive(false);
            //berriesCollected++;
            //audioSource.clip = fruitSFX;
            //audioSource.Play();
        }
        if (other.gameObject.tag == "Hazard") 
        {
            Invoke("DeathTransiton", 1.0f);
            animator.SetBool("IsDead", true);
            //audioSource.clip = deathSFX;
            //audioSource.Play();
        }
        if (other.gameObject.tag == "Goal" && berriesCollected == 3)
        {
            Invoke("LevelTransiton", 1.0f);
            animator.SetBool("HasWon", true);
            //audioSource.clip = goalSFX;
            //audioSource.Play();
        }
    }
    void LevelTransiton() 
    {
        SceneManager.LoadScene("LevelTwo");
    }
    void DeathTransiton()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
