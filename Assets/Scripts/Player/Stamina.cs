using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float rateOfDecreasePercent = 0.4f;
    [SerializeField] private float rateOfIncreasePercent = 5f;

    private readonly bool[] _canRegenerate = { false, false };
    
    public const float MaxStamina = 100f;
    public static float SharedStamina { get; private set; } = MaxStamina;

    // This automatically updates, whew! 
    public bool HasStamina => SharedStamina > 0f;
    public delegate void OnStaminaChanged(float newStamina, float maxStamina = MaxStamina);
    public static event OnStaminaChanged StaminaChanged;
    public delegate void OnStaminaFullyRecharged();
    public static event OnStaminaFullyRecharged StaminaFullyRecharged;
    
    public void TickSharedStamina()
    {
        SharedStamina -= rateOfDecreasePercent;
        StaminaChanged?.Invoke(SharedStamina);
    }

    public void RequestStaminaRegeneration(int playerNumber) => _canRegenerate[playerNumber] = true;
    public void StopStaminaRegeneration(int playerNumber) => _canRegenerate[playerNumber] = false;

    public void FixedUpdate()
    {
        if (!_canRegenerate[0] || !_canRegenerate[1]) return;
        // We don't want to invoke the event if the stamina is already full
        if (SharedStamina >= MaxStamina) return;
        SharedStamina = Mathf.Min(SharedStamina + rateOfIncreasePercent, 100f);
        StaminaChanged?.Invoke(SharedStamina);
        if (SharedStamina < MaxStamina) return;
        StaminaFullyRecharged?.Invoke();
    }
}