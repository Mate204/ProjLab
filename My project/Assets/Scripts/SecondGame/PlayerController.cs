using System.Linq.Expressions;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private Camera _camera;
    private Vector3 _lookDir;
    [SerializeField]
    private AudioClip _lookClip, _lookCollideClip, _lookInvestigatableClip, _moveClip, _interactClip, _interactFailClip, _fallbackInspectNarratorClip;
    [SerializeField] private Vector2 _movementScale = Vector2.one;
    [SerializeField] private LayerMask _raycastMask = int.MaxValue;
    [SerializeField] private float _raycastLength = 0.5f;
    [SerializeField] private SecondGameState _gameState;

    private void Awake()
    {
        _input.InteractPerformed += Interact;
        _input.InspectPerformed += Inspect;
        _input.Look += Look;
    }

    private void Start()
    {
        _lookDir = transform.forward;
    }

    private void Look(Vector2 dir)
    {
        Vector3 lookDirection = new(dir.x, 0, dir.y);
        _lookDir = lookDirection;

        transform.forward = _lookDir;

        bool hit = !TryGetTile(out var target);
        if (hit)
        {
            //AudioSource.PlayClipAtPoint(_lookCollideClip, _camera.transform.position);
            return;
        }

        if (target.TryGetComponent<Interactable>(out _)) { }
            //AudioSource.PlayClipAtPoint(_lookInvestigatableClip, _camera.transform.position);
        else { }
            //AudioSource.PlayClipAtPoint(_lookClip, _camera.transform.position);
    }
    private void Interact()
    {
        if (!TryGetTile(out var target))
        {
            Vector3 moveDir = _lookDir;
            moveDir.x *= _movementScale.x;
            moveDir.y *= _movementScale.y;

            transform.position += moveDir;
            //AudioSource.PlayClipAtPoint(_moveClip, _camera.transform.position);
        }
        else
        {
            if (target.TryGetComponent<Interactable>(out var interact))
            {
                //AudioSource.PlayClipAtPoint(_interactClip, _camera.transform.position);
                interact.Interact(gameObject);
            }
            else { }
                //AudioSource.PlayClipAtPoint(_interactFailClip, _camera.transform.position);
        }

        _gameState.StepTurn();
    }
    private void Inspect()
    {
        if (!TryGetTile(out var target))
            return;

        if (target.TryGetComponent<Interactable>(out var interact)) { }
            //AudioSource.PlayClipAtPoint(interact.NarratorClip, _camera.transform.position);
        else { }
            //AudioSource.PlayClipAtPoint(_fallbackInspectNarratorClip, _camera.transform.position);
    }
    private bool TryGetTile(out Transform hitObject)
    {
        Vector3 up = Vector3.up * 0.5f;
        bool r = Physics.Raycast(transform.position + up, transform.forward, out var target, _raycastLength, _raycastMask);

        if (r)
            hitObject = target.transform;
        else
            hitObject = null;

        return r;
    }


}
