using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
        set

        {
            damage = value;

        }
    }
    private float speed;
    public float Speed
    {
        get
        {
            return speed;
        }
        set

        {
            speed = value;

        }
    }
    private float lifetime;
    public float LifeTime
    {
        get
        {
            return lifetime;
        }
        set

        {
            lifetime = value;

        }
    }
    protected IShootable shooter;

    public void InnitWeapon(int _damage,float _lifetime,float _speed, IShootable _shooter)
    {
        Damage = _damage;
        shooter = _shooter;
        Speed = _speed;
        lifetime = _lifetime;

    }

    public abstract void OnHitWith(Character character);

    public abstract void Move();

    public int GetShootDirection()
    {
        float shootDir = shooter.SpawnPoint.position.x - shooter.SpawnPoint.parent.position.x;
        if (shootDir < 0)
            return -1; 
        else return 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHitWith(collision.GetComponent<Character>());
        Destroy(this.gameObject, 3f);
    }
}