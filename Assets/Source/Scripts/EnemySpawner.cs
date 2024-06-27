using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private readonly bool _canExpandPool = false;
    private readonly float _offset = 0.5f;

    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _delay;

    private Pool<Enemy> _pool;

    private float _minBoundY;
    private float _maxBoundY;

    private void Awake()
    {
        _minBoundY = _boxCollider.bounds.min.y + _offset;
        _maxBoundY = _boxCollider.bounds.max.y - _offset;

        float rotationY = 180f;

        _enemyPrefab.transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        _pool = new Pool<Enemy>(_enemyPrefab, _enemyCount, transform, _canExpandPool);
    }

    private void OnEnable() => Subscribe(_pool.AllObjects);

    private void OnDisable() => Unscribe(_pool.AllObjects);

    private void Start() => StartCoroutine(StartSpawning(_delay));

    public void Reset() => _pool.Reset();

    private void Subscribe(IReadOnlyList<Enemy> enemies)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Shooted += _bulletSpawner.Shoot;
            enemy.Died += _scoreCounter.Add;
        }
    }

    private void Unscribe(IReadOnlyList<Enemy> enemies)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Shooted -= _bulletSpawner.Shoot;
            enemy.Died -= _scoreCounter.Add;
        }
    }

    private IEnumerator StartSpawning(float delay)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        bool isExecute = true;

        while (isExecute)
        {
            float positionY = Random.Range(_minBoundY, _maxBoundY);

            Vector3 newPosition = new Vector3(transform.position.x, positionY, transform.position.z);

            Spawn(newPosition, _enemyPrefab.transform.right, _enemyPrefab.Speed);

            yield return waitForSeconds;
        }
    }

    private void Spawn(Vector3 startPosition, Vector3 direction, float shootForce)
    {
        var enemy = _pool.Get();

        if (enemy == null)
        {
            return;
        }

        enemy.transform.position = startPosition;

        enemy.Rigidbody2D.velocity = direction * shootForce;
    }
}