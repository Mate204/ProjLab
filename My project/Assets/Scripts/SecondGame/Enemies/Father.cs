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
    [SerializeField] private float stepIntervall;
    private int _currentNode = 0;
    public void StartRoutine()
    {
        StartCoroutine(Step());
    }
    private IEnumerator Step()
    {
        yield return new WaitForSeconds(stepIntervall);

        if (_path[_currentNode].transform.position == transform.position)
        {
            _currentNode++;
        }
        Vector3 diff = _path[_currentNode].transform.position - transform.position;

        diff.y = 0;
        diff.Normalize();
        Vector3 lookdir = new(
            diff.x >= 0.5 ? 1 : 0,
            0,
            diff.y >= 0.5 ? 1 : 0);

        _enemy.Look(lookdir);
        _enemy.Move(lookdir);
        _enemy.SeesPlayer();

        if (_currentNode < _path.Count)
            StartCoroutine(Step());
    }
}
