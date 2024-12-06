using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IShootable
{
    [field: SerializeField] public GameObject Bullets { get; set; }
    [field: SerializeField] public Transform BulletSpawnPoint { get; set; }

    [field: SerializeField] public float BulletSpawntime { get; set; }
    [field: SerializeField] public float BulletTimer { get; set; }

    public void Shoot()
    {
        //Leftclickshoot
        if (Input.GetButtonDown("Fire1") && BulletTimer <= 0)
        {
            GameObject obj = Instantiate(Bullets, BulletSpawnPoint.position, Quaternion.identity);
            Banana banana = obj.GetComponent<Banana>();
            banana.Innit(10, this);

            BulletTimer = BulletSpawntime;

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
        Innit(100);

    }






}