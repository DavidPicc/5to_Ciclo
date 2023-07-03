using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase4 : MonoBehaviour
{
    public float timer;
    public float maxTimer;
    public List<GameObject> attackObjects;
    public Collider area;
    public GameObject[] attackPrefabs;
    public List<AudioClip> attackSFX;
    public AudioSource audioManager;

    void Start()
    {
        area = GameObject.Find("Area").GetComponent<Collider>();
        attackObjects = new List<GameObject>();
        foreach (GameObject attackPrefab in attackPrefabs)
        {
            attackObjects.Add(attackPrefab);
        }
    }

    void Update()
    {
        ActivatePhase4();
    }

    void ActivatePhase4()
    {
        Vector3 randomPos = new Vector3(Random.Range(area.bounds.min.x, area.bounds.max.x), Random.Range(area.bounds.min.y, area.bounds.max.y), area.transform.position.z);
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
