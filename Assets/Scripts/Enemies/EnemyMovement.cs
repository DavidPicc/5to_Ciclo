using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Movement Type", menuName = "Enemy Movement")]
public class EnemyMovement : ScriptableObject
{
    public float velocity;
    public float rotationSpeed;
    public float xLock;
    public bool followPlayer;
    
}
