using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float rateOfDecreasePercent = 2f;

    public float SharedStamina { get; private set; } = 100f;
    
    public bool HasStamina => SharedStamina > 0f;

    public void TickSharedStamina() => SharedStamina -= rateOfDecreasePercent;
}