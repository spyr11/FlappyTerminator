using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
{
    [SerializeField] private T _prefab;

    private Pool<T> _pool;

    private void Awake()
    {
        Init();

        _pool = new Pool<T>(_prefab, container: transform, actionOnGet: SetActionOnGet, actionOnRelease: SetActionOnRelease);
    }

    private void Start()
    {
        StartAction();
    }

    public void Reset() => _pool.Reset();

    public void Spawn(int layerValue, Vector3 startPosition, Vector3 direction, float shootForce)
    {
        T obj = _pool.Get();

        obj.Init(layerValue, startPosition, direction, shootForce);
    }

    protected abstract void Init();

    protected abstract void StartAction();

    protected virtual void SetActionOnGet(T obj) => obj.Disabled += OnDisabled;

    protected virtual void SetActionOnRelease(T obj) => obj.Disabled -= OnDisabled;

    protected T GetObject() => _pool.Get();

    private void OnDisabled(T obj) => _pool.Release(obj);
}
