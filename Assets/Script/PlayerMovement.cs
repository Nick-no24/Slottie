using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    public Animator animator;
    public float attackRange = 1f; // ระยะการโจมตี
    public int attackDamage = 10; // ค่าดาเมจ
    public Transform attackPoint; // จุดปล่อยการโจมตี
    public LayerMask enemyLayers; // เลเยอร์ของศัตรู

    private Vector2 movement;
    private bool facingRight = true;
    private int jumpCount = 0;
    public int maxJumpCount = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", Mathf.Abs(movement.x));

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }

        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }

        // ตรวจจับคลิกซ้ายเพื่อโจมตี
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void Attack()
    {
        Debug.Log("Click Attack");

        // แสดง Animation การโจมตี
      //*****  animator.SetTrigger("Attack");

        // ตรวจจับศัตรูในระยะโจมตี
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // ทำดาเมจให้ศัตรูที่ถูกโจมตี
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);

            // ตรวจสอบว่า Enemy มีฟังก์ชัน TakeDamage แล้วเรียกใช้งาน
            if (enemy.TryGetComponent(out Enemy enemyScript))
            {
                enemyScript.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}