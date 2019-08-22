using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float jumpForce;
    public Animator animator;
    public float runningSpeed;
    public Text scoreText;
    public Text deadScoreText;
    public Text maxScoreText;
    int count = 0;
    public Text levelText;
    public GameObject player;

    public LayerMask groundLayer;

    public static PlayerController sharedInstance;
    public Vector3 startPosition;

    private void Awake()
    {
        sharedInstance = this;
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        count = 0;
        runningSpeed = 5.0f;
        jumpForce = 15.0f;
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isMoving", false);
        this.transform.position = startPosition;
        animator.SetBool("isDimoni", false);

        Powerup.sharedInstance.Start();
        Coin.sharedInstance.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            animator.SetBool("isGrounded", IsTouchinTheGround());
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
                animator.SetBool("isMoving", true);
                count = count + 1;
                SetScore();
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }

        if (GameManager.sharedInstance.currentGameState == GameState.endGame)
        {
            SetDeadScore();
            SetMaxScore();
        }
    }

    private void MoveRight()
    {
        if (rigidbody.velocity.x < runningSpeed)
        {
            rigidbody.velocity = new Vector2(runningSpeed, rigidbody.velocity.y);
        }
    }

    private void MoveLeft()
    {
        if (rigidbody.velocity.x < runningSpeed)
        {
            rigidbody.velocity = new Vector2(-runningSpeed, rigidbody.velocity.y);
        }
    }

    private void Jump()
    {
        if (IsTouchinTheGround())
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsTouchinTheGround()
    {
        if (Physics2D.Raycast(this.transform.position,
            Vector2.down,
            3.1f,
            groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SetScore()
    {
        scoreText.text = "Score " + count.ToString();
    }

    public void SetDeadScore()
    {
        deadScoreText.text = "Score " + count.ToString();
    }

    public void SetMaxScore()
    {
        maxScoreText.text = "Score " + count.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Kill();
        }
    }

    public void Kill()
    {
        GameManager.sharedInstance.DeadGame();
        animator.SetBool("isAlive", false);
        SetDeadScore();
    }

    public void End()
    {
        SetMaxScore();
        GameManager.sharedInstance.EndGame();
    }
}
