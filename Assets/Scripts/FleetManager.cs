using UnityEngine;


public class FleetManager : MonoBehaviour
{
public GameObject playerPrefab;
public GameObject enemyPrefab;
public int enemyCount = 6;
public float spawnRadius = 120f;


GameObject playerInstance;


void Start()
{
SpawnPlayer();
SpawnEnemies();
}


void SpawnPlayer()
{
Vector3 pos = Vector3.zero;
Quaternion rot = Quaternion.identity;
playerInstance = Instantiate(playerPrefab, pos, rot);
playerInstance.tag = "Player";
}


void SpawnEnemies()
{
for (int i = 0; i < enemyCount; i++)
{
Vector2 r = Random.insideUnitCircle.normalized * Random.Range(spawnRadius * 0.6f, spawnRadius);
Vector3 pos = new Vector3(r.x, 0f, r.y);
Quaternion rot = Quaternion.LookRotation((Vector3.zero - pos).normalized, Vector3.up);
GameObject e = Instantiate(enemyPrefab, pos, rot);
// Optionally randomize stats here
}
}
}