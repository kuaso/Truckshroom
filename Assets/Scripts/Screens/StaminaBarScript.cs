using System.Collections;
using UnityEngine;

public class StaminaBarScript : MonoBehaviour
{
    private SpriteRenderer[] _sprites;
    private SpriteMask _spriteMask;
    private float _spriteMaskHeight;
    [SerializeField] private float fadeOutDuration = 3f;
    
    // We set this to true by default to ensure that the first time an update is called, the sprites are rendered
    private bool shouldFadeOut = true;

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
        if (shouldFadeOut)
        {
            shouldFadeOut = false;
            RenderSprites();
        }
        var maskTransform = _spriteMask.transform;
        var pos = maskTransform.localPosition;
        // This essentially gets the fill as a percentage of stamina left, and multiplies it by the height of the stamina bar
        maskTransform.localPosition = new Vector3(pos.x, _spriteMaskHeight * newStamina / maxStamina, pos.z);
    }

    private void RenderSprites()
    {
        foreach (var sprite in _sprites)
        {
            var spriteColor = sprite.color;
            sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1);
        }
    }

    private void HideStaminaBar()
    {
        shouldFadeOut = true;
        foreach (var sprite in _sprites)
        {
            StartCoroutine(FadeOutSprite(sprite));
        }
    }

    /**
     * Code taken from https://stackoverflow.com/questions/44933517/fading-in-out-gameobject
     */
    private IEnumerator FadeOutSprite(SpriteRenderer sprite)
    {
        var counter = 0f;
        // Not material color!
        var spriteColor = sprite.color;

        while (counter < fadeOutDuration)
        {
            if (!shouldFadeOut) yield break;
            counter += Time.deltaTime;
            // Fade from 1 to 0 to fade out
            var alpha = Mathf.Lerp(1, 0, counter / fadeOutDuration);
            sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
    }
}