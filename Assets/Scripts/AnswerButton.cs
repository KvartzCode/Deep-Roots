using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI btnText;


    int nextID;
    Dialogue dialogue;


    public void Instantiate(int nextID, string text, Dialogue dialogue)
    {
        this.nextID = nextID;
        btnText.text = text;
        this.dialogue = dialogue;
    }

    public void OnAnswer()
    {
        dialogue.OnAnswer(nextID);
    }
}
