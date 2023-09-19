using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] TMP_Text textComponent;
    [SerializeField] string[] lines;
    [SerializeField] float speed;

    private PlayerInput _playerInput;
    private int index;
    private bool inRange;

    private void OnEnable()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Dialogue.Skip.performed += skip;
        _playerInput.Dialogue.Chat.performed += StartDialogue;
    }

    private void OnDisable()
    {
        _playerInput.Dialogue.Skip.performed -= skip;
        _playerInput.Dialogue.Chat.performed -= StartDialogue;
        _playerInput.Disable();
    }

    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
        inRange = false;
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    void StartDialogue(InputAction.CallbackContext ctx)
    {
        Debug.Log("dialogue should start");
        if (inRange)
        {
            //move the stuff here ltr
        }
        gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    void CompleteText()
    {
        StopAllCoroutines();
        textComponent.text = lines[index].ToString();
    }

    void skip(InputAction.CallbackContext ctx)
    {
        if (gameObject.activeInHierarchy)
        {
            if (lines[index].Equals(textComponent.text))
            {
                NextLine();
            }
            else
            {
                CompleteText();
            }
        }

    }
}
