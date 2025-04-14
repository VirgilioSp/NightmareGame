using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openEnemyChest : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerNear = false;
    private bool isOpen = false;

    public int saludMaxima = 5;
    private int saludActual;

    void Start()
    {
        animator = GetComponent<Animator>();
        saludActual = saludMaxima;
    }

    void Update()
    {
        if (isPlayerNear && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("enemyOpen");
            isOpen = true;
        }
        else if (isPlayerNear && isOpen)
        {
            animator.SetTrigger("enemyAttack");
            isOpen = true;
        }
        else if (!isPlayerNear && isOpen)
        {
            animator.SetTrigger("enemyClose");
            isOpen = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    public void TakeDamage(int daño)
    {
        saludActual -= daño;

        if (saludActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        gameObject.SetActive(false);
    }
}