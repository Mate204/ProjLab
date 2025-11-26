using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public long TurnCounter => _turns;
    [SerializeField] private long _turns;

    public Level CurrentLevel => _currentLevel;
    [SerializeField] private Level _currentLevel;
    void Start()
    {
        _turns = 0;
        _currentLevel = Level.House;
    }

    public void NextTurn()
    {
        _turns++;
        OnTurn?.Invoke(TurnCounter);

        switch (_currentLevel)
        {
            case Level.House:
                if (_turns == _alertOnTurn)
                    Alert();
                break;
            case Level.Outside:
                break;
            case Level.Church:
                break;
        }
    }

    public event Action<long> OnTurn = delegate { };

    [SerializeField] private AudioClip _houseAlertClip;
    [SerializeField] private Text _hesComingText;
    [SerializeField] private Father _father;
    [SerializeField] private float _secondAfterAlert = 5;
    [SerializeField] private float _flashRate = 0.2f;
    [SerializeField] private float _alertOnTurn = 20;
    private void Alert()
    {
        AudioSource.PlayClipAtPoint(_houseAlertClip, Camera.main.transform.position);
        InvokeRepeating(nameof(FlashText),0, _flashRate);

        StartCoroutine(AlertTimer());
    }
    private void FlashText()
    {
        if (_hesComingText.gameObject.activeSelf)
            _hesComingText.gameObject.SetActive(false);
        else
            _hesComingText.gameObject.SetActive(true);
    }
    private IEnumerator AlertTimer()
    {
        yield return new WaitForSeconds(_secondAfterAlert);

        _father.gameObject.SetActive(true);
        _father.StartRoutine();
        CancelInvoke();
    }

    public enum Level
    {
        House,
        Outside,
        Church,
    }
}
