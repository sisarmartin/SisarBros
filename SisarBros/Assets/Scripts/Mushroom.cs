using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public static Mushroom sharedInstance;
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
            PlayerController.sharedInstance.animator.SetBool("isDimoni", true);
            PlayerController.sharedInstance.runningSpeed = 10.0f;
        }
    }
}
