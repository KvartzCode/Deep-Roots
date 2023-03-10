using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public enum GameState
{
    Game,
    Dialogue,
    Paused,
}

[Serializable]
public class DialogueRef
{
    public Dialoguee[] Dialoguees;
    public Interactable InteractableRef;

    public DialogueRef(Interactable interactableRef, Dialoguee[] dialoguees)
    {
        Dialoguees = dialoguees;
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
    public GameState gameState = GameState.Paused;

    public DepthOfField dof;
    public VolumeProfile vp;

    public TextMeshProUGUI interactText;


    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        if (vp.TryGet(out DepthOfField tmp))
        {
            dof = tmp;
            dof.focalLength.value = 1;
        }
        else
            Debug.LogError("Can't get access to Depth of field");
    }

    public void ShowSceneButtons()
    {
        var btns = GameObject.Find("SceneButtons");
        for (var i = btns.transform.childCount - 1; i >= 0; i--)
        {
            btns.transform.GetChild(i).gameObject.SetActive(true);
        }
        gameState = GameState.Paused;
        dialogue.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ShowInteractionButton()
    {
        interactText.gameObject.SetActive(true);
    }

    public void HideInteractionButton()
    {
        interactText.gameObject.SetActive(false);
    }

    public void ChangeScene(string sceneName)
    {
        gameState = GameState.Paused;
        StartCoroutine(StartLoadScene(sceneName));
    }

    IEnumerator StartLoadScene(string sceneName)
    {
        SceneTransition transition = FindObjectOfType<SceneTransition>();
        transition.StartTransition(1, 4);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(sceneName);
    }


    public void AddDialogue(Interactable InteractableRef, Dialoguee[] dialoguees)
    {
        AddDialogue(new DialogueRef(InteractableRef, dialoguees));
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
        dialogue.StartDialogue(interactable.dialoguees);
    }

    public void DialogueFinished()
    {
        dof.focalLength.value = 1;
        gameState = GameState.Game;
        dialogue.gameObject.SetActive(false);
    }
}
