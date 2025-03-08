using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Prefab zombie
    public int maxZombies = 10; // so luong zombie toi da duoc sinh ra
    public float spawnRate = 5f; // thoi gian cho truoc khi sinh ra zombie moi
    public Vector3 spawnCenter; // trung tam khu vuc spawner
    public float spawnWidth = 10f; // chieu rong
    public float spawnHeight = 10f;
    public LayerMask groundLayer; // lop mat dat

    public int currentZombies = 0; // So zombie hien co
    public Queue<GameObject> zombieQueue = new Queue<GameObject>(); // Hang doi Zombie duoc spawn

    void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    private IEnumerator SpawnZombies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            if (currentZombies < maxZombies)
            {
                SpawnZombie();
            }
        }
    }

    private void SpawnZombie()
    {
        Vector3 spawnPoint = GetRandomSpawnPoint();
        GameObject newZombie = Instantiate(zombiePrefab, spawnPoint, Quaternion.identity);

        ZomManager zomManager = newZombie.GetComponent<ZomManager>();
        zomManager.ChangeState(new ZombieSpawnState());

        HealthZombie healthZombie = newZombie.GetComponentInChildren<HealthZombie>();
        if (healthZombie != null)
        {
            Debug.Log("Zombie moi duoc spawn!");
            healthZombie.onDead += OnZombieDeath; // dang ky su kien
        }

        else
        {
            Debug.LogError("Khong tim thay healthzombie tren zombie moi");
        }
        zombieQueue.Enqueue(newZombie);
        currentZombies++;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 spawnPoint;
        int maxAttempts = 10; 
        int attempt = 0;

        do
        {
            spawnPoint = spawnCenter + new Vector3(
                Random.Range(-spawnWidth / 2, spawnWidth / 2),
                0.2f,
                Random.Range(-spawnHeight / 2, spawnHeight / 2)
            );

            
            if (Physics.Raycast(spawnPoint + Vector3.up * 2, Vector3.down, out RaycastHit hit, 5f, groundLayer))
            {
                spawnPoint.y = hit.point.y;
            }

            attempt++;

           
        } while (Physics.CheckSphere(spawnPoint, 1f, groundLayer) && attempt < maxAttempts);

        return spawnPoint;
    }

    private void OnZombieDeath(bool isDead)
    {
        if (isDead)
        {
            currentZombies--;
            StartCoroutine(RespawnZombie());
        }
    }

    private IEnumerator RespawnZombie()
    {
        yield return new WaitForSeconds(15f);

        if (currentZombies < maxZombies)
        {
            SpawnZombie();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(spawnCenter, new Vector3(spawnWidth, 1, spawnHeight));
    }
}
