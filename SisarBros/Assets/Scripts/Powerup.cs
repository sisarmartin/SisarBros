using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public static Powerup sharedInstance;
    public Vector3 startPosition;

    private void Awake()
    {
        sharedInstance = this;
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            PlayerController.sharedInstance.jumpForce = 30.0f;
        }
    }
}
