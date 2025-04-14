using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.U2D;

public class ControladorSlime : MonoBehaviour
{
    private Animator animador;
    private GameObject jugador;

    public float velocidad = 5f;
    public float distanciaDetencion = 2f;
    public float distanciaAtaque = 1.2f;
    public float alturaSalto = 12f;
    public float duracionSalto = 1f;
    public float tiempoRecargaSalto = 2.5f;
    public float distanciaSalto = 5f;

    private float ultimoTiempoSalto;
    private bool estaSaltando = false;
    private bool esSaltoDeAtaque = false;
    private Vector3 posicionInicioSalto;
    private Vector3 posicionDestinoSalto;
    private float temporizadorSalto;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        animador = GetComponent<Animator>();

    }
    void Update()
    {
        if (jugador == null) return;

        // Voltear sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.flipX = (jugador.transform.position.x > transform.position.x);

        float distancia = Vector2.Distance(transform.position, jugador.transform.position);

        // SALTO NORMAL o DE ATAQUE
        if (!estaSaltando && Time.time - ultimoTiempoSalto >= tiempoRecargaSalto)
        {
            if (distancia <= distanciaAtaque)
            {
                // SALTO DE ATAQUE
                IniciarSalto(jugador.transform.position, distanciaSalto * 0.5f, true);
                animador.SetTrigger("SLattack");
            }
            else if (distancia > distanciaDetencion)
            {
                // SALTO NORMAL
                IniciarSalto(jugador.transform.position, distanciaSalto, false);
                animador.SetBool("SLjump", true);
            }

            if (estaSaltando)
            {
                animador.SetBool("SLidle", false);
                ultimoTiempoSalto = Time.time;
            }
        }

        // MOVIMIENTO DEL SALTO
        if (estaSaltando)
        {
            temporizadorSalto += Time.deltaTime;
            float t = temporizadorSalto / duracionSalto;

            if (t >= 1f)
            {
                // Fin del salto
                transform.position = new Vector3(posicionDestinoSalto.x, posicionInicioSalto.y, transform.position.z);
                estaSaltando = false;
                animador.SetBool("SLjump", false);
                StartCoroutine(EsperarAntesDeIdle(0.1f));

                float distanciaAlJugador = Vector2.Distance(transform.position, jugador.transform.position);
                if (esSaltoDeAtaque && distanciaAlJugador <= distanciaAtaque + 0.5f)
                {
                    Debug.Log("El slime golpeó al jugador");
                    // Aquí podrías aplicar daño al jugador
                }
            }
            else
            {
                Vector3 posicion = Vector3.Lerp(posicionInicioSalto, posicionDestinoSalto, t);
                posicion.y += Mathf.Sin(t * Mathf.PI) * alturaSalto;
                transform.position = posicion;
            }
        }
    }

    void IniciarSalto(Vector3 objetivo, float escalaDistancia, bool esAtaque)
    {
        posicionInicioSalto = transform.position;
        Vector3 direccion = (objetivo - transform.position).normalized;
        posicionDestinoSalto = transform.position + direccion * escalaDistancia;
        temporizadorSalto = 0f;
        estaSaltando = true;
        esSaltoDeAtaque = esAtaque;
    }



    IEnumerator EsperarAntesDeIdle(float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);
        animador.SetBool("SLidle", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth player = collision.gameObject.GetComponent<playerHealth>();
            if (player != null)
            {
                player.health -= 1;
                animador.SetTrigger("SLattack");
            }
        }
    }

    // Este método permite recibir daño desde otros scripts

}


