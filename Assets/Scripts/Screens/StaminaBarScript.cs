using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    private Image _staminaBarMask;
    private SpriteMask _spriteMask;
    private float _spriteMaskHeight;

    private void Awake()
    {
        _staminaBarMask = GetComponent<Image>();
        _spriteMask = GetComponentInChildren<SpriteMask>();
        if (_spriteMask != null)
        {
            _spriteMaskHeight = _spriteMask.GetComponent<Renderer>().bounds.size.y;
            var scale = _spriteMask.transform.localScale;
            _spriteMask.transform.localScale = new Vector3(scale.x, _spriteMaskHeight, scale.z);
        }

        Stamina.StaminaChanged += UpdateStaminaBar;
    }

    private void UpdateStaminaBar(float newStamina)
    {
        if (_staminaBarMask != null)
        {
            _staminaBarMask.fillAmount = newStamina / 100f;
        }
        else if (_spriteMask != null)
        {
            var maskTransform = _spriteMask.transform;
            var scale = maskTransform.localScale;
            maskTransform.localScale = new Vector3(scale.x, (newStamina / 100f) * _spriteMaskHeight, scale.z);
            // Debug.Log((newStamina / 100f) * _spriteMaskHeight);
        }
    }
}