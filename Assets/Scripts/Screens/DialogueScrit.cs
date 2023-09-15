using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueScrit : MonoBehaviour
{
    public TextMeshPro textComponent;
    public string[] lines;
    public float speed;

    private int index;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartDialogue()
    {
        index = 0;
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(speed);
        }
    }
}
