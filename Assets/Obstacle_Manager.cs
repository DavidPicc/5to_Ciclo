using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclesPrefabs;
    [Range(20.0f, 40.0f)]
    [SerializeField] float offset_X_min, offset_X_max;
    [SerializeField] Transform moveTransform;
    [SerializeField] Transform notMoveTransform;
    Transform lastObstacle_position;
    Vector3 spawnPosition;
    public void SpawnObstacle()
    {
        int randomObstacle = Random.Range(0, obstaclesPrefabs.Length);
        Debug.Log(randomObstacle);
        float randomOffset = Random.Range(offset_X_min, offset_X_max);
        Vector3 offsetVector = new Vector3(randomOffset, 0, 0);
        if(lastObstacle_position == null)
        {
            spawnPosition = moveTransform.position + offsetVector;
        }
        else
        {
            spawnPosition = new Vector3(lastObstacle_position.position.x + offsetVector.x, 0,0);
        }
        var obstacle = Instantiate(obstaclesPrefabs[randomObstacle], spawnPosition + obstaclesPrefabs[randomObstacle].transform.localPosition, Quaternion.identity, notMoveTransform);
        lastObstacle_position = obstacle.transform;
    }
}
