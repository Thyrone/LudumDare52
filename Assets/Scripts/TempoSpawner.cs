using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoSpawner : MonoBehaviour
{
    public Transform spawnTransform;
    public Transform earth;
    public List<GameObject> ObjectsToSpawn = new List<GameObject>();

    void Start()
    {
        MusicManager.beatUpdated += CreateEnemy;
        BeatTracker.tempoChanged += CreateEnemyTmp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemyTmp(float beatInterval)
    {
        //Debug.Log("beatInterval"+beatInterval);
    }
    void CreateEnemy()
    {
        //Debug.Log(MusicManager.lastMarkString);
        if (MusicManager.lastMarkString == "Marker A")
        {
            //if (MusicManager.lastBeat == 1)
            if (MusicManager.lastBeat % 2 == 1)
                Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Count)],
                    new Vector3(
                        spawnTransform.position.x,
                        spawnTransform.position.y,
                        spawnTransform.position.z), Quaternion.identity, earth); //.transform.LookAt(earth);
            //Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Count)], spawnTransform);
        }
        /*
        Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Count)],
           new Vector3(
               spawnTransform.position.x + Random.Range(-5, 5)
                   , spawnTransform.position.y + Random.Range(-5, 5),
                   spawnTransform.position.z), Quaternion.identity);*/
    }
}
