using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : Weapon
{
    private Vector2 direction;
    
    public float speed = 10f;
    public float lifetime = 3f; 
    

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        Move();
       //transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

   
    public override void Move()
    {
       transform.Translate(Vector2.left * speed * Time.deltaTime);
       
    }

   
    public override void OnHitWith(Character character)
    {
        if (character is Player)
        {
            character.TakeDamage(this.Damage);
            Destroy(gameObject);
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Character>();
            if (player != null)
            {
               
                player.TakeDamage(2);
            }

          
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
           
            Destroy(gameObject);
        }
    }
}