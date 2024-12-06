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
    protected IShootable shooter;

    public void Innit(int _damage, IShootable _shooter)
    {
        Damage = _damage;
        shooter = _shooter;

    }

    public abstract void OnHitWith(Character character);

    public abstract void Move();

    public int GetShootDirection()
    {
        float shootDir = shooter.BulletSpawnPoint.position.x - shooter.BulletSpawnPoint.parent.position.x;
        if (shootDir < 0)
            return -1; //หันขวา
        else return 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHitWith(collision.GetComponent<Character>());
        Destroy(this.gameObject, 3f);
    }
}