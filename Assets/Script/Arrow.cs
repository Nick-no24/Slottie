using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // ความเร็วของลูกธนู
    [SerializeField] private float damage = 20f; // ความเสียหายที่สร้าง
    [SerializeField] private float lifetime = 5f; // ระยะเวลาที่ลูกธนูจะถูกทำลายเอง

    private Vector2 direction;

    // เรียกใช้ตอนเริ่มต้น
    private void Start()
    {
        // กำหนดเวลาให้ลูกธนูถูกทำลายอัตโนมัติ
        Destroy(gameObject, lifetime);
    }

    // ตั้งค่าทิศทางการเคลื่อนที่ของลูกธนู
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized; // กำหนดให้ทิศทางเป็นเวกเตอร์หน่วย
    }

    private void Update()
    {
        // เคลื่อนที่ไปในทิศทางที่กำหนด
        transform.Translate(direction * speed * Time.deltaTime);
    }

 
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าชนกับวัตถุที่มีคอมโพเนนต์ "Player"
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage); // ลดพลังชีวิตผู้เล่น
            Destroy(gameObject); // ทำลายลูกธนู
        }

        // ทำลายลูกธนูเมื่อชนกับสิ่งกีดขวาง (เช่น กำแพง)
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }*/
}
