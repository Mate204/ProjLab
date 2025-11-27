using UnityEngine;

public class Door : Interactable {
    public bool locked = true;
    [Tooltip("DO NOT CHANGE!")] public bool Open = false;
    [SerializeField] private AudioClip _openSound, _closeSound;
    [SerializeField] private Transform _playerTeleportPos, _camerateleportPos;
    [SerializeField] private PlayerController _playerController;

    public override void Interact(GameObject caller) {
        if (locked)
            return;

        if (!Open) {
            OpenDoorVisual();
            Open = true;
        }
        else {
            CloseDoorVisual();
            Open = false;
            _playerController.transform.position = _playerTeleportPos.position;
            Camera.main.transform.position = _camerateleportPos.position;
        }
    }

    public void OpenDoorVisual() {
        transform.localScale -= new Vector3(0.5f, 0, 0);
        transform.localPosition += new Vector3(0.25f, 0, 0);
        AudioSource.PlayClipAtPoint(_openSound, Camera.main.transform.position);
    }
    public void CloseDoorVisual() {
        transform.localScale += new Vector3(0.5f, 0, 0);
        transform.localPosition -= new Vector3(0.25f, 0, 0);
        AudioSource.PlayClipAtPoint(_closeSound, Camera.main.transform.position);
    }
}
