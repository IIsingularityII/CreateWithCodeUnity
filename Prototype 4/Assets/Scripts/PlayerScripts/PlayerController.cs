using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private float _powerUpStrength = 10.0f;

    private GameObject _focalPoint;
    [SerializeField] private GameObject _powerUpIndicator;
    private Vector3 _offset = new Vector3(0, -0.5f, 0);

    private Rigidbody _playerRb;

    private bool _hasPowerUp = false;
    private PowerUpType _powerUpType;

    public GameObject bulletPrefab;
    private GameObject _bullet;

    private float _explosionForce = 100.0f;
    private float _explosionRadius = 40.0f;
    private float _smashSpeed = 20.0f;
    private float _hangTime = 0.5f;
    private bool _smashing = false;
    private float _floorY;

    private enum PowerUpType
    {
        none = 0,
        push = 1,
        shoot = 2,
        smash = 3,
    }
    void Start()
    {
        _focalPoint = FindObjectOfType<RotateCamera>().gameObject;
        _playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        _playerRb.AddForce(_focalPoint.transform.forward * _speed * forwardInput);
        _powerUpIndicator.transform.position = transform.position + _offset;

        if (Input.GetKeyDown(KeyCode.Space) && (_powerUpType == PowerUpType.shoot) && _hasPowerUp)
        {
            Shoot();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && (_powerUpType == PowerUpType.smash) && _hasPowerUp && _smashing == false)
        {
            _smashing = true;
            StartCoroutine(Smash());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUpPush"))
        {
            PowerUpTrigger(PowerUpType.push);

        }
        else if (other.CompareTag("PowerUpShoot"))
        {
            PowerUpTrigger(PowerUpType.shoot);
        }
        else if (other.CompareTag("PowerUpHopAndSmash"))
        {
            PowerUpTrigger(PowerUpType.smash);
        }
        Destroy(other.gameObject);
    }
    private void PowerUpTrigger(PowerUpType type)
    {
        _hasPowerUp = true;
        _powerUpType = type;
        _powerUpIndicator.SetActive(true);
        StartCoroutine(PowerUpCountdownRoutine());
    }
    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        _hasPowerUp = false;
        _powerUpType = PowerUpType.none;
        _powerUpIndicator.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _hasPowerUp)
        {
            if (_powerUpType == PowerUpType.push)
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

                enemyRigidbody.AddForce(awayFromPlayer * _powerUpStrength, ForceMode.Impulse);
            }

        }
    }
    private void Shoot()
    {
        foreach (var enemy in FindObjectsOfType<EnemyBehaviour>())
        {
            _bullet = Instantiate(bulletPrefab, transform.position + Vector3.up, Quaternion.identity);
            _bullet.GetComponent<BulletBehaviour>().Fire(enemy.transform);
        }
    }
    
    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<EnemyBehaviour>();

        _floorY = transform.position.y;

        float jumpTime = _hangTime + Time.time;
        while(Time.time < jumpTime)
        {
            _playerRb.velocity = new Vector2(_playerRb.velocity.x, _smashSpeed);
            yield return null;
        }
        while(transform.position.y > _floorY)
        {
            _playerRb.velocity = new Vector2(_playerRb.velocity.x, -_smashSpeed * 2);
            yield return null;
        }
        for(int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }
        _smashing = false;
    }
}
