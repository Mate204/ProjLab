using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.Windows;

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
    [SerializeField] private GameState _gameState;
    public bool IsHiding = false;

    private void OnEnable()
    {
        _input.InteractPerformed += Interact;
        _input.InspectPerformed += Inspect;
        _input.Look += Look;
    }

    private void Start()
    {
        _lookDir = transform.forward;
    }
    private void OnDisable()
    {
        _input.InteractPerformed -= Interact;
        _input.InspectPerformed -= Inspect;
        _input.Look -= Look;
        _rumble.SetRumble(0, 0);
    }

    private void Look(Vector2 dir)
    {
        Vector3 lookDirection = new(dir.x, 0, dir.y);
        var x = Mathf.Abs(lookDirection.x) <= .1f ? 0f : // Deadzone
        lookDirection.x > 0 ? 1f : -1f; // Direction

        var y = Mathf.Abs(lookDirection.z) <= .1f ? 0f :
            lookDirection.z > 0 ? 1f : -1f;
        _lookDir = new Vector3(x,0,y);

        transform.forward = _lookDir;

        bool hit = !TryGetTile(out var target);
        if (hit)
        {
            AudioSource.PlayClipAtPoint(_lookCollideClip, _camera.transform.position);
            return;
        }

        if (target.TryGetComponent<Interactable>(out _))
            AudioSource.PlayClipAtPoint(_lookInvestigatableClip, _camera.transform.position);
        else
            AudioSource.PlayClipAtPoint(_lookClip, _camera.transform.position);
    }
    private void Interact()
    {
        if (!TryGetTile(out var target))
        {
            Vector3 moveDir = _lookDir;
            moveDir.x *= _movementScale.x;
            moveDir.y *= _movementScale.y;

            transform.position += moveDir;
            AudioSource.PlayClipAtPoint(_moveClip, _camera.transform.position);
        }
        else
        {
            if (target.TryGetComponent<Interactable>(out var interact))
            {
                AudioSource.PlayClipAtPoint(_interactClip, _camera.transform.position);
                interact.Interact(gameObject);
            }
            else
                AudioSource.PlayClipAtPoint(_interactFailClip, _camera.transform.position);
        }

        _gameState.NextTurn();
    }
    private void Inspect()
    {
        if (!TryGetTile(out var target))
            return;

        if (target.TryGetComponent<Interactable>(out var interact))
            AudioSource.PlayClipAtPoint(interact.NarratorClip, _camera.transform.position);
        else
            AudioSource.PlayClipAtPoint(_fallbackInspectNarratorClip, _camera.transform.position);
    }
    public Interactable Toggleable = null;
    private bool TryGetTile(out Transform hitObject)
    {
        if (Toggleable != null) {
            hitObject = Toggleable.transform;
            return true;
        }

        Vector3 origin = transform.position+ Vector3.up * 0.5f;
        bool r = Physics.Raycast(origin, transform.forward, out var target, _raycastLength, _raycastMask);

        if (r)
            hitObject = target.transform;
        else
            hitObject = null;

        return r;
    }

    public float AlertLevel = float.MaxValue;
    public Vector2 AlertDir = Vector2.zero;
    [SerializeField] private float _alertSoundDistanceFromCamera = 3f;
    [SerializeField] private RumbleManager _rumble;
    [SerializeField] private AudioSource _alertSource;
    [SerializeField] private AudioClip _alertHigh, _alertMed, _alertLow;
    [SerializeField] private float _alertHighDist, _alertMedDist, _alertLowDist;
    [SerializeField] private float _rumbleStrengthHigh = 10, _rumbleStrengthMed = 5, _rumbleStrengthLow = 2;
    public void FixedUpdate()
    {
        if (AlertLevel > _alertLowDist || AlertLevel==0)
        {
            _rumble.SetRumble(0, 0);
            _alertSource.mute = true;
            return;
        }
        _alertSource.mute = false;
        if (!_alertSource.isPlaying)
        {
            _alertSource.Play();
        }

        if (AlertLevel > _alertMedDist)
        {
            _alertSource.clip = _alertLow;
            _rumble.SetRumble(_rumbleStrengthLow, _rumbleStrengthLow);
        }
        else
        if (AlertLevel > _alertHighDist)
        {
            _alertSource.clip = _alertMed;
            _rumble.SetRumble(_rumbleStrengthMed, _rumbleStrengthMed);
        }
        else
        {
            _alertSource.clip = _alertHigh;
            _rumble.SetRumble(_rumbleStrengthHigh, _rumbleStrengthHigh);
        }
        _alertSource.transform.localPosition = AlertDir*_alertSoundDistanceFromCamera;
    }
}
