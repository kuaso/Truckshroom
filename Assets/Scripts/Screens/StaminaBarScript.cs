using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    private Stamina _stamina;
    private Image _staminaBarMask;
    
    private void Awake()
    {
        _stamina = GetComponentInParent<Stamina>();
        _staminaBarMask = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        _staminaBarMask.fillAmount = _stamina.SharedStamina / 100f;
    }
}
