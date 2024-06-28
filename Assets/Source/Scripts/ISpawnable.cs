using System;
using UnityEngine;

public interface ISpawnable<T> where T : MonoBehaviour
{
    event Action<T> Disabled;

    void Init(int layerValue, Vector3 startPosition, Vector3 direction, float shootForce);
}