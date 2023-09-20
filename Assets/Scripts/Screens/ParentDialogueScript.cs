using UnityEngine;
using UnityEngine.InputSystem;

public class ParentDialogueScript : MonoBehaviour
{
    private ChildDialogueScript _childDialogueScript;
    private PlayerInput _playerInput;
    private bool _inRange;

    private void OnEnable()
    {
        _childDialogueScript = GetComponentInChildren<ChildDialogueScript>();
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Dialogue.Chat.performed += StartDialogue;
    }

    private void OnDisable()
    {
        _playerInput.Dialogue.Chat.performed -= StartDialogue;
        _playerInput.Disable();
    }

    private void StartDialogue(InputAction.CallbackContext ctx)
    {
        if (!_inRange) return;
        _childDialogueScript.gameObject.SetActive(true);
        _childDialogueScript.Index = 0;
        StartCoroutine(_childDialogueScript.TypeLine());
    }

    // If the player spawns in the trigger zone, the player will still be detected by this function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _inRange = false;
        }
    }
}