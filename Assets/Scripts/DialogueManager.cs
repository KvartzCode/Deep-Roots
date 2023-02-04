using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum GameState
{
    Game,
    Dialogue,
    Paused,
}

[Serializable]
public class DialogueRef
{
    [TextArea()]
    public string[] Lines;
    public Interactable InteractableRef;

    public DialogueRef(Interactable interactableRef, string[] lines)
    {
        Lines = lines;
        InteractableRef = interactableRef;
    }
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    [SerializeField]
    public List<DialogueRef> dialogues = new List<DialogueRef>();
    [SerializeField]
    Dialogue dialogue;
    public GameState gameState;

    public DepthOfField dof;
    public VolumeProfile vp;


    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        DepthOfField tmp;
        if (vp.TryGet(out tmp))
            dof = tmp;
        else
            Debug.LogError("Can't get access to Depth of field");
    }


    public void AddDialogue(Interactable InteractableRef, string[] lines)
    {
        AddDialogue(new DialogueRef(InteractableRef, lines));
    }

    public void AddDialogue(DialogueRef dialogueRef)
    {
        dialogues.Add(dialogueRef);
    }

    public void StartDialogue(Interactable interactable)
    {
        if (!dialogues.Any(d => d.InteractableRef == interactable))
        {
            Debug.LogWarning("No dialogue in list referes to interactable!");
            return;
        }

        gameState = GameState.Dialogue;
        dof.focalLength.value = 300f;

        dialogue.gameObject.SetActive(true);
        dialogue.StartDialogue(interactable.lines);
    }

    public void DialogueFinished()
    {
        dof.focalLength.value = 1;
        gameState = GameState.Game;
        dialogue.gameObject.SetActive(false);
    }
}
