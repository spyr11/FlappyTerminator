using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorRandomer : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake() => _renderer = GetComponent<SpriteRenderer>();

    private void OnEnable() => Change();

    public void Change() => _renderer.color = Random.ColorHSV();
}