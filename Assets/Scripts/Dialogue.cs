using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class Dialoguee
{
    public int ID;
    public int NextID = -1;
    [TextArea()]
    public string[] Lines;
    public DialogueAnswer[] Answers;
    //public string sceneName;
    public bool showSceneSelectionAfterDialogue = false;
}

[Serializable]
public class DialogueAnswer
{
    public int NextID;
    [TextArea()]
    public string Line;
}


public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [SerializeField]
    Dialoguee[] dialoguees;
    public string[] lines;
    public DialogueAnswer[] answers;
    [Min(1)]
    public float charsPerSecond = 15;

    [SerializeField]
    private int diaIndex;
    [SerializeField]
    private int lineIndex;

    [SerializeField]
    GameObject buttonsHolder;
    [SerializeField]
    GameObject buttonPrefab;
    

    void Awake()
    {
        textComponent.text = "";
    }

    void Update()
    {
        if (DialogueManager.Instance.gameState != GameState.Dialogue)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (textComponent.text == lines[lineIndex])
                NextLine();
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[lineIndex];
            }
        }
    }

    public void StartDialogue(Dialoguee[] dialoguees)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        diaIndex = 0;
        this.dialoguees = dialoguees;

        ChangeLines();

        StartCoroutine(TypeLine());
    }

    void DialogueFinished()
    {
        StopAllCoroutines();
        DestroyButtons();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (dialoguees[diaIndex].showSceneSelectionAfterDialogue)
        {
            DialogueManager.Instance.ShowSceneButtons();
            return;
        }

        DialogueManager.Instance.DialogueFinished();
    }

    public void OnAnswer(int nextID)
    {
        diaIndex = nextID;
        DestroyButtons();
        ChangeLines();
        StartCoroutine(TypeLine());
    }

    void ChangeLines()
    {
        lineIndex = 0;
        lines = dialoguees[diaIndex].Lines;
        answers = dialoguees[diaIndex].Answers;
    }

    void DestroyButtons()
    {
        Debug.LogWarning("Destroying Buttons");
        for (var i = buttonsHolder.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonsHolder.transform.GetChild(i).gameObject);
        }
    }

    void NewButtons()
    {
        foreach (var answer in answers)
        {
            var button = Instantiate(buttonPrefab, buttonsHolder.transform).GetComponent<AnswerButton>();
            button.Instantiate(answer.NextID, answer.Line, this);
        }
    }

    IEnumerator TypeLine()
    {
        textComponent.text = "";
        foreach (char c in lines[lineIndex].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(1 / charsPerSecond);
        }
    }

    void NextLine()
    {
        if (lineIndex < lines.Length - 1)
        {
            lineIndex++;
            StartCoroutine(TypeLine());
        }
        else if (answers.Length > 0)
        {
            DestroyButtons();
            NewButtons();
        }
        else if (diaIndex < dialoguees.Length - 1)
        {
            diaIndex++;
            ChangeLines();
            StartCoroutine(TypeLine());
        }
        else
        {
            DialogueFinished();
        }
    }
}
