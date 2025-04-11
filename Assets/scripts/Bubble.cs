using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float VelocidadBurbuja;
    [SerializeField] private float Daño;
    [SerializeField] private float RangoMaximo = 10f;

    private Animator animator;
    private Vector3 puntoDeOrigen;

    private bool varSpawn = false;
    private bool varMov = false;
    private bool haColisionado = false;

    private void Start()
    {
        puntoDeOrigen = transform.position;
        animator = GetComponent<Animator>();

        varSpawn = true;
        animator.SetBool("VarSpawn", varSpawn);
    }

    private void Update()
    {
        if (!haColisionado)
        {
            transform.Translate(Vector2.right * VelocidadBurbuja * Time.deltaTime);
        }

        animator.SetBool("VarMov", VelocidadBurbuja > 0f);


        animator.SetBool("VarMov", varMov);
        if (Vector3.Distance(puntoDeOrigen, transform.position) > RangoMaximo)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            haColisionado = true;
            animator.SetBool("VarMov", false);
            animator.speed = 5f;
            animator.SetTrigger("VarPlop");
            Destroy(gameObject, 0.3f);
        }
    }
}

