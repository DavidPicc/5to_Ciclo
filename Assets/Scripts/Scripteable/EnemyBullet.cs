using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Type", menuName = "Enemy bullet")]
public class EnemyBullet : ScriptableObject
{
    public float NumbulletShoot;
    public float bulletDisappear;
    public GameObject shootPoint;
    public GameObject BulletPrefab;
    public float bulletAng;
    public float bulletoffset;
    public float timetoshot;
    public float dmg;
    public float bulletSpeed;
   
   

}
    