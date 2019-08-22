using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float speed = 4f;

    private Rigidbody2D rigidbody;
    public static EnemyController sharedInstance;
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
    void FixedUpdate()
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
}
