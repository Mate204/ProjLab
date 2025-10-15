using UnityEngine;

public class Door : Interactable
{
    [Tooltip("DO NOT CHANGE!")] public bool Open = false;
    [SerializeField] private AudioClip _openSound, _closeSound;
    public override void Interact(GameObject caller)
    {
        var t = transform;
        if (!Open)
        {
            t.rotation.SetEulerAngles(0, 90, 0);
            t.position += Vector3.right;
            AudioSource.PlayClipAtPoint(_openSound, Camera.main.transform.position);
            Open = true;
        }
    }
}
