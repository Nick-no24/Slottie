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

    // ค่าพลังชีวิต
    public int maxHealth = 100;
    private int currentHealth;

    private Vector2 movement;
    private bool facingRight = true;
    private int jumpCount = 0;
    public int maxJumpCount = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // ตั้งค่าพลังชีวิตเริ่มต้น
    }

    void Update()
    {
        // การเคลื่อนที่
        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", Mathf.Abs(movement.x));

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }

        // การเปลี่ยนทิศทาง
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }

        // การโจมตี
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
       //***** animator.SetTrigger("Attack");

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow")) // ตรวจสอบว่าเป็นลูกธนู
        {
            if (collision.TryGetComponent(out Arrow arrow))
            {
                TakeDamage(arrow.damage); // รับค่าดาเมจจากลูกธนู
            }
            Destroy(collision.gameObject); // ลบลูกธนูหลังจากโดน Player
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        // คุณสามารถเพิ่ม Animation หรือเปลี่ยน Scene ได้ที่นี่
        // Destroy(gameObject); // ลบ Player ออกจากเกม
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}