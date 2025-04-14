using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemitaColeccionar : MonoBehaviour
{
    private Animator gemAnimator;
    private bool collected = false;

    void Start()
    {
        gemAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected == true)
        {
            return;
        } 

        if (other.CompareTag("Player"))
        {
            collected = true;
            gemAnimator.SetTrigger("Collect"); // Trigger the Collect animation
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f); // Adjust to match the animation
        gameObject.SetActive(false);
    }
}
