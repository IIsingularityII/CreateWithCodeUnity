using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    private Rigidbody _targetRb;
    private GameManager _gameManager;

    private float _minSpeed = 12;
    private float _maxSpeed = 16;
    private float _maxTorque = 10;
    private float xRange = 4;
    private float yPos = -2;

    [SerializeField] private int _pointValue;
    public ParticleSystem ExplosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        _targetRb = GetComponent<Rigidbody>();
        _gameManager = FindObjectOfType<GameManager>();

        _targetRb.AddForce(RandomSpeed(),ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(),ForceMode.Impulse);

        transform.position = RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 RandomSpeed()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }
    private float RandomTorque()
    {
        return Random.Range(-_maxTorque, _maxTorque);
    }
    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), yPos);
    }
    public void DestroyTarget()
    {
        if (_gameManager.IsGameActive)
        {
            Destroy(gameObject);
            Instantiate(ExplosionParticle, transform.position, ExplosionParticle.transform.rotation);
            _gameManager.UpdateScore(_pointValue);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            _gameManager.DecreaseLiveCount();
        }
    }
}
