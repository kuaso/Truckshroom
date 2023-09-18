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
        _staminaBarMask.fillAmount =  _stamina.SharedStamina;
    }
}
