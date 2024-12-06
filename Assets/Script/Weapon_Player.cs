using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Banana : Weapon
{
    [SerializeField] private float speed;

    private void Start()
    {
        Damage = 10;
        speed = 10.0f * GetShootDirection();

    }
    private void FixedUpdate()
    {
        Move();
    }
    public override void Move()
    {
        //s = v*t
        float newX = transform.position.x + speed * Time.deltaTime;
        float newY = transform.position.y;
        Vector2 newPosition = new Vector2(newX, newY);
        transform.position = newPosition;



        // Debug.Log($"Banana moves with constant speed useing Tranform");
    }
    public override void OnHitWith(Character character)
    {

        if (character is Enemy)
            character.TakeDamage(this.Damage);

    }



}