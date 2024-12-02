using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ความเร็วในการเคลื่อนที่
    public float jumpForce = 10f; // แรงกระโดด
    public Rigidbody2D rb; // อ้างอิงถึง Rigidbody2D ของตัวละคร
    public Animator animator; // สำหรับควบคุม Animation

    private Vector2 movement;
    private bool facingRight = true; // สถานะการหันหน้าของตัวละคร (true = หันขวา)
    private int jumpCount = 0; // นับจำนวนครั้งที่กระโดด
    public int maxJumpCount = 2; // จำนวนครั้งสูงสุดที่กระโดดได้ (ตั้งค่าได้ใน Inspector)

    void Update()
    {
        // รับค่า Input การเคลื่อนที่
        movement.x = Input.GetAxisRaw("Horizontal");

      /*  // อัปเดต Animator Parameter
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", Mathf.Abs(movement.x));*/

        // กระโดดเมื่อกด Spacebar และยังไม่เกินจำนวนครั้งที่กำหนด
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // รีเซ็ตความเร็วแนวตั้งก่อนเพิ่มแรงกระโดด
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++; // เพิ่มจำนวนครั้งที่กระโดด
        }

        // ตรวจสอบการหันหน้าของตัวละคร
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // เคลื่อนที่ตัวละครในแนวนอน
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบการชนกับพื้น
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0; // รีเซ็ตจำนวนครั้งที่กระโดดเมื่อสัมผัสพื้น
        }
    }

    private void Flip()
    {
        // เปลี่ยนทิศทางการหันหน้า
        facingRight = !facingRight;

        // สลับค่า Scale ในแกน X
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}