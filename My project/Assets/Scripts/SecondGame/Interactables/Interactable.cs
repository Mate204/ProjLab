using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private AudioClip _narratorClip;
    public AudioClip NarratorClip => _narratorClip;
    public abstract void Interact(GameObject caller);
}
