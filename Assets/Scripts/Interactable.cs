using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Interactable : MonoBehaviour
{
    public Volume urpVolume;
    public bool isOn = false;
    public DepthOfField dof;
    public VolumeProfile vp;
    DepthOfField dofComponent;


    private void Start()
    {
        //dof = vp.components.FirstOrDefault(
        //    c => c.GetType() == typeof(DepthOfField)) as DepthOfField;

        DepthOfField tmp;
        if (vp.TryGet(out tmp))
            dof = tmp;
        else
            Debug.LogError("Can't get access to Depth of field");

    }

    void Update()
    {
        if (!isOn)
            dof.focalLength.value = 1;
    }

    public void TriggerInteraction()
    {
        isOn = true;
        dof.focalLength.value = 300f;

        Debug.Log("Interaction Triggered!");
    }
}
