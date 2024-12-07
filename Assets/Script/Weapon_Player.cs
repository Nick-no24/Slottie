using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Kunai : Weapon
{
    [SerializeField] private float speed;

    private void Start()
    {
        Damage = 1;
        speed = 10.0f * GetShootDirection(); 
    }

    private void FixedUpdate()
    {
        Move(); 
    }

    public override void Move()
    {
       
        float newX = transform.position.x + speed * Time.deltaTime;
        float newY = transform.position.y;
        Vector2 newPosition = new Vector2(newX, newY);
        transform.position = newPosition;
    }

    public override void OnHitWith(Character character)
    {
        if (character is Enemy)
        {
            
            character.TakeDamage(this.Damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
           
            enemy.TakeDamage(this.Damage);
            Destroy(gameObject);
        }

       
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

       
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }



}