using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ronin : Enemy
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private Vector3 currentPosition;
    private Transform currentTarget;
    private bool isMovingToB = true;

    private void Start()
    {
        currentPosition = transform.position;
        currentTarget = pointA;
        Behavior();
    }

    private void FixedUpdate()
    {
        Behavior();
    }

    private void Behavior()
    {
        MoveTowardsTarget();
        CheckTargetReached();
    }

    private void MoveTowardsTarget()
    {
        // ค่อยๆเคลื่อนที่ไปยังจุดเป้าหมายด้วย Lerp()
        currentPosition = Vector3.Lerp(currentPosition, currentTarget.position, speed * Time.deltaTime);
        transform.position = currentPosition;
    }

    private void CheckTargetReached()
    {
        // ตรวจสอบว่าถึงจุดเป้าหมายหรือยัง
        if (Vector3.Distance(currentPosition, currentTarget.position) < 0.1f)
        {
            SwitchTarget();
        }
    }

    private void SwitchTarget()
    {
        // สลับจุดเป้าหมายระหว่าง A และ B
        isMovingToB = !isMovingToB;
        currentTarget = isMovingToB ? pointB : pointA;
        FlipCharacter();
    }

    private void FlipCharacter()
    {
        // พลิกตัวละครให้หันไปตามทิศทาง
        Vector3 scale = transform.localScale;
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
