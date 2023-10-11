using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    private const float RateOfDecreasePercent = 0.4f;
    private const float RateOfIncreasePercent = 5f;

    private const float MaxStamina = 100f;
    private bool canRegenerate = false;
    private float SharedStamina { get; set; } = MaxStamina;

    // This automatically updates, whew! 
    public bool HasStamina => SharedStamina > 0f;
    public delegate void OnStaminaChanged(float newStamina, float maxStamina = MaxStamina);
    public static event OnStaminaChanged StaminaChanged;
    public delegate void OnStaminaFullyRecharged();
    public static event OnStaminaFullyRecharged StaminaFullyRecharged;
    
    public void TickSharedStamina()
    {
        SharedStamina -= RateOfDecreasePercent;
        StaminaChanged?.Invoke(SharedStamina);
    }

    public void Regenerate()
    {
        canRegenerate = true;
        StartCoroutine(RecoverStamina());
    }

    private IEnumerator RecoverStamina()
    {
        while (canRegenerate)
        {
            // We don't want to invoke the event if the stamina is already full
            if (SharedStamina >= MaxStamina) yield return null;
            SharedStamina = Mathf.Min(SharedStamina + RateOfIncreasePercent, 100f);
            StaminaChanged?.Invoke(SharedStamina);
            if (SharedStamina < MaxStamina) yield return null;
            StaminaFullyRecharged?.Invoke();
        }
    }

    public void StopRegenerate()
    {
        Debug.Log("hi");
        canRegenerate = false;
    }
}