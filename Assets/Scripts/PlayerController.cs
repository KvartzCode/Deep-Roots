using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Interactable currentTarget = null;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ray test = new Ray();
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Ray ray = Camera.main.ViewportPointToRay(Camera.main.transform.forward);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);
        Debug.DrawRay(ray.origin, ray.direction);
        if (hitData.collider.CompareTag("Interactable"))
        {
            currentTarget = hitData.collider.GetComponent<Interactable>();
        }
    }

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }

    void OnFire()
    {
        Debug.Log("OnFire triggered!");
        //if ()
    }
}
