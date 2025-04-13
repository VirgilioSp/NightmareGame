using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeController : MonoBehaviour
{
    private GameObject player; // Cambiado de Transform a GameObject
    public float detectionRadius = 5.0f;

    //public float bounceForce = 500f; // Fuerza de rebote al jugador

    private Animator animator;

    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;
    private Sprite previousSprite;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        UpdateCollider();
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) // Verifica que el GameObject del jugador no sea nulo
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < detectionRadius)
            {
                animator.SetBool("angry", true);
            }
            else
            {
                animator.SetBool("angry", false);
            }

            if (spriteRenderer.sprite != previousSprite)
            {
                UpdateCollider();
            }
        }
    }

    void UpdateCollider() 
    { 
    
        previousSprite = spriteRenderer.sprite;

        Destroy(polygonCollider);
        polygonCollider = gameObject.AddComponent<PolygonCollider2D>(); 

        polygonCollider.offset = new Vector2(0 , 0);
        polygonCollider.isTrigger = true;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            /*
            Rigidbody2D rbPlayer = other.GetComponent<Rigidbody2D>();

            Vector2 bounceDirection = (other.transform.position - transform.position).normalized;

            rbPlayer.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
            */
            // Aquí puedes agregar la lógica que deseas ejecutar cuando el jugador entra en el área de detección
            Debug.Log("El jugador ha sido detectado por el enemigo.");

            other.gameObject.GetComponent<playerHealth>().health -= 1;

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
