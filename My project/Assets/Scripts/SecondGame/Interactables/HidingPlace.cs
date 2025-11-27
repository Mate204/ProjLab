using UnityEngine;

public class HidingPlace : Interactable
{
    [Tooltip("DO NOT CHANGE!")] public bool Hiding = false;
    [SerializeField] private AudioClip _hideSoundClip, _unhideSoundClip;
    private Vector3 _positionBeforeHide= Vector3.zero;
    public override void Interact(GameObject caller)
    {
        if (!Hiding)
        {
            AudioSource.PlayClipAtPoint(_hideSoundClip, Camera.main.transform.position);
            if (caller.CompareTag("Player")) {
                var player = caller.GetComponent<PlayerController>();
                player.IsHiding = true;
                player.Toggleable = this;
            }
            _positionBeforeHide = caller.transform.position;
            caller.transform.position = transform.position;
            caller.transform.localScale = new Vector3(1, 0.5f, 1);
            Hiding = true;
        }
        else
        {
            AudioSource.PlayClipAtPoint(_unhideSoundClip, Camera.main.transform.position);
            if (caller.CompareTag("Player")) {
                var player = caller.GetComponent<PlayerController>();
                player.IsHiding = false;
                player.Toggleable = null;
            }
            caller.transform.position=_positionBeforeHide;
            caller.transform.localScale = new Vector3(1, 1, 1);
            Hiding = false;
        }
    }
}