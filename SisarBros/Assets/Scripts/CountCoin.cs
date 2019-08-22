using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountCoin : MonoBehaviour
{
    public int count = 0;
    public Text coinsText;
    public static CountCoin sharedInstance;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            count = count + 1;
            SetCoins();
        }

        void SetCoins()
        {
            coinsText.text = "Coins " + count.ToString();
        }
    }
}
