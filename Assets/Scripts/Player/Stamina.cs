using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float rateOfDecreasePercent = 2f;
    [SerializeField] private float rateOfIncreasePercent = 5f;

    private readonly bool[] _canRegenerate = { true, true };

    public float SharedStamina { get; private set; } = 100f;
    public bool HasStamina => SharedStamina > 0f;

    public void TickSharedStamina(int playerNumber)
    {
        _canRegenerate[playerNumber] = false;
        SharedStamina -= rateOfDecreasePercent;
    }

    public void RequestStaminaRegeneration(int playerNumber) => _canRegenerate[playerNumber] = true;

    public void FixedUpdate()
    {
        if (!_canRegenerate[0] || !_canRegenerate[1]) return;
        if (SharedStamina < 100f)
        {
            SharedStamina += rateOfIncreasePercent;
        }
        else if (SharedStamina + rateOfIncreasePercent > 100f)
        {
            SharedStamina = 100f;
        }
    }
}