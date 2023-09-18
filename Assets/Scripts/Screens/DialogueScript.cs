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
        _playerInput.Disable();
    }

    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
    
    }

    void StartDialogue(InputAction.CallbackContext ctx)
    {
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
        if (lines[index].Equals(textComponent.text)) 
        {
            Debug.Log("same");
            NextLine();
        }
        else
        {
            Debug.Log("diff");
            CompleteText();
        }
    }
}
