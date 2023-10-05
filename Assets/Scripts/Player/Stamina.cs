using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float rateOfDecreasePercent = 0.4f;
    [SerializeField] private float rateOfIncreasePercent = 5f;

    private readonly bool[] _canRegenerate = { false, false };
    
    private const float MaxStamina = 100f;
    private float sharedStamina = MaxStamina;

    // This automatically updates, whew! 
    public bool HasStamina => sharedStamina > 0f;

    public delegate void OnStaminaChanged(float newStamina, float maxStamina = MaxStamina);
    public static event OnStaminaChanged StaminaChanged;
    public delegate void OnStaminaFullyRecharged();
    public static event OnStaminaFullyRecharged StaminaFullyRecharged;
    
    public void TickSharedStamina()
    {
        sharedStamina -= rateOfDecreasePercent;
        StaminaChanged?.Invoke(sharedStamina);
    }

    public void RequestStaminaRegeneration(int playerNumber) => _canRegenerate[playerNumber] = true;
    public void StopStaminaRegeneration(int playerNumber) => _canRegenerate[playerNumber] = false;

    public void FixedUpdate()
    {
        if (!_canRegenerate[0] || !_canRegenerate[1]) return;
        if (sharedStamina >= 100f)
        {
            StaminaFullyRecharged?.Invoke();
            return;
        }
        sharedStamina = Mathf.Min(sharedStamina + rateOfIncreasePercent, 100f);
        StaminaChanged?.Invoke(sharedStamina);
    }
}