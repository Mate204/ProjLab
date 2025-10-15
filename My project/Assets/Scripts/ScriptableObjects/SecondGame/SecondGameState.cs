using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SecondGameState", menuName = "Scriptable Objects/SecondGameState")]
public class SecondGameState : ScriptableObject
{
    private long _turn;
    public long Turn => _turn;

    public event Action OnStep = delegate { };
    private void Awake()
    {
        _turn = 0;
    }

    public void StepTurn()
    {
        _turn++;
        OnStep?.Invoke();
    }
}
