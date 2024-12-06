using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acher : Enemy, IShootable
{
    [SerializeField] private float attackRange;
    public Player player;
    [field: SerializeField] public GameObject Bullets { get; set; }
    [field: SerializeField] public Transform BulletSpawnPoint { get; set; }

    [field: SerializeField] public float BulletSpawntime { get; set; }
    public float BulletTimer { get; set; }
    private void Update()
    {
        BulletTimer -= Time.deltaTime;

        Behavior();
        if (BulletTimer < 0f)
        {
            BulletTimer = BulletSpawntime;
        }
        Destroy();

    }
    public override void Behavior()
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
            GameObject obj = Instantiate(Bullets, BulletSpawnPoint.position, Quaternion.identity);
          //  Rock rock = obj.GetComponent<Rock>();
          //  rock.Innit(20, this);
        }
    }
    public void Start()
    {
        Innit(100);
    }

}