using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    Transform SpawnPoint { get; set; }


    GameObject Arrows { get; set; }

    float ArrowSpawnTime { get; set; }
    float BulletTimer { get; set; }
    void Shoot();
}