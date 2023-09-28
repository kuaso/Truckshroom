using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    private Image _staminaBarMask;
    
    private void Awake()
    { ;
        _staminaBarMask = GetComponent<Image>();
        Stamina.OnStaminaChanged += UpdateStaminaBar;
    }

    private void UpdateStaminaBar(float newStamina)
    {
        _staminaBarMask.fillAmount = newStamina / 100f;
    }
}
