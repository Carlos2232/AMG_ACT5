using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject missilePrefab;
    public float spawnInterval = 3f;
    public int missilesPerSpawn = 1;

    private float spawnTimer = 0f;
    private float difficultyTimer = 0f;
    private Transform playerTransform;

    void Start()
    {
        GameObject pObj = GameObject.FindGameObjectWithTag("Player");
        if (pObj != null) playerTransform = pObj.transform;
    }

    void Update()
    {
        if (playerTransform == null)
        {
            GameObject pObj = GameObject.FindGameObjectWithTag("Player");
            if (pObj != null) playerTransform = pObj.transform;
            return;
        }

        spawnTimer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (difficultyTimer >= 10f)
        {
            difficultyTimer = 0f;
            missilesPerSpawn++;
            Debug.Log($"Difficulty Increased! Missiles per wave: {missilesPerSpawn}");
        }

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < missilesPerSpawn; i++)
        {
            float randomXOffset = Random.Range(-18f, 18f);
            float backwardOffset = 25f;

            Vector3 spawnPos = playerTransform.position - (playerTransform.forward * backwardOffset) + (playerTransform.right * randomXOffset);
            spawnPos.y = playerTransform.position.y;

            Instantiate(missilePrefab, spawnPos, Quaternion.identity);
        }
    }
}