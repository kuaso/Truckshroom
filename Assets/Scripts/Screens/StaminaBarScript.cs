using UnityEngine;

public class StaminaBarScript : MonoBehaviour
{
    private SpriteMask _spriteMask;
    private float _spriteMaskHeight;

    private void Awake()
    {
        _spriteMask = GetComponentInChildren<SpriteMask>();
        _spriteMaskHeight = _spriteMask.GetComponent<Renderer>().bounds.size.y;
        var maskTransform = _spriteMask.transform;
        var pos = maskTransform.localPosition;
        // Set the sprite mask to outside the bounds of the stamina bar
        maskTransform.localPosition = new Vector3(pos.x, _spriteMaskHeight, pos.z);

        Stamina.StaminaChanged += UpdateStaminaBar;
    }

    private void UpdateStaminaBar(float newStamina, float maxStamina)
    {
        var maskTransform = _spriteMask.transform;
        var pos = maskTransform.localPosition;
        // This essentially gets the fill as a percentage of stamina left, and multiplies it by the height of the stamina bar
        maskTransform.localPosition = new Vector3(pos.x, _spriteMaskHeight * newStamina / maxStamina, pos.z);
    }
}