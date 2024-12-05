using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{

    public Animator animator; // อ้างถึง Animator
    public GameObject projectilePrefab; // Prefab ของลูกธนู
    public Transform firePoint; // จุดที่ลูกธนูจะออก
    public float shootDelay = 1f; // ระยะเวลาระหว่างยิงแต่ละครั้ง
    private bool isShooting = false; // ตรวจสอบว่ากำลังยิงหรือไม่

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ตรวจจับว่าผู้เล่นเข้าใกล้
        {
            StartShooting();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ตรวจจับว่าผู้เล่นออกจากระยะ
        {
            StopShooting();
        }
    }

    void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            animator.SetBool("IsShooting", true); // เล่น Animation Shoot
            InvokeRepeating("Shoot", 0f, shootDelay); // ยิงทุกๆ shootDelay วินาที
        }
    }

    void StopShooting()
    {
        isShooting = false;
        animator.SetBool("IsShooting", false); // กลับไป Animation Idle
        CancelInvoke("Shoot");
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation); // สร้างลูกธนู
        }
    }
}