using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    public float spawnInterval = 3f;
    public float spawnIntervalReductor;

    public float speedMultiplicator;
    float timePassedAlive = 0;

    public Transform[] obstaclesSpawnPosition; // id 0 = Left | id 1 = Middle | id 2 = Right
    public GameObject[] obstaclesPrefab;


    void Start()
    {
        SpawnObstacle(1);
    }

    void Update()
    {
        timePassedAlive += Time.fixedDeltaTime;
        if(spawnInterval>0.5f)
        {
            spawnInterval -= timePassedAlive * spawnIntervalReductor;
        }
    }

    public void SpawnObstacle(int _nbObstacleToSpawn)
    {
        bool[] _spawnPositionPossible = { false, false, false }; // id 0 = Left | id 1 = Middle | id 2 = Right
        for (int i = 0; i<_nbObstacleToSpawn; i++)
        {
            int _spawnPositionId = GetSpawnPosition(_spawnPositionPossible); // Avoid double obstacles on the same spawn point at the same time
            _spawnPositionPossible[_spawnPositionId] = true;
            GameObject _newObstacle = Instantiate(obstaclesPrefab[Random.Range(0, obstaclesPrefab.Length)], obstaclesSpawnPosition[_spawnPositionId]);
            _newObstacle.GetComponent<ObstacleBehaviour>().speed += timePassedAlive * speedMultiplicator;
        }
        StartCoroutine(SpawnCouldown());
    }

    public int GetSpawnPosition(bool[] _spawnPossibilities) // Get a valid spawn position (avoid multiples obstacles spawn on the same position at the same time)
    {
        int _positionId = Random.Range(0, _spawnPossibilities.Length);
        while(_spawnPossibilities[_positionId])
        {
            _positionId = Random.Range(0, _spawnPossibilities.Length);
        }
        return _positionId;
    }

    public IEnumerator SpawnCouldown()
    {
        yield return new WaitForSeconds(spawnInterval);

        if(timePassedAlive > 150) // 100% of spawning 2 obstacles
        {
            SpawnObstacle(2);
        }

        else if(timePassedAlive > 60)
        {

            if(Random.Range(0, 2) == 0) // 50% of spawning 2 obstacles
            {
                SpawnObstacle(2);
            }
            else
            {
                SpawnObstacle(1);
            }
        }

        else // 0% of spawning 2 obstacles
        {
            SpawnObstacle(1);
        }
    }
}
