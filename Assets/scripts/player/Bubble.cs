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

    private float direccion = 1f;

    public void SetDireccion(float nuevaDireccion)
    {
        direccion = nuevaDireccion;
        
        Vector3 escalaActual = transform.localScale;
        escalaActual.x = Mathf.Abs(escalaActual.x) * direccion;
        transform.localScale = escalaActual;
    }

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
            transform.Translate(Vector2.right * VelocidadBurbuja * direccion * Time.deltaTime);

        }

        animator.SetBool("VarMov", VelocidadBurbuja > 0f);


        animator.SetBool("VarMov", varMov);
        if (Vector3.Distance(puntoDeOrigen, transform.position) > RangoMaximo)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<enemyHealth>(out enemyHealth enemyComponent))
        {
            enemyComponent.TakeDamage(1);
            haColisionado = true;
            animator.SetBool("VarMov", false);
            animator.speed = 5f;
            animator.SetTrigger("VarPlop");
            Destroy(gameObject, 0.3f);
        }
        
        else if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); 
            Destroy(gameObject);          
        }
        else
        {
            Destroy(gameObject); 
        }
    }

}

