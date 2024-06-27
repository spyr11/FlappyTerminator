using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite _enemySprite;
    [SerializeField] private Sprite _playerSprite;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Set(int valueType)
    {
        int divisor = 2;

        _spriteRenderer.sprite = valueType % divisor == 0 ? _enemySprite : _playerSprite;
    }
}