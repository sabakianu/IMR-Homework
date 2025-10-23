using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private Animator animator;
    private bool isGrabbed = false;
    private bool isActive = false;

    [SerializeField] private KeyCode grabKey = KeyCode.G;
    [SerializeField] private KeyCode toggleKey;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isActive = !isActive;
            Debug.Log($"Mana ({toggleKey}) este activa");
        }

        if (!isActive) return;

        bool pressed = Input.GetKey(grabKey);

        if (pressed != isGrabbed)
        {
            isGrabbed = pressed;
            animator.SetBool("Grabbed", isGrabbed);
        }
    }
}
