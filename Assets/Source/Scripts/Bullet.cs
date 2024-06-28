using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteChanger))]
public class Bullet : MonoBehaviour, ISpawnable<Bullet>
{
    private SpriteChanger _spriteChanger;
    private Rigidbody2D _rigidbody2D;
    private int _layerValue;
    private bool _isHit;

    public event Action<Bullet> Disabled;

    private void Awake()
    {
        _spriteChanger = GetComponent<SpriteChanger>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetAction(collision);
    }

    public void Init(int layerValue, Vector3 startPosition, Vector3 direction, float shootForce)
    {
        _layerValue = layerValue;

        transform.position = startPosition;
        _rigidbody2D.velocity = direction * shootForce;

        _spriteChanger.Set(layerValue);
    }

    private void SetAction(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable character)
         && (collision.gameObject.layer != _layerValue))
        {
            character.TakeDamage();
            Disable();
        }
        else if (collision.TryGetComponent(out Wall _) || collision.TryGetComponent(out Bullet _))
        {
            Disable();
        }
    }

    private void Disable()
    {
        if (_isHit)
        {
            return;
        }

        _isHit = true;

        Disabled?.Invoke(this);
    }
}