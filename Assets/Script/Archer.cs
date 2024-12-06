using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public Animator animator;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootDelay = 1f;

    // ระบบเลือด
    public int maxHealth = 50;
    private int currentHealth;

    private bool isShooting = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartShooting();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopShooting();
        }
    }

    void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            animator.SetBool("IsShooting", true);
            InvokeRepeating("Shoot", 0f, shootDelay);
        }
    }

    void StopShooting()
    {
        isShooting = false;
        animator.SetBool("IsShooting", false);
        CancelInvoke("Shoot");
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }

    // ฟังก์ชันรับดาเมจ
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"TakeDamage: {damage} HpRemaining: {currentHealth}");

        // ตรวจสอบว่าเลือดหมด
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ฟังก์ชันตาย
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died!");

        // เล่นแอนิเมชันตาย (ถ้ามี)
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // ปิด Collider เพื่อป้องกันการชนซ้ำ
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // ทำลายวัตถุหลังแอนิเมชันจบ (เช่น 1 วินาที)
        Destroy(gameObject, 1f);
    }
    /*
    public Animator animator; 
    public GameObject projectilePrefab; 
    public Transform firePoint; 
    public float shootDelay = 1f; 
    private bool isShooting = false; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            StartShooting();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            StopShooting();
        }
    }

    void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            animator.SetBool("IsShooting", true); 
            InvokeRepeating("Shoot", 0f, shootDelay); 
        }
    }

    void StopShooting()
    {
        isShooting = false;
        animator.SetBool("IsShooting", false); 
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }*/
}