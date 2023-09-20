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
        Debug.LogError("TODO: Make stamina bar change colours depending on how much stamina is left");
    }

    private void FixedUpdate()
    {
        /*
         * Explanation of how fillAmount is derived:
         * 1f -: 1f means total fill. Minus a value after this to get rid of used up stamina
         * Math.Abs(_stamina.ShareStamina - 100f): Get the absolute value of stamina used
         * / 100f: Divide by 100 to the amount used out of 1f
         * * 2: Multiply by 2 to double the rate that the stamina bar drains by visually (since it drains from 2 sides)
         */
        _staminaBarMask.fillAmount = 1f - (Mathf.Abs(_stamina.SharedStamina - 100f) / 100f * 2) ;
    }
}
