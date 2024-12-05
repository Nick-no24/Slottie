using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ronin : Enemy
{
    [SerializeField] private Vector2 velocity; // ความเร็วการเคลื่อนที่
    [SerializeField] private Transform[] movePoints; // จุดที่ศัตรูจะเดินถึง
   // [SerializeField] private Rigidbody2D rb; // Rigidbody ของศัตรู

    private void Start()
    {
        if (movePoints.Length < 2)
        {
            Debug.LogError("กรุณาเพิ่ม Move Points อย่างน้อย 2 จุด");
            return;
        }

        // ตรวจสอบ Rigidbody
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        Behavior();
    }

    private void Behavior()
    {
        // เคลื่อนที่ศัตรู
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        // เปลี่ยนทิศทางเมื่อถึงจุดที่กำหนด
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
        // เปลี่ยนทิศทางการเดิน
        velocity *= -1;

        // พลิกตัวละครในแกน X
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
