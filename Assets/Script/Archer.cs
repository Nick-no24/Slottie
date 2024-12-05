using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{

    public Animator animator; // ��ҧ�֧ Animator
    public GameObject projectilePrefab; // Prefab �ͧ�١���
    public Transform firePoint; // �ش����١��٨��͡
    public float shootDelay = 1f; // �������������ҧ�ԧ���Ф���
    private bool isShooting = false; // ��Ǩ�ͺ��ҡ��ѧ�ԧ�������

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ��Ǩ�Ѻ��Ҽ�����������
        {
            StartShooting();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ��Ǩ�Ѻ��Ҽ������͡�ҡ����
        {
            StopShooting();
        }
    }

    void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            animator.SetBool("IsShooting", true); // ��� Animation Shoot
            InvokeRepeating("Shoot", 0f, shootDelay); // �ԧ�ء� shootDelay �Թҷ�
        }
    }

    void StopShooting()
    {
        isShooting = false;
        animator.SetBool("IsShooting", false); // ��Ѻ� Animation Idle
        CancelInvoke("Shoot");
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation); // ���ҧ�١���
        }
    }
}