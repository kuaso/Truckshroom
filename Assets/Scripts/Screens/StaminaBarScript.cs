using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    private Image _staminaBarMask;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        ;
        _staminaBarMask = GetComponent<Image>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Stamina.StaminaChanged += UpdateStaminaBar;
    }

    private void UpdateStaminaBar(float newStamina)
    {
        if (_staminaBarMask != null)
        {
            _staminaBarMask.fillAmount = newStamina / 100f;
        }
        else
        {
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b,
                newStamina / 100f);
        }
    }
}