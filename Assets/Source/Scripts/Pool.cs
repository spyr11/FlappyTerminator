using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private readonly Action<T> _actionOnRelease;
    private readonly Action<T> _actionOnGet;

    private readonly List<T> _activeElements;
    private readonly Stack<T> _elements;

    private readonly Transform _container;
    private readonly T _prefab;

    public Pool(T prefab, int initialSize = 0, Transform container = null, Action<T> actionOnGet = null, Action<T> actionOnRelease = null)
    {
        _elements = new Stack<T>();
        _activeElements = new List<T>();

        _prefab = prefab;
        _container = container;
        _actionOnGet = actionOnGet;
        _actionOnRelease = actionOnRelease;

        CreatePool(initialSize);
    }

    public T Get()
    {
        T element;

        if (_elements.TryPop(out element) == false)
        {
            element = CreateObject();
        }

        _activeElements.Add(element);

        _actionOnGet?.Invoke(element);

        element.gameObject.SetActive(true);

        return element;
    }

    public void Release(T element)
    {
        _actionOnRelease?.Invoke(element);

        _elements.Push(element);
        _activeElements.Remove(element);

        element.gameObject.SetActive(false);
    }

    public void Reset()
    {
        for (int i = _activeElements.Count - 1; i >= 0; i--)
        {
            Release(_activeElements[i]);
        }
    }

    private void CreatePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            _elements.Push(CreateObject());
        }
    }

    private T CreateObject()
    {
        T newObject = UnityEngine.Object.Instantiate(_prefab);
        newObject.gameObject.SetActive(false);
        newObject.transform.SetParent(_container);

        return newObject;
    }
}
