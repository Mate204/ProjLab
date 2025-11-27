using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Enemy))]
public class Father : MonoBehaviour
{
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }
    [SerializeField]private List<PathNode> _path=new List<PathNode>();
    [SerializeField] private float _stepIntervall = 0.4f;
    [SerializeField] private float _chaseStepIntervall = 0.1f;
    public bool IsInRoutine = false;
    private int _currentNode = 0;
    public void StartRoutine()
    {
        IsInRoutine= true;
    }
    private float _nextTimeToStep = 0;
    private void FixedUpdate() {
        if (_nextTimeToStep <= Time.time)
            Step();
    }
    private void Step()
    {
        if (_currentNode >= _path.Count) {
            _enemy.m_GameState.StopAlert();
            _currentNode = 0;
            IsInRoutine = false;
            gameObject.SetActive(false);
        }

        if (_enemy.SeesPlayer()) {
            _enemy.StepTowardsPlayer();
            _nextTimeToStep= Time.time+_chaseStepIntervall;

            if (Vector3.Distance(_enemy.Player.transform.position, transform.position) <= 0.5f) {
                Debug.Log("Father cought you!");
                _enemy.m_GameState.GameOver();
            }
            return;
        }

        if (Vector3.Distance(_path[_currentNode].transform.position, transform.position) <= 0.5f)
        {
            _currentNode++;
        }
        Vector3 diff = _path[_currentNode].transform.position - transform.position;

        diff.y = 0;
        diff.Normalize();
        Vector2 lookdir = new(
            Mathf.Round(diff.x),
            Mathf.Round(diff.z));

        _enemy.Look(lookdir);
        _enemy.Move(lookdir);

        _nextTimeToStep = Time.time + _stepIntervall;
    }
}
