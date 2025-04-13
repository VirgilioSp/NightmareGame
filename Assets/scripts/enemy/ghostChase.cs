using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostChase : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 10f;

    private GameObject player;
    private Animator animator;
    private bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Damaged"))
        {
            animator.SetBool("seesPlayer", false);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < detectionRange)
        {
            animator.SetBool("seesPlayer", true);

            // Cambiamos el trigger por el bool 'chasing'
            if (!isChasing)
            {
                isChasing = true;
                animator.SetBool("chasing", true); // Empieza la persecución
            }

            ChasePlayer();
        }
        else
        {
            animator.SetBool("seesPlayer", false);

            // Detenemos la persecución
            if (isChasing)
            {
                isChasing = false;
                animator.SetBool("chasing", false); // Detiene la persecución
            }
        }

        FlipTowardsPlayer();
    }

    void ChasePlayer()
    {
        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, 0).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void FlipTowardsPlayer()
    {
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
    }
}
