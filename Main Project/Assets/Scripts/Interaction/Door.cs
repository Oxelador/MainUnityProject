using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Animator _animator;

    public bool IsInteracted { get; set; }

    private void Start()
    {
        IsInteracted = false;
        _animator = GetComponent<Animator>();
    }

    public string GetDescription()
    {
        return $"Open the {gameObject.name}";
    }

    public void Interact()
    {
        _animator.SetBool("isOpen", true);
    }
}
