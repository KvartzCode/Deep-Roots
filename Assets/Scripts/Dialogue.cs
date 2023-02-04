using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    [Min(1)]
    public float charsPerSecond = 15;

    private int index;

    private void Awake()
    {
        textComponent.text = "";
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    textComponent.text = "";
    //    //StartDialogue();
    //}

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.Instance.gameState != GameState.Dialogue)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (textComponent.text == lines[index])
                NextLine();
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue(string[] lines)
    {
        this.lines = lines;
        index = 0;

        StartCoroutine(TypeLine());
    }

    void DialogueFinished()
    {
        StopAllCoroutines();
        DialogueManager.Instance.DialogueFinished();
    }

    IEnumerator TypeLine()
    {
        textComponent.text = "";
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(1 / charsPerSecond);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            //textComponent.text = "";
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            DialogueFinished();
        }
    }
}
