using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Interactable : MonoBehaviour
{
    [TextArea()]
    public string[] lines;


    private void Start()
    {
        DialogueManager.Instance.AddDialogue(this, lines);
    }

    void Update()
    {
    }

    public void TriggerInteraction()
    {
        DialogueManager.Instance.StartDialogue(this);
        Debug.Log("Interaction Triggered!");
    }
}
