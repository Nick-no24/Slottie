using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float speed = 10f; // ความเร็วลูกธนู
    public float lifeTime = 5f; // เวลาที่ลูกธนูจะหายไป
    public int damage = 10; // ความเสียหายที่สร้าง

   

    void Start()
    {
        Destroy(gameObject, lifeTime); // ลบลูกธนูหลังจากเวลาที่กำหนด
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime); // เคลื่อนที่ไปข้างหน้า
    }


    void OnHitWith(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ลด HP ผู้เล่น (เพิ่มระบบ HP ตามต้องการ)
            Debug.Log("Player hit!");
            Destroy(gameObject); // ลบลูกธนูเมื่อชน
        }
    }
}
