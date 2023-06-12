using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy7 : MonoBehaviour
{
    Transform _camera;
    Transform player;
    public Transform point;
    public GameObject horWarnings, verWarnings;
    public float enemySpeed;
    public bool horizontal;
    public bool fromRight;
    public bool fromTop;
    float offsetX = 30f;
    float offsetY = 10f;
    public GameObject enemyPrefab;
    public float timeToSpawn;
    public float activationDistance;
    float timer;
    bool enemySpawned = false;
    bool spawnerActive = false;
    void Start()
    {
        _camera = FindObjectOfType<StageMovement>().transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = timeToSpawn;
        if(horizontal)
        {
            fromRight = true;
        }
    }

    public void StartSpawner()
    {
        Vector3 cameraPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        if (horizontal)
        {
            verWarnings.SetActive(false);
            horWarnings.SetActive(true);
            //horWarnings.transform.position = _camera.position + new Vector3(0, point.position.y, 0);
            //horWarnings.transform.parent = _camera;
            horWarnings.transform.position = new Vector3(cameraPos.x, point.position.y, 0);
        }
        else
        {
            horWarnings.SetActive(false);
            verWarnings.SetActive(true);
            verWarnings.transform.position = _camera.position + new Vector3(point.position.x, 0, 0);
        }
    }

    void Update()
    {
        if(point.position.x - player.position.x <= activationDistance)
        {
            spawnerActive = true;
        }

        if (spawnerActive)
        {
            StartSpawner();
            if(!enemySpawned)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    SpawnEnemy();
                    enemySpawned = true;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        //if (spawnerActive && !enemySpawned)
        //{
        //    timer -= Time.deltaTime;
        //    if(timer <= 0)
        //    {
        //        //if (!spawnerActive)
        //        //{
        //        //    spawnerActive = true;
        //        //}
        //        //else
        //        //{
        //        //    SpawnEnemy();
        //        //    enemySpawned = true;
        //        //}
        //        SpawnEnemy();
        //        enemySpawned = true;
        //    }
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    public void SpawnEnemy()
    {
        Vector3 distance;
        Vector3 rotation;
        GameObject enemy7 = null;
        if(horizontal)
        {
            distance = new Vector3(offsetX, 0, 0);
            if (fromRight)
            {
                rotation = Vector3.zero;
                enemy7 = Instantiate(enemyPrefab, horWarnings.transform.position + distance, enemyPrefab.transform.rotation, _camera);
            }
            else
            {
                rotation = new Vector3(0, 180, 0);
                enemy7 = Instantiate(enemyPrefab, horWarnings.transform.position - distance, enemyPrefab.transform.rotation, _camera);
            }
        }
        else
        {
            distance = new Vector3(0, offsetY, 0);
            if (fromTop)
            {
                rotation = new Vector3(0, 0, -90);
                enemy7 = Instantiate(enemyPrefab, verWarnings.transform.position + distance, enemyPrefab.transform.rotation, _camera);
            }
            else
            {
                rotation = new Vector3(0, 0, 90);
                enemy7 = Instantiate(enemyPrefab, verWarnings.transform.position - distance, enemyPrefab.transform.rotation, _camera);
            }
        }
        enemy7.transform.Rotate(rotation);
        enemy7.GetComponent<Rigidbody>().AddForce(enemy7.transform.right * enemySpeed, ForceMode.Impulse);
    }
}
