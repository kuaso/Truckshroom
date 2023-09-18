using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueScrit : MonoBehaviour
{
    [SerializeField] TMP_Text textComponent;
    [SerializeField] string[] lines;
    [SerializeField] float speed;

    private int index;
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartDialogue()
    {
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
}
