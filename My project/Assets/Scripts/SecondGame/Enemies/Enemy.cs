using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameState m_GameState;
    public PlayerController Player => _player;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Vector3 _tileSize = Vector3.one;

    private void OnDisable() {
        _player.AlertLevel = float.MaxValue;
    }
    private void OnDestroy() {
        _player.AlertLevel = float.MaxValue;
    }

    public void Look(Vector2 dir) {
        Vector3 lookDirection = new(dir.x, 0, dir.y);

        transform.forward = lookDirection;
    }
    public void StepTowardsPlayer() {
        Vector3 diff = _player.transform.position - transform.position;

        diff.y = 0;
        diff.Normalize();
        Vector2 lookdir = new(
            Mathf.Round(diff.x),
            Mathf.Round(diff.z));
        transform.forward = new Vector3(lookdir.x,0,lookdir.y);
        Move(lookdir);
    }
    public void Move(Vector2 dir) {
        Vector3 moveDir = new Vector3(dir.x, 0, dir.y);
        moveDir.x *= _tileSize.x;
        moveDir.z *= _tileSize.y;

        transform.position += moveDir;
    }

    [SerializeField] private float _lineOfSightDistance = 50;
    [SerializeField] LayerMask _lineOfSightLayer;
    public bool SeesPlayer() {
        Vector3 diffDir = _player.transform.position-transform.position;

        Debug.DrawRay(transform.position + Vector3.up,
            diffDir * _lineOfSightDistance,
            Color.aliceBlue,
            0.5f);
        Physics.Raycast(transform.position + Vector3.up,
            diffDir,
            out var hit,
            _lineOfSightDistance,
            _lineOfSightLayer);

        if (hit.transform.CompareTag("Player")) {
            _player.AlertLevel = hit.distance;
            Vector3 diff = hit.transform.position - transform.position;
            _player.AlertDir = new Vector2(diff.x, diff.z);

            return !_player.IsHiding;
        }
        return false;
    }
}
