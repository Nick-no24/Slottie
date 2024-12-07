using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Kunai : Weapon
{
    [SerializeField] private float speed;

    private void Start()
    {
        Damage = 10;
        speed = 10.0f * GetShootDirection(); // กำหนดทิศทางการเคลื่อนที่
    }

    private void FixedUpdate()
    {
        Move(); // เรียกใช้งานการเคลื่อนที่ในทุกๆ FixedUpdate
    }

    public override void Move()
    {
        // เคลื่อนที่ของ Kunai
        float newX = transform.position.x + speed * Time.deltaTime;
        float newY = transform.position.y;
        Vector2 newPosition = new Vector2(newX, newY);
        transform.position = newPosition;
    }

    public override void OnHitWith(Character character)
    {
        if (character is Enemy)
        {
            // หากชนกับศัตรู ให้ทำลาย Kunai และทำดาเมจ
            character.TakeDamage(this.Damage);
            Destroy(gameObject); // ทำลาย Kunai
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าชนกับผู้เล่น
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            // ลดพลังชีวิตผู้เล่น และทำลาย Kunai
            player.TakeDamage(this.Damage);
            Destroy(gameObject);
        }

        // ทำลาย Kunai เมื่อชนกับสิ่งกีดขวาง (Obstacle)
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        // ทำลาย Kunai เมื่อชนกับศัตรู
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); // ทำลาย Kunai
        }
    }



}