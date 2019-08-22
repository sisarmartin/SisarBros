using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemyController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float jumpForce = 38.0f;
    public Animator animator;

    public static FirstEnemyController sharedInstance;

    public Vector3 startPosition;

    float currentTime = 0;
    float maxTime = 2;

    private void Awake()
    {
        sharedInstance = this;
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isGrounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            currentTime = 0;
            Jump();
        }
        animator.SetBool("isGrounded", IsTouchinTheGround());
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
            PlayerController.sharedInstance.groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.sharedInstance.Kill();
        }
    }
}
