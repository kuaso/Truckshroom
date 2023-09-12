using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class StaminaDisplayer : MonoBehaviour
{

    private TextMeshProUGUI _tmp;
    private GameObject _staminaManager;
    private Stamina _stamina;
    
    void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _staminaManager = GameObject.Find("StaminaManager");
        _stamina = _staminaManager.GetComponent<Stamina>();
    }

    // Update is called once per frame
    // Does not need to be called every frame cause stamina amount doesn't matter if the frame doesn't change
    void Update() => _tmp.text = Math.Round(Math.Max(_stamina.SharedStamina, 0f), 2).ToString(CultureInfo.CurrentCulture);
    
}
