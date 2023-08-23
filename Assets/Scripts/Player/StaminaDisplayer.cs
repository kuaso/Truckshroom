using System.Collections;
using System.Collections.Generic;
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
    void Update() => _tmp.text = _stamina.SharedStamina.ToString();
    
}
