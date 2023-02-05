using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] SceneTransition sceneTransition;

    [SerializeField] float startDuration = 2;
    [SerializeField] float startDelay = 1;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        sceneTransition.StartTransition(0, startDuration, startDelay);
        yield return new WaitForSeconds(startDuration + startDelay);
        DialogueManager.Instance.gameState = GameState.Game;
    }
}
