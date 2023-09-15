using UnityEngine;
using UnityEngine.UI;

// TODO make stamina bar
public class StaminaBarScript : MonoBehaviour
{
    private Stamina _stamina;
    private Image _staminaBarMask;
    
    private void Awake()
    {
        _stamina = GetComponent<Stamina>();
        _staminaBarMask = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        _staminaBarMask.fillAmount = 
        
            _stamina.SharedStamina;
    }
}
