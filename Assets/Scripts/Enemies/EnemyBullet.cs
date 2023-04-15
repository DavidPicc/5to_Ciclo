using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Type", menuName = "Enemy Bullet")]
public class EnemyBullet : ScriptableObject
{
    public GameObject bulletPrefab;
    public float damage;
    public float bulletSpeed;
    public float fireRate;
    public float bulletNumber;
    public float bulletDisappear;
    public float bulletAng;
    public float bulletOffset;
    public float anticipation;
}
