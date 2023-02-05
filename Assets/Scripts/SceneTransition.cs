using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SceneTransition : MonoBehaviour
{
    public void StartTransition(float endValue, float duration, float delay = 0)
    {
        StartCoroutine(StartAnimation(endValue, duration, delay));
    }

    IEnumerator StartAnimation(float endValue, float duration, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        var img = GetComponent<Image>();
        img.CrossFadeAlpha(endValue, duration, true);
    }
}
