using System.Collections;
using UnityEngine;

public class collectGem : MonoBehaviour
{
    private Animator animator;
    private bool isCollected = false;
    private bool isOpen = false;
    public openChest scriptChest;

    private float collectDuration = 0.5f;
    private float warningDelay = 0.5f;
    private float explodeDelay = 3f;
    private float timer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isOpen) return;

        timer += Time.deltaTime;

        if (!isCollected && timer >= warningDelay && timer < warningDelay + Time.deltaTime)
        {
            Debug.Log("Warning");
            animator.SetTrigger("Warning");
        }

        if (!isCollected && timer >= warningDelay + explodeDelay)
        {
            Debug.Log("Explode");
            animator.SetTrigger("Explode");
            Invoke("deactivateObject", 0.5f);
            isOpen = false; // stop updating after this
        }

        if (isCollected && timer >= collectDuration)
        {
            Debug.Log("Deactivate after Collect");
            deactivateObject();
            isOpen = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected || !other.CompareTag("Player")) return;

        isCollected = true;
        isOpen = true;
        timer = 0f;

        animator.SetTrigger("Collect");
        Debug.Log("Collect");
        scriptChest.closeChest();
    }

    void deactivateObject()
    {
        gameObject.SetActive(false);
    }
}
