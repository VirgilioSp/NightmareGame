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
    private bool ViewDerecha = true;
    private bool Grounded; //true si estamos en el suelo y false si no

    public bool rayRed;
    public bool rayYellow;

    public float gravityScaleInAir = 3f; 
    public float gravityScaleNormal = 1f; 



    // Start is called before the first frame update
    void Start()
    {

        Rigidbody2D = GetComponent<Rigidbody2D>(); //inserta el rigidBody2d del objeto player en el codigo para utilizarlo
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Grounded)
        {
            horizontal = Input.GetAxisRaw("Horizontal"); 

        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal") * 0.5f;
        }
        if (horizontal < 0.0f)
        {

            transform.localScale = new Vector3(-1f, 1f, 1.0f);

        }
        else if (horizontal > 0.0f) {

            transform.localScale = new Vector3(1f, 1f, 1.0f);

        }

        animator.SetBool("running", horizontal != 0.0f);

        if (Physics2D.Raycast(transform.position, Vector3.down, 5f) || Physics2D.Raycast(transform.position, new Vector3(-1, -1, 0), 5f))
        {
            Grounded = true;
            animator.SetBool("jumping", Grounded);
            Rigidbody2D.gravityScale = gravityScaleNormal; 
        }
        else
        {
            Grounded = false;
            animator.SetBool("jumping", Grounded);
            Rigidbody2D.gravityScale = gravityScaleInAir;
        }

        Debug.DrawRay(transform.position, Vector3.down * 5f, Color.blue);

        Debug.DrawRay(transform.position, new Vector3(1, -1, 0) * 5f, Color.red);

        Debug.DrawRay(transform.position, new Vector3(-1, -1, 0) * 5f, Color.yellow);

        if (Physics2D.Raycast(transform.position, new Vector3(1, -1, 0), 5f)) 
        { 
        
            rayRed = true;

        }
        else { rayRed = false; }

        if (Physics2D.Raycast(transform.position, new Vector3(-1, -1, 0), 5f))
        {

            rayYellow = true;

        }
        else { rayYellow = false; }

        if (Physics2D.Raycast(transform.position, Vector3.down, 5f)
            || Physics2D.Raycast(transform.position, new Vector3(-1, -1, 0), 5f))
        {
            Grounded = true;
            animator.SetBool("jumping" , Grounded);
        }
        else 
        { 
            Grounded = false;
            animator.SetBool("jumping", Grounded);
        }
        

        if (Input.GetKeyDown(KeyCode.W) && Grounded) 
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal * Speed, Rigidbody2D.velocity.y);
    }

    //funcion de salto
    private void Jump()
    {

        Rigidbody2D.AddForce(Vector2.up * JumpForce);


    }

 


}
