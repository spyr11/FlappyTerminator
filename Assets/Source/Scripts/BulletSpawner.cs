using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    private Pool<Bullet> _pool;

    private void Awake()
    {
        _pool = new Pool<Bullet>(_bulletPrefab, container: transform);
    }

    public void Reset()
    {
        _pool.Reset();
    }

    public void Shoot(int layerMaskValue, Vector3 startPosition, Vector3 direction, float shootForce)
    {
        var bullet = _pool.Get();

        if (bullet == null)
        {
            return;
        }

        bullet.SetLayerValue(layerMaskValue);
        bullet.transform.position = startPosition;
        bullet.Rigidbody2D.velocity = direction * shootForce;
    }
}
