using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StaminaBarScript : MonoBehaviour
{
    private SpriteRenderer[] _sprites;
    private SpriteMask _spriteMask;
    private float _spriteMaskHeight;

    private void Awake()
    {
        _sprites = GetComponentsInChildren<SpriteRenderer>();
        _spriteMask = GetComponentInChildren<SpriteMask>();
        _spriteMaskHeight = _spriteMask.GetComponent<Renderer>().bounds.size.y;
        var maskTransform = _spriteMask.transform;
        var pos = maskTransform.localPosition;
        // Set the sprite mask to outside the bounds of the stamina bar
        maskTransform.localPosition = new Vector3(pos.x, _spriteMaskHeight, pos.z);

        Stamina.StaminaChanged += UpdateStaminaBar;
        Stamina.StaminaFullyRecharged += HideStaminaBar;
    }
    
    private void OnDestroy()
    {
        Stamina.StaminaChanged -= UpdateStaminaBar;
        Stamina.StaminaFullyRecharged -= HideStaminaBar;
    }

    private void UpdateStaminaBar(float newStamina, float maxStamina)
    {
        foreach (var sprite in _sprites)
        {
            sprite.GameObject().SetActive(true);
        }
        var maskTransform = _spriteMask.transform;
        var pos = maskTransform.localPosition;
        // This essentially gets the fill as a percentage of stamina left, and multiplies it by the height of the stamina bar
        maskTransform.localPosition = new Vector3(pos.x, _spriteMaskHeight * newStamina / maxStamina, pos.z);
    }

    private IEnumerable HideStaminaBar()
    {
        Debug.Log("h");
        yield return new WaitForSeconds(2f);
        if (Stamina.SharedStamina >= Stamina.MaxStamina) yield break;
        foreach (var sprite in _sprites)
        {
            sprite.enabled = false;
            Debug.Log("hi");
        }
    }
}