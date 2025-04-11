using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount)
    {
        animator.SetTrigger("damaged");
        health -= damageAmount;

        if (health <= 0)
        {
            animator.SetTrigger("death");
            StartCoroutine(DieAfterAnimation());
        }
    }
    private IEnumerator DieAfterAnimation()
    {
        float deathAnimLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimLength);

        Destroy(transform.parent.gameObject);
    }
}
