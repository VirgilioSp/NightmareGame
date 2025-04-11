using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDisparo : MonoBehaviour
{
    [SerializeField] private Transform ControllerShoot;
    [SerializeField] private GameObject Burbuja;
    [SerializeField] private float tiempoEntreDisparos = 0.3f;

    private Animator animator;
    private float tiempoSiguienteDisparo = 0f;
    private bool disparando = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool estaPresionando = Input.GetButton("Fire1");

        animator.SetBool("isShooting", estaPresionando);

        if (estaPresionando)
        {
            if (Time.time >= tiempoSiguienteDisparo)
            {
                Disparar();
                tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
            }
        }
        else
        {
            tiempoSiguienteDisparo = 0f; 
        }
    }

    private void Disparar()
    {
        StartCoroutine(DisparoConDelay());
    }

    private IEnumerator DisparoConDelay()
    {
        yield return new WaitForSeconds(0.1f); 

        GameObject burbujaInstanciada = Instantiate(Burbuja, ControllerShoot.position, ControllerShoot.rotation);
        float direccion = transform.localScale.x > 0 ? 1f : -1f;
        burbujaInstanciada.GetComponent<Bubble>().SetDireccion(direccion);
    }

}
