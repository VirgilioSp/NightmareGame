using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float Speed;
    public float JumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator animator;

    private float horizontal;
    private bool Grounded; //true si estamos en el suelo y false si no

    // Start is called before the first frame update
    void Start()
    {

        Rigidbody2D = GetComponent<Rigidbody2D>(); //inserta el rigidBody2d del objeto player en el codigo para utilizarlo
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); //obtiene datos de la pulsacion del usuario en horizontal

        if (horizontal < 0.0f)
        {

            transform.localScale = new Vector3(0.15f, 0.15f, 1.0f);

        }
        else if (horizontal > 0.0f) {

            transform.localScale = new Vector3(-0.15f, 0.15f, 1.0f);

        }

        animator.SetBool("walking", horizontal != 0.0f); //verifica si esta quieto. Si esta quieto al moverse se reproduce la animacion de walk. Si no lo esta, la animacion walk se detiene.

        Debug.DrawRay(transform.position, Vector3.down * 2f, Color.blue);
        if (Physics2D.Raycast(transform.position, Vector3.down, 2f))
        {
            Grounded = true;
        }
        else 
        { 
            Grounded = false; 
        }
        

        if (Input.GetKeyDown(KeyCode.W) && Grounded) 
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal, Rigidbody2D.velocity.y);
    }

    //funcion de salto
    private void Jump()
    {

        Rigidbody2D.AddForce(Vector2.up * JumpForce);

    }
}
