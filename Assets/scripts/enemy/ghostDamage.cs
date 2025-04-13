using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostDamage : MonoBehaviour
{
    public playerHealth playerHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth player = collision.gameObject.GetComponent<playerHealth>();
            if (player != null)
            {
                player.health -= 1;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
