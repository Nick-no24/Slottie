using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int health;
   
    public int Health
    {
        get
        {
            return health;
        }
        set

        {
            health = value;
        }
    }
    
    

    public Animator anim;
    public Rigidbody2D rb;

    public void Init(int _health)
    {
        Health = _health;
    }
    public void Destroy()
    {
        if (IsDead())
        {
            Destroy(this.gameObject);
        }
    }
    public bool IsDead()
    {
        return Health <= 0;
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"TakeDamage :{damage} HpRemainnig : {Health}");
        
    }
    
}
