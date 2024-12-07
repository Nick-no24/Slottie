using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acher : Character, IShootable
{
    [SerializeField] private float attackRange;
    public Player player;
    [field: SerializeField] public GameObject Arrows { get; set; }
    [field: SerializeField] public Transform SpawnPoint { get; set; }

    [field: SerializeField] public float ArrowSpawnTime { get; set; }
    public float BulletTimer { get; set; }
    private void Update()
    {
        BulletTimer -= Time.deltaTime;

        Behavior();
        if (BulletTimer < 0f)
        {
            BulletTimer = ArrowSpawnTime;
        }
        Destroy();

    }
    public void Behavior()
    {
        Vector2 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;
        if (distance < attackRange)
        {
            Shoot();

        }


    }
    public void Shoot()
    {
        if (BulletTimer <= 0)
        {
            anim.SetTrigger("Shoot");
            GameObject obj = Instantiate(Arrows, SpawnPoint.position, Quaternion.identity);
            Arrow arrow = obj.GetComponent<Arrow>();
            arrow.InnitWeapon(2, 3f, 10f, this);
        }
    }
    public void Start()
    {
        Innit(3);
    }

}