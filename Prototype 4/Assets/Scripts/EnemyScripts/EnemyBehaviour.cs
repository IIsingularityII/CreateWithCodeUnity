using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private EnemyType _enemyType;
    private Rigidbody _enemyRb;
    private GameObject _player;

    private bool _isBoss = false;
    private float _spawnInterval;
    private float _nextSpawn;
    public int miniEnemySpawnCount;
    private Spawner spawner;
    private enum Abilities
    {
        spawnEnemies = 1,
    }
    private enum EnemyType
    {
        Easy = 1,
        Medium = 2,
        Boss = 3,
    }
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerController>().gameObject;
        if (_isBoss) spawner = FindObjectOfType<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        CheckForFall();

        if (_isBoss)
        {
            if(Time.time > _nextSpawn)
            {
                _nextSpawn = Time.time + _spawnInterval;
                spawner.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }
    }
    private void ChasePlayer()
    {
        Vector3 lookDirection = (_player.transform.position - transform.position).normalized;
        if(_enemyType == EnemyType.Easy) _enemyRb.AddForce(lookDirection * _speed);
        else if(_enemyType == EnemyType.Medium) _enemyRb.AddForce(lookDirection * _speed * 1.75f);

    }
    private void CheckForFall()
    {
        if (transform.position.y < -10.0f) Destroy(gameObject);
    }
}
