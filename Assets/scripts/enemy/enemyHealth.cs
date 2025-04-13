using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;
    private Animator animator;

    
    public GameObject pillPickupPrefab;
    [Range(0f, 1f)]
    public float dropChance = 0.5f;

    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

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


        if (pillPickupPrefab != null && Random.value <= dropChance)
        {
            GameObject pill = Instantiate(pillPickupPrefab, transform.position, Quaternion.identity);
            Animator pillAnimator = pill.GetComponent<Animator>();
            if (pillAnimator != null)
            {
                pillAnimator.SetTrigger("Aparecer");
            }
        }


        Destroy(transform.parent.gameObject);
    }
}
