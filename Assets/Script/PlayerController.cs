using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 10f;
    bool facingRight = true;

    Rigidbody2D r2d;
    Animator anim;

    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700.0f;

    // Use this for initialization
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            r2d.AddForce(new Vector2(0, jumpForce));
        }
    }

    void FixedUpdate()
    {
        // ตรวจจับพื้น
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        // การตั้งค่า vSpeed และ Speed
        anim.SetFloat("vSpeed", r2d.velocity.y);

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        // การเคลื่อนที่
        r2d.velocity = new Vector2(move * maxSpeed, r2d.velocity.y);

        // การกลับด้านตัวละคร
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Debugging Gizmos
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
    /*
     public float maxSpeed = 10f;
     bool facingRight = true;

     Rigidbody2D r2d;
     Animator anim;

     public bool grounded = false;
     public Transform groundCheck;
     float groundRadius = 0.2f;
     public LayerMask whatIsGround;
     public float jumpForce = 700.0f;

     // Use this for initialization
     void Start()
     {
         r2d = GetComponent<Rigidbody2D>();
         anim = GetComponent<Animator>();
     }

     void Update()
     {
         if (grounded && Input.GetKeyDown(KeyCode.Space))
         {
             anim.SetBool("Ground", false);
             r2d.AddForce(new Vector2(0, jumpForce));
         }
     }

     void FixedUpdate()
     {

         grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
         anim.SetBool("Ground", grounded);

         anim.SetFloat("vSpeed", r2d.velocity.y);

         float move = Input.GetAxis("Horizontal");

         anim.SetFloat("Speed", Mathf.Abs(move));

         r2d.velocity = new Vector2(move * maxSpeed, r2d.velocity.y);

         if (move > 0 && !facingRight)
         {
             Flip();
         }    

         else if (move < 0 && facingRight)
         {
             Flip();
         }

     }

     void Flip()
     {
         facingRight = !facingRight;
         Vector3 theScale = transform.localScale;
         theScale.x *= -1;
         transform.localScale = theScale;
     }*/


}