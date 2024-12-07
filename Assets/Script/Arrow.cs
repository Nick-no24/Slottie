using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    private Vector2 direction;
    public float speed = 10f;
    public float lifetime = 3f; // เวลาที่ลูกธนูจะถูกทำลายหลังจากยิงออกไป

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        // ตั้งเวลาให้ลูกธนูถูกทำลายหลังจาก 3 วินาที
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // เคลื่อนที่ลูกธนูไปทางซ้าย
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    // ฟังก์ชัน Move (ยังคงใช้งานได้ตามเดิม)
    public override void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    // ฟังก์ชันเมื่อชนกับผู้เล่น
    public override void OnHitWith(Character character)
    {
        if (character is Player)
        {
            character.TakeDamage(this.Damage);
            Destroy(gameObject); // ทำลายลูกธนูเมื่อชนกับผู้เล่น
        }
    }

    // ฟังก์ชันเมื่อชนกับสิ่งกีดขวาง
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าชนกับผู้เล่นหรือไม่
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Character>();
            if (player != null)
            {
                // ลดเลือดผู้เล่น
                player.TakeDamage(2);
            }

            // ทำลายลูกธนูหลังจากชน
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            // กรณีชนสิ่งกีดขวาง
            Destroy(gameObject);
        }
    }
}