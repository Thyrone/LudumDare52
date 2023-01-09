using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoSpawner : MonoBehaviour
{
    public Transform spawnTransform;
    public Transform earth;
    public List<GameObject> ObjectsToSpawn = new List<GameObject>();

    bool gameStarted = false;
    void Start()
    {
        MusicManager.beatUpdated += CreateEnemy;
        BeatTracker.tempoChanged += CreateEnemyTmp;
    }
    private void OnDestroy()
    {
        MusicManager.beatUpdated -= CreateEnemy;
        BeatTracker.tempoChanged -= CreateEnemyTmp;
    }

    void CreateEnemyTmp(float beatInterval)
    {
        //Debug.Log("beatInterval"+beatInterval);
    }
    void CreateEnemy()
    {
        if (MusicManager.lastMarkString == "Start")
            gameStarted = true;
        //Debug.Log(MusicManager.lastMarkString);
        if (gameStarted)
        {
            if (MusicManager.lastBeat == 1 || MusicManager.lastBeat == 4)
            {
                Debug.Log("yo");
                Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Count)],
        new Vector3(
            spawnTransform.position.x,
            spawnTransform.position.y,
            spawnTransform.position.z), Quaternion.identity, earth);
            }
        }
        // if (MusicManager.lastBeat % 2 == 1)
        //if (MusicManager.lastBeat % 2 == 1)
        //.transform.LookAt(earth);
        //Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Count)], spawnTransform);

        /*
        Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Count)],
           new Vector3(
               spawnTransform.position.x + Random.Range(-5, 5)
                   , spawnTransform.position.y + Random.Range(-5, 5),
                   spawnTransform.position.z), Quaternion.identity);*/
    }
}
