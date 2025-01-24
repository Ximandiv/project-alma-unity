using System.Collections;
using System.Collections.Generic;
using Scripts.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    [Header("Boss Settings")]
    public GameObject bossPrefab;         
    public float spawnTimer = 10f;        
    public float spawnDistance = 5f;

    [Header("Player Settings")]
    public Transform player;

    public List<GameObject> enemySpawners;

    [Header("SFX")]
    public AudioClip bossClip;
    public float sfxVolumen;

    public bool bossIsDead;

    private float timer;  
    private bool bossIsActive = false;
    private float remainingTime;    
    private bool isRunning = false; 

    private void Start()
    {
        timer = spawnTimer;
        StartCountdown(spawnTimer);
    }

    private void Update()
    {
        checkDead();

        if (!bossIsActive && !bossIsDead)
        {
            timer -= Time.deltaTime;

            if (isRunning)
            {
                UpdateTimer();
            }

            if (timer <= 0f)
            {
                bossIsActive = true;

                SpawnBoss();
                DestroyObjects();
            }
        }
    }

    private void checkDead() 
    {
        if (bossIsDead) 
        {
            StartCoroutine(PlayerVictory());
        }
    }

    IEnumerator PlayerVictory() 
    {
        yield return new WaitForSeconds(6f);
        GameEvents.Instance.LevelBeaten();
        SceneManager.LoadScene("FerneyHomeWinner");
    }

    private void DestroyObjects()
    {
        foreach (GameObject obj in enemySpawners)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        enemySpawners.Clear();
    }

    private void SpawnBoss()
    {
        if (bossPrefab != null && player != null)
        {
            AudioManager.Instance.PlayMusic(bossClip, true, sfxVolumen);

            Vector3 spawnPosition = CalculateSpawnPosition();

            GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            boss.gameObject.GetComponent<BossBehavior>().startAttack = true;
        }
        else
        {
            Debug.LogWarning("BossPrefab o Player no est� configurado en el BossManager.");
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        Vector3 playerPosition = player.position;
        Vector3 directionToPlayer = (playerPosition - Vector3.zero).normalized;
        Vector3 spawnPosition = Vector3.zero + (directionToPlayer * spawnDistance);
        return spawnPosition;
    }

    public void StartCountdown(float timeInSeconds)
    {
        remainingTime = timeInSeconds;
        isRunning = true;
    }

    public void StopCountdown()
    {
        isRunning = false;
    }

    private void UpdateTimer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime < 0)
            {
                remainingTime = 0;
            }

            GameEvents.Instance.TimerUpdated(remainingTime);
        }
        else
        {
            if (isRunning)
            {
                isRunning = false;
            }
        }
    }
}
