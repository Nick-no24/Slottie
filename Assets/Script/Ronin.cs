using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ronin : Enemy
{
    public Transform pointA; // จุดที่ 1
    public Transform pointB; // จุดที่ 2
    public float speed = 2f; // ความเร็วในการเดิน
    public float stopDistance = 0.1f; // ระยะที่ถือว่า "ถึง" เป้าหมาย
    public int maxHealth = 50; // เลือดสูงสุดของศัตรู

    private Transform targetPoint; // จุดเป้าหมายปัจจุบัน
    private SpriteRenderer spriteRenderer;
    private int currentHealth; // เลือดปัจจุบันของศัตรู

    void Start()
    {
        targetPoint = pointA; // เริ่มต้นที่จุด A
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth; // ตั้งค่าเลือดเริ่มต้น
    }

    void Update()
    {
        // เดินไปยังจุดเป้าหมาย
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // เปลี่ยนทิศทางเมื่อถึงเป้าหมาย
        if (Vector2.Distance(transform.position, targetPoint.position) <= stopDistance)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;

            // พลิก sprite
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // ฟังก์ชันสำหรับรับดาเมจ
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ลดเลือด
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

        // ถ้าเลือดหมด ให้ทำลายศัตรู
        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} has no health left.");
            Die();  // เรียกฟังก์ชัน Die
        }
    }

    // ฟังก์ชันเมื่อศัตรูตาย
    private void Die()
    {
        // เช็คก่อนว่า `currentHealth` เป็น 0 แล้วหรือยัง
        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} has died!");
            Destroy(gameObject); // ทำลาย GameObject
        }
        else
        {
            Debug.Log($"{gameObject.name} is still alive, no destruction.");
        }
    }
    /*
    [SerializeField] private Vector2 velocity;
    [SerializeField] private Transform[] movePoints;

    public void Start()
    {
        Behavior();
    }
    public void Behavior()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        if (rb.position.x <= movePoints[0].position.x && velocity.x < 0)
        {
            FlipCharacter();
           
        }
        else if (rb.position.x >= movePoints[1].position.x && velocity.x > 0)
        {
            FlipCharacter();
        }


    }
    private void FlipCharacter()
    {

        velocity *= -1;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    */



}
