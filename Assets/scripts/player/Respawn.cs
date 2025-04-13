using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float coord_y_caida = -10f;
    public Vector3[] coord_respawn = new Vector3[]
    {
        /* Especifico para lvl 1 !!
         * Coordenadas de cada punto de caida.
         * En este caso, izquierda y derecha del mapa.
         */
        new Vector3(-30f, 30f, 0f),
        new Vector3(76f, 30f, 0f)
    };

    void Update()
    {
        if (transform.position.y < coord_y_caida)
        {
            Vector3 coord_mas_cerca = get_coord_mas_cerca();
            respawn(coord_mas_cerca);
        }
    }

    Vector3 get_coord_mas_cerca()
    {
        Vector3 coord_mas_cerca = Vector3.zero;
        float distancia_minima = Mathf.Infinity;

        foreach (Vector3 point in coord_respawn)
        {
            float distancia = Vector3.Distance(transform.position, point);
            if (distancia < distancia_minima)
            {
                distancia_minima = distancia;
                coord_mas_cerca = point;
            }
        }

        return coord_mas_cerca;
    }

    void respawn(Vector3 posicion)
    {
        transform.position = posicion;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
