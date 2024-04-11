using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Cinemachine;

//Written by Jancy



public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _startingSceneTransistion, _endingSceneTransistion;
    [SerializeField] private GameObject deathAnimation, lightPitBackGround, darkPitBackGround, howToReadSignText, signText, goalEffectObject;
    [SerializeField] private GameObject virtualCineCamrea;
    private float horizontal;
    [SerializeField] private float speed = 5f, minSpeedToPlaySound = 0.1f;
    private float jumpingPower = 6.2f;
    private bool isFacingRight = true;
    public Animator animator;
    private bool doubleJump;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private int berriesCollected;
    [SerializeField] private TMP_Text berryScore, darkBerryScore;
    [SerializeField] private bool isColliding = false;
    private AudioSource audioSource;
    public AudioClip fruitSFX, deathSFX, jumpSFX, goalSFX, doubleJumpSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        goalEffectObject.SetActive(false);
        signText.SetActive(false);
        howToReadSignText.SetActive(false);
        _endingSceneTransistion.SetActive(false);
        //_startingSceneTransistion.SetActive(true);
        deathAnimation.SetActive(false);
        lightPitBackGround.SetActive(false);
        darkPitBackGround.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        berryScore.text = "Score: " + berriesCollected;
        darkBerryScore.text = "Score: " + berriesCollected;
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if (Input.GetKeyDown(KeyCode.Z) && isColliding)
        {
            signText.SetActive(!signText.activeSelf);
        }
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            animator.SetBool("IsDoubleJump", false);
            animator.SetBool("IsJumping", false);
            doubleJump = false;
        }
       
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
            audioSource.clip = jumpSFX;
            audioSource.Play();
            if (IsGrounded() || doubleJump)
            {
                
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                doubleJump = !doubleJump;
                if (!doubleJump)
                {
                    audioSource.clip = doubleJumpSFX;
                    audioSource.Play();
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

        if (berriesCollected == 3)
        {
            goalEffectObject.SetActive(true);
        }
        
    }
    private void DisableStartingSceneTransition()
    {
        _startingSceneTransistion.SetActive(false);
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
        if (other.gameObject.tag == "Info")
        {
            isColliding = true;
            howToReadSignText.SetActive(true);
        }
        if (other.gameObject.tag == "DeathPit")
        {
            Invoke("PlayDeathAnim", 1.0f);
            Invoke("GameOver", 2f);
            lightPitBackGround.SetActive(true);
            darkPitBackGround.SetActive(true);
            virtualCineCamrea.SetActive(false);
        }
        if (other.gameObject.tag == "Berry")
        {
            other.gameObject.SetActive(false);
            berriesCollected++;
            audioSource.clip = fruitSFX;
            audioSource.Play();
        }
        if (other.gameObject.tag == "Hazard") 
        {
            Invoke("DeathTransiton", 1.25f);
            animator.SetBool("IsDead", true);
            audioSource.clip = deathSFX;
            audioSource.Play();
        }
        if (other.gameObject.tag == "Goal" && berriesCollected == 3)
        {
            Invoke("GameOver", 1.0f);
            Invoke("LevelTransiton", 2.0f);
            animator.SetBool("HasWon", true);
            audioSource.clip = goalSFX;
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Info")
        {
            isColliding = false;
            howToReadSignText.SetActive(false);
            
        }
    }
    void LevelTransiton() 
    {
        //JAY WROTE THIS
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "LevelOne")
        {
            SceneManager.LoadScene("LevelTwo");
        }
        if(currentSceneName == "LevelTwo")
        {
            SceneManager.LoadScene("WIN");
        }
    }
    void DeathTransiton()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    void PlayDeathAnim()
    {
        deathAnimation.SetActive(true);
    }
    void GameOver()
    {
        _endingSceneTransistion.SetActive(true);
    }
}
