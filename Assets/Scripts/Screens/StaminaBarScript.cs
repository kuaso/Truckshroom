using UnityEngine;
using UnityEngine.UI;

// TODO make stamina bar
public class StaminaBarScript : MonoBehaviour
{
    private Stamina _stamina;
    private Image _staminaBarMask;
    
    private void Awake()
    {
        var staminaManager = GameObject.Find("StaminaManager");
        _stamina = staminaManager.GetComponent<Stamina>();
        _staminaBarMask = GetComponent<Image>();
        Debug.Log(_stamina);
        Debug.Log(_staminaBarMask);
    }

    private void FixedUpdate()
    {
        // SharedStamina is out of 100f, while fillAmount is out of 1f
        _staminaBarMask.fillAmount = _stamina.SharedStamina / 100f;
    }
}
