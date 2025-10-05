using UnityEngine;

public class HidingPlace : Interactable
{
    [Tooltip("DO NOT CHANGE!")] public bool Hiding = false;
    [SerializeField] private AudioClip _hideSoundClip, _unhideSoundClip;
    public override void Interact(GameObject caller)
    {
        if (!Hiding)
        {
            AudioSource.PlayClipAtPoint(_hideSoundClip, Camera.main.transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(_unhideSoundClip, Camera.main.transform.position);
        }
    }
}