using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toothController : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    private float timer;
    private Animator animator;
    private bool firstShot;

    public float moveSpeed = 2.0f; // Velocidad de movimiento del enemigo
    public float stopDistance = 5.0f; // Distancia m�nima para detenerse antes de atacar
    public float stepDistance = 3.0f; // Distancia m�xima que puede moverse en un paso
    private float distanceMoved = 0.0f; // Distancia recorrida en el paso actual

    private bool isMoving = true; // Controla si el enemigo puede moverse
    private Vector3 previousPosition; // Almacena la posici�n anterior del enemigo

    public float shootDelay = 10f; // Delay p�blico en segundos para el disparo
    private bool canShoot = true; // Controla si el enemigo puede disparar

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        firstShot = false;
        previousPosition = transform.position; // Inicializa la posici�n anterior
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Damaged")) return;

        // Orientar al enemigo hacia el jugador
        Vector3 scale = transform.localScale;
        if (player.transform.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x); // Camina hacia la izquierda (sin reflejo)
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x); // Camina hacia la derecha (reflejo)
        }
        transform.localScale = scale;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        // Movimiento hacia el jugador si est� fuera de la distancia m�nima
        if (distance > stopDistance && isMoving)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float step = moveSpeed * Time.deltaTime;

            // Mover al enemigo y acumular la distancia recorrida
            transform.position += (Vector3)direction * step;
            distanceMoved += step;

            // Activar la animaci�n de caminar
            animator.SetBool("Walking", true);

            // Detener el movimiento si se alcanza la distancia m�xima del paso
            if (distanceMoved >= stepDistance)
            {
                isMoving = false;
                distanceMoved = 0.0f;

                // Reiniciar el movimiento despu�s de un breve retraso
                StartCoroutine(ResumeMovement());
            }
        }
        else
        {
            // Detener la animaci�n de caminar si no se est� moviendo
            animator.SetBool("Walking", false);
        }

        // L�gica de ataque
        if (distance < 15)
        {
            if (canShoot)
            {
                animator.SetBool("Shooting", true);
                StartCoroutine(ShootWithDelay());
            }
        }
        else
        {
            animator.SetBool("Shooting", false);
        }

        // Verificar si la posici�n ha cambiado
        if (transform.position != previousPosition)
        {
            previousPosition = transform.position;
        }
    }

    IEnumerator ShootWithDelay()
    {
        canShoot = false; // Evitar disparos m�ltiples durante el delay
        yield return new WaitForSeconds(shootDelay); // Esperar el tiempo definido en segundos
        shoot(); // Disparar despu�s del delay
        canShoot = true; // Permitir disparar nuevamente
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    IEnumerator ResumeMovement()
    {
        yield return new WaitForSeconds(1.0f); // Espera 1 segundo antes de reanudar el movimiento
        isMoving = true;
    }
}
