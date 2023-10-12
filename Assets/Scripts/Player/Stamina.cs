using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float rateOfDecreasePercent = 0.4f;
    [SerializeField] private float rateOfIncreasePercent = 5f;

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
        SharedStamina -= rateOfDecreasePercent;
        StaminaChanged?.Invoke(SharedStamina);
    }

    public void Regenerate()
    {
        canRegenerate = true;
        StartCoroutine(RecoverStamina());
    }

    private IEnumerator RecoverStamina()
    {
        while (canRegenerate && SharedStamina < MaxStamina)
        {
            SharedStamina = Mathf.Min(SharedStamina + rateOfIncreasePercent, 100f);
            StaminaChanged?.Invoke(SharedStamina);
            if (SharedStamina >= MaxStamina)
            {
                StaminaFullyRecharged?.Invoke();
            }
            yield return null;
        }
    }

    public void StopRegenerate() => canRegenerate = false;
    
}