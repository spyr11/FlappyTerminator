using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    private readonly float _offset = 0.5f;

    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private float _speed;
    [SerializeField] private float _delay;

    private float _minBoundY;
    private float _maxBoundY;

    protected override void Init()
    {
        _minBoundY = _boxCollider.bounds.min.y + _offset;
        _maxBoundY = _boxCollider.bounds.max.y - _offset;
    }

    protected override void StartAction() => StartCoroutine(StartSpawning(_delay));

    protected override void SetActionOnGet(Enemy enemy)
    {
        base.SetActionOnGet(enemy);

        enemy.Died += _scoreCounter.Add;
        enemy.Shooted += _bulletSpawner.Spawn;
    }

    protected override void SetActionOnRelease(Enemy enemy)
    {
        base.SetActionOnRelease(enemy);

        enemy.Died -= _scoreCounter.Add;
        enemy.Shooted -= _bulletSpawner.Spawn;
    }

    private IEnumerator StartSpawning(float delay)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        bool isExecute = true;

        while (isExecute)
        {
            float positionY = Random.Range(_minBoundY, _maxBoundY);

            Vector3 newPosition = new Vector3(transform.position.x, positionY, transform.position.z);

            Spawn(0, newPosition, transform.right, _speed);

            yield return waitForSeconds;
        }
    }
}
