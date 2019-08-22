using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;

    public static Laser sharedInstance;
    public Vector3 startPosition;

    private void Awake()
    {
        sharedInstance = this;
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    public void Start()
    {
        speed = 4.0f;
        this.transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
        else
        {
            transform.position = startPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.sharedInstance.Kill();
            // GameManager.sharedInstance.SetGameState(GameState.endGame);
        }
    }
}
