using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ronin : Enemy
{
    public HealthBar healthBar;
    public Transform pointA; // จุดที่ 1
    public Transform pointB; // จุดที่ 2
    public float speed = 2f; // ความเร็วในการเดิน
    public float stopDistance = 0.1f; // ระยะที่ถือว่า "ถึง" เป้าหมาย
    public float attackRange = 1f; // ระยะที่ศัตรูสามารถโจมตีได้
    public int maxHealth = 50; // เลือดสูงสุดของศัตรู
    public int attackDamage = 10; // ดาเมจที่สร้างต่อ Player
    public float attackCooldown = 1f; // เวลา cooldown ระหว่างการโจมตี

    public Animator animator; // ตัวควบคุม Animation

    private Transform targetPoint; // จุดเป้าหมายปัจจุบัน
    private SpriteRenderer spriteRenderer;
    private int currentHealth; // เลือดปัจจุบันของศัตรู
    private Transform player; // อ้างอิง Player
    private float lastAttackTime; // เวลาโจมตีครั้งล่าสุด

    void Start()
    {
        targetPoint = pointA; // เริ่มต้นที่จุด A
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth; // ตั้งค่าเลือดเริ่มต้น
        player = GameObject.FindGameObjectWithTag("Player").transform; // ค้นหา Player
    }

    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // อยู่ในระยะโจมตี
            Attack();
        }
        else
        {
            // เดินไปตามเส้นทาง
            Patrol();
        }
    }

    private void Patrol()
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

    private void Attack()
    {
        // ตรวจสอบ cooldown
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // หันหน้าไปทาง Player
            FacePlayer();

            // เล่น Animation การโจมตี
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            Debug.Log("Enemy attacks!");

            // ตรวจสอบว่า Player มีฟังก์ชัน TakeDamage แล้วเรียกใช้
            if (player.TryGetComponent(out PlayerMovement playerScript))
            {
                playerScript.TakeDamage(attackDamage);
            }
        }
    }

    private void FacePlayer()
    {
        // เช็คตำแหน่ง Player และพลิก sprite
        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false; // หันไปทางขวา
        }
        else
        {
            spriteRenderer.flipX = true; // หันไปทางซ้าย
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
            Die(); // เรียกฟังก์ชัน Die
        }
    }

    // ฟังก์ชันเมื่อศัตรูตาย
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject); // ทำลาย GameObject
    }

    private void OnDrawGizmosSelected()
    {
        // แสดงระยะโจมตี
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
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
