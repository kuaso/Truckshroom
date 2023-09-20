using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChildDialogueScript : ParentDialogueScript
{
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float speed = 0.1f;

    private PlayerInput _playerInput;
    public int Index { private get; set; }

    private void OnEnable()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Dialogue.Skip.performed += Skip;
    }

    private void OnDisable()
    {
        _playerInput.Dialogue.Skip.performed -= Skip;
        _playerInput.Disable();
    }

    private void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
    }

    public IEnumerator TypeLine()
    {
        foreach (var c in lines[Index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    private void NextLine()
    {
        if (Index < lines.Length - 1)
        {
            Index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    private void CompleteText()
    {
        StopAllCoroutines();
        textComponent.text = lines[Index];
    }

    private void Skip(InputAction.CallbackContext ctx)
    {
        if (!gameObject.activeInHierarchy) return;
        if (lines[Index].Equals(textComponent.text))
        {
            NextLine();
        }
        else
        {
            CompleteText();
        }
    }
}