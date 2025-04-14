using System.Collections;
using UnityEngine;

public class collectGem : MonoBehaviour
{
    private Animator animator;
    private bool isCollected = false;

    private float timer = 0f;
    private float warningTime = 5f;
    private float explodeTime = 8f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isCollected) return;

        timer += Time.deltaTime;

        if (Mathf.Approximately(timer, warningTime) || (timer >= warningTime && timer < warningTime + Time.deltaTime))
        {
            Debug.Log("Warning Triggered");
            animator.SetTrigger("Warning");
        }

        if (timer >= explodeTime)
        {
            Debug.Log("Explode Triggered");
            animator.SetTrigger("Explode");
            Invoke("deactivateObject", 0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected || !other.CompareTag("Player")) return;

        isCollected = true;
        animator.SetTrigger("Collect");
        Debug.Log("Collected!");

        CancelInvoke(); // just in case!
        Invoke("deactivateObject", 0.5f);
    }

    void deactivateObject()
    {
        gameObject.SetActive(false);
    }
}
