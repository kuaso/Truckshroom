using UnityEngine;

public class Stamina : MonoBehaviour
{
    private const float RateOfDecreasePercent = 0.4f;
    private const float RateOfIncreasePercent = 5f;


    private const float MaxStamina = 100f;
    private static float SharedStamina { get; set; } = MaxStamina;

    // This automatically updates, whew! 
    public static bool HasStamina => SharedStamina > 0f;
    public delegate void OnStaminaChanged(float newStamina, float maxStamina = MaxStamina);
    public static event OnStaminaChanged StaminaChanged;
    public delegate void OnStaminaFullyRecharged();
    public static event OnStaminaFullyRecharged StaminaFullyRecharged;
    
    public static void TickSharedStamina()
    {
        SharedStamina -= RateOfDecreasePercent;
        StaminaChanged?.Invoke(SharedStamina);
    }

    public static void Regenerate() // TODO CONVERT TO COROUTINE THAT WE CAN CANCEL THORUGH #StopRegenerate
    {
        // We don't want to invoke the event if the stamina is already full
        if (SharedStamina >= MaxStamina) return;
        SharedStamina = Mathf.Min(SharedStamina + RateOfIncreasePercent, 100f);
        StaminaChanged?.Invoke(SharedStamina);
        if (SharedStamina < MaxStamina) return;
        StaminaFullyRecharged?.Invoke();
    }
    
    public static void StopRegenerate()
    {
        // TODO IMPLEMENT
    }
}