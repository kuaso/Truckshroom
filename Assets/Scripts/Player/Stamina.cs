using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float rateOfDecreasePercent = 2f;
    [SerializeField] private float rateOfIncreasePercent = 5f;

    private readonly bool[] _canRegenerate = { false, false };

    public float SharedStamina { get; private set; } = 100f;
    public bool HasStamina => SharedStamina > 0f;

    public void TickSharedStamina()
    {
        SharedStamina -= rateOfDecreasePercent;
    }

    public void RequestStaminaRegeneration(int playerNumber) => _canRegenerate[playerNumber] = true;
    public void StopStaminaRegeneration(int playerNumber) => _canRegenerate[playerNumber] = false;

    public void FixedUpdate()
    {
        if (!_canRegenerate[0] || !_canRegenerate[1]) return;
        SharedStamina = Mathf.Min(SharedStamina + rateOfIncreasePercent, 100f);
    }
}