using System;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup _windowGroup;
    [SerializeField] private Button _actionButton;

    public event Action ButtonClicked;

    private void OnEnable() => _actionButton.onClick.AddListener(OnButtonClick);

    private void OnDisable() => _actionButton.onClick.RemoveListener(OnButtonClick);

    public void Open()
    {
        _windowGroup.alpha = 1f;
        _windowGroup.blocksRaycasts = true;

        _actionButton.interactable = true;
    }

    public void Close()
    {
        _windowGroup.alpha = 0f;
        _windowGroup.blocksRaycasts = false;

        _actionButton.interactable = false;
    }

    private void OnButtonClick() => ButtonClicked?.Invoke();
}