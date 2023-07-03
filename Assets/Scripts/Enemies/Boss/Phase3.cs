using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3 : MonoBehaviour
{
    public float timer;
    public float maxTimer;
    public List<GameObject> attackObjects;
    public Collider area2;
    public GameObject[] attackPrefabs;
    public List<AudioClip> attackSFX;
    public AudioSource audioManager;

    void Start()
    {
        area2 = GameObject.Find("Area2").GetComponent<Collider>();
        attackObjects = new List<GameObject>();
        foreach (GameObject attackPrefab in attackPrefabs)
        {
            attackObjects.Add(attackPrefab);
        }
    }

    void Update()
    {
        ActivatePhase3();
    }

    void ActivatePhase3()
    {
        Vector3 randomPos = new Vector3(Random.Range(area2.bounds.min.x, area2.bounds.max.x), Random.Range(area2.bounds.min.y, area2.bounds.max.y), area2.transform.position.z);
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            int randomIndex = Random.Range(0, attackObjects.Count);
            GameObject obj = Instantiate(attackObjects[randomIndex]);
            obj.transform.position = randomPos;
            timer = 0;
        }
    }
    void PlayRandomAudioClip()
    {
        int randomIndex = Random.Range(0, attackSFX.Count);

        AudioClip randomClip = attackSFX[randomIndex];

        audioManager.PlayOneShot(randomClip);
    }
}
