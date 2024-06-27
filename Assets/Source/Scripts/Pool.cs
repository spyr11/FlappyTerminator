using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private readonly Transform _container;
    private readonly bool _canExpand;
    private readonly T _prefab;

    private List<T> _objects;

    public IReadOnlyList<T> AllObjects => _objects;

    public Pool(T prefab, int size = 10, Transform container = null, bool canExpand = true)
    {
        _objects = new List<T>();

        _prefab = prefab;
        _container = container;
        _canExpand = canExpand;

        CreatePool(size);
    }

    public T Get()
    {
        if (TryGetObject(out T element))
        {
            return element;
        }

        if (_canExpand)
        {
            return CreateObject(true);
        }

        return null;
    }

    public void Reset()
    {
        foreach (T element in _objects)
        {
            if (_container != null)
            {
                element.transform.position = _container.position;
            }

            element.gameObject.SetActive(false);
        }
    }

    private void CreatePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActive = false)
    {
        T newObject = Object.Instantiate(_prefab);
        newObject.gameObject.SetActive(isActive);
        newObject.transform.SetParent(_container);

        _objects.Add(newObject);

        return newObject;
    }

    private bool TryGetObject(out T obj)
    {
        foreach (T element in _objects)
        {
            if (element.gameObject.activeInHierarchy == false)
            {
                obj = element;
                obj.gameObject.SetActive(true);

                return true;
            }
        }

        obj = null;
        return false;
    }
}
