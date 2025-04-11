using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDisparo : MonoBehaviour
{
    [SerializeField] private Transform ControllerShoot;
    [SerializeField] private GameObject Burbuja;



    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    private void Disparar()
    {
        Instantiate(Burbuja, ControllerShoot.position, ControllerShoot.rotation);
    }
}
