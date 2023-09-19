using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] TMP_Text textComponent;
    [SerializeField] string[] lines;
    [SerializeField] float speed;

    private PlayerInput _playerInput;
    private int _index;
    private bool _inRange;

    private void OnEnable()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Dialogue.Skip.performed += Skip;
        _playerInput.Dialogue.Chat.performed += StartDialogue;
    }

    private void OnDisable()
    {
        _playerInput.Dialogue.Skip.performed -= Skip;
        _playerInput.Dialogue.Chat.performed -= StartDialogue;
        _playerInput.Disable();
    }

    private void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
        _inRange = false;
    }

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

    private void StartDialogue(InputAction.CallbackContext ctx)
    {
        Debug.Log("dialogue should start");
        if (inRange)
        {
            //move the stuff here ltr
        }
        gameObject.SetActive(true);
        _index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (var c in lines[_index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    private void NextLine()
    {
        if (_index < lines.Length - 1)
        {
            _index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void CompleteText()
    {
        StopAllCoroutines();
        textComponent.text = lines[_index];
    }

    private void Skip(InputAction.CallbackContext ctx)
    {
        if (!gameObject.activeInHierarchy) return;
        if (lines[_index].Equals(textComponent.text))
        {
            NextLine();
        }
        else
        {
            CompleteText();
        }

    }
}
