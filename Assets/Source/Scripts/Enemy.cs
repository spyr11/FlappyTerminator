using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(ColorRandomer))]
public class Enemy : MonoBehaviour, IDamagable
{
    private readonly float _offset = 0.5f;

    [SerializeField, Range(0, 10)] private float _shootDelay;
    [SerializeField, Range(0, 10)] private float _speed;

    private ColorRandomer _colorRandomer;
    private Rigidbody2D _rigidBody2D;
    private float _shootForce = 2f;

    public event Action<int, Vector3, Vector3, float> Shooted;
    public event Action Died;

    public Rigidbody2D Rigidbody2D => _rigidBody2D;
    public float Speed => _speed;

    private void Awake()
    {
        _shootForce += _speed;

        _rigidBody2D = GetComponent<Rigidbody2D>();
        _colorRandomer = GetComponent<ColorRandomer>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartShooting(_shootDelay));
        _colorRandomer.Change();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Wall wall))
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }

    private IEnumerator StartShooting(float shootDelay)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(shootDelay);

        bool isExecute = true;

        yield return new WaitForFixedUpdate();

        while (isExecute)
        {
            Vector3 newPosition = transform.position;

            newPosition.x += transform.right.x + _offset;

            Shooted?.Invoke(gameObject.layer, newPosition, transform.right, _shootForce);

            yield return waitForSeconds;
        }
    }
}
