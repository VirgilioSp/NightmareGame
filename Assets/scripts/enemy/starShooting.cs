using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class starShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    private float timer;
    private Animator animator;
    private bool firstShot;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        firstShot = false;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Damaged")) return;
        Vector3 scale = transform.localScale;
        if (player.transform.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x); 
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x); 
        }
        transform.localScale = scale;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 15)
        {
            animator.SetBool("seesPlayer", true);

            if (firstShot is false)
            {
                firstShot = true;
            }
            else
            {
                timer += Time.deltaTime;

                if (timer > 1)
                {
                    animator.SetTrigger("attack");
                    timer = 0;
                    shoot();
                }
            }
        }
        else
        {
            animator.SetBool("seesPlayer", false);
            timer = 0;
        }
            
    }

        void shoot()
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }
