using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float velocidad_seguimiento = 2f;
    public float offset_y = 1f;
    public Transform objetivo;

    void Update()
    {
        Vector3 nueva_posicion = new Vector3(objetivo.position.x, objetivo.position.y + offset_y, -10f);
        transform.position = Vector3.Slerp(transform.position, nueva_posicion, velocidad_seguimiento * Time.deltaTime);
    }
}
