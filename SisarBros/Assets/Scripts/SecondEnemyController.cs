using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEnemyController : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float speed = 5f;

    private Rigidbody2D rigidbody;
    public static SecondEnemyController sharedInstance;
    public Vector3 startPosition;

    private void Awake()
    {
        sharedInstance = this;
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    public void Start()
    {
        this.transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            rigidbody.AddForce(Vector2.right * speed);
            float limitedSpeed = Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed);
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
        }
        else
        {
            transform.position = startPosition;
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
