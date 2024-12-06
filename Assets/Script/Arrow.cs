using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{

    public float speed = 10f; // ความเร็วลูกธนู
    public float lifeTime = 5f; // เวลาที่ลูกธนูจะหายไป
    public int damage = 1; // ความเสียหายที่สร้าง

   

    void Start()
    {
    
        Destroy(gameObject, lifeTime); // ลบลูกธนูหลังจากเวลาที่กำหนด
    }

    void Update()
    {
        Move();
    }

    public override void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime); // เคลื่อนที่ไปข้างหน้า
    }

    public override void OnHitWith(Character character)
    {
        if (character is Player)
        {
            character.TakeDamage(this.Damage);

        }

    }
}
