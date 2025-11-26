using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    public PlayerController Player => _player;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Vector3 _tileSize = Vector3.one;

    private void OnDisable()
    {
        _player.alertLevel = float.MaxValue;
    }
    private void OnDestroy()
    {
        _player.alertLevel = float.MaxValue;
    }

    public void Look(Vector2 dir)
    {
        Vector3 lookDirection = new(dir.x, 0, dir.y);

        transform.forward = lookDirection;
    }
    public void LookAtPlayer()
    {
        Vector3 diff = _player.transform.position - transform.position;

        diff.y = 0;
        diff.Normalize();
        Vector3 lookdir = new(
            diff.x >= 0.5 ? 1 : 0,
            0,
            diff.y >= 0.5 ? 1 : 0);
        transform.forward = lookdir;
    }
    public void Move(Vector2 dir)
    {
        Vector3 moveDir = dir;
        moveDir.x *= _tileSize.x;
        moveDir.y *= _tileSize.y;

        transform.position += moveDir;
    }

    [SerializeField] private float _lineOfSightDistance = 50;
    [SerializeField] LayerMask _lineOfSightLayer;
    public bool SeesPlayer()
    {
        Physics.Raycast(transform.position, _player.transform.position, out var hit, _lineOfSightDistance, _lineOfSightLayer);

        if (hit.transform == _player.transform)
        {
            _player.alertLevel = hit.distance;
            Vector3 diff = hit.transform.position - transform.position;
            _player.alertDir = new Vector2(diff.x,diff.z);
            return true;
        }
        return false;
    }
}
