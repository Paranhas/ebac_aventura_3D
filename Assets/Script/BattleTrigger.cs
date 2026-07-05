using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Boss;

public class BattleTrigger : MonoBehaviour
{
    [Header("Inimigos da Sala")]
    [SerializeField] private EnemyBase[] enemies;

    [Header("Boss")]
    [SerializeField] private BossBase boss;

    private bool battleStarted;
    private bool bossSpawned;
    private int enemiesAlive;

    private void OnEnable()
    {
        EnemyBase.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyBase.OnEnemyKilled -= OnEnemyKilled;
    }

    private void Start()
    {
        foreach (EnemyBase enemy in enemies)
        {
            if (enemy != null)
                enemy.gameObject.SetActive(false);
        }

        boss.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (battleStarted)
            return;

        if (other.CompareTag("Player"))
        {
            battleStarted = true;

            enemiesAlive = enemies.Length;

            SpawnEnemies();

            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnEnemyKilled(EnemyBase enemy)
    {
        if (!battleStarted || bossSpawned)
            return;

        enemiesAlive--;

        Debug.Log($"Inimigos restantes: {enemiesAlive}");

        if (enemiesAlive <= 0)
        {
            SpawnBoss();
        }
    }

    private void SpawnEnemies()
    {
        foreach (EnemyBase enemy in enemies)
        {
            if (enemy != null)
                enemy.gameObject.SetActive(true);
        }
    }

    private void SpawnBoss()
    {
        bossSpawned = true;

        boss.gameObject.SetActive(true);

        boss.SwitchState(BossAction.INIT);

        Debug.Log("Boss apareceu!");
    }
}

