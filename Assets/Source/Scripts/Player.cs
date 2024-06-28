using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Player : MonoBehaviour, IDamagable
{
    private readonly float _offset = 0.5f;

    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private float _shootForce = 3f;

    private Mover _mover;

    public event Action Died;

    private void Start() => _mover = GetComponent<Mover>();

    private void OnTriggerEnter2D(Collider2D collision) => TakeDamage();

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    public void Reset() => _mover.Reset();

    public void TakeDamage() => Died?.Invoke();

    private void Shoot()
    {
        Vector3 newPosition = transform.position;

        newPosition.x += _offset;

        _bulletSpawner.Spawn(gameObject.layer, newPosition, transform.right, _shootForce);
    }
}