using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IShootable
{
    [field: SerializeField] public GameObject Arrows { get; set; }
    [field: SerializeField] public Transform SpawnPoint { get; set; }

    [field: SerializeField] public float ArrowSpawnTime { get; set; }
    [field: SerializeField] public float BulletTimer { get; set; }

    public void Shoot()
    {
        //Leftclickshoot
        if (Input.GetButtonDown("Fire1") && BulletTimer <= 0)
        {
            GameObject obj = Instantiate(Arrows, SpawnPoint.position, Quaternion.identity);
            Kunai kunai = obj.GetComponent<Kunai>();
            kunai.InnitWeapon(10, 3f, 10f, this);

            BulletTimer = ArrowSpawnTime;

        }

    }

    private void FixedUpdate()
    {
        BulletTimer -= Time.deltaTime;



    }
    public void Onhitwith()
    {

    }

    void Update()
    {
        Shoot();
        Destroy();
    }
    public void Start()
    {
        Innit(20);

    }







}