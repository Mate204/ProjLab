using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
    public long TurnCounter => _turns;
    [SerializeField] private long _turns;

    public Level CurrentLevel => _currentLevel;
    [SerializeField] private Level _currentLevel;
    void Start() {
        _turns = 0;
        _currentLevel = Level.House;
    }

    public void NextTurn() {
        _turns++;
        OnTurn?.Invoke(TurnCounter);

        switch (_currentLevel) {
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
    [SerializeField] private PlayerController _player;
    [SerializeField] private float _secondAfterAlert = 5;
    [SerializeField] private float _flashRate = 0.2f;
    [SerializeField] private float _alertOnTurn = 20;
    public event Action OnAlert = delegate { };
    private void Alert() {
        AudioSource.PlayClipAtPoint(_houseAlertClip, Camera.main.transform.position);
        InvokeRepeating(nameof(FlashText), 0, _flashRate);

        StartCoroutine(AlertTimer());
    }
    private void FlashText() {
        if (_hesComingText.gameObject.activeSelf)
            _hesComingText.gameObject.SetActive(false);
        else
            _hesComingText.gameObject.SetActive(true);
    }
    private IEnumerator AlertTimer() {
        yield return new WaitForSeconds(_secondAfterAlert);

        OnAlert.Invoke();
        _father.gameObject.SetActive(true);
        _father.StartRoutine();
        StartCoroutine(FlashDoor());

        if (_player.IsHiding) {
            _player.enabled= false;
        }
        CancelInvoke();
        _hesComingText.gameObject.SetActive(false);
    }
    private IEnumerator FlashDoor() {
        _door.OpenDoorVisual();
        yield return new WaitForSeconds(0.2f);
        _door.CloseDoorVisual();
    }
    [SerializeField] private Door _door;
    public void StopAlert() {
        if (_player.IsHiding) {
            _player.enabled = true;
        }
        _door.locked = false;
        _father.gameObject.SetActive(false);
    }

    [SerializeField] private Image _died_text=null;
    [SerializeField] private AudioClip _died_audio=null;
    [SerializeField] private InputReader _input;
    public void GameOver() {
        Debug.Log("Game over!");
        _player.gameObject.SetActive(false);
        _died_text.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(_died_audio, Camera.main.transform.position);
        _input.InteractPerformed += Reload;
    }
    private void Reload() {
        _input.InteractPerformed -= Reload;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public enum Level {
        House,
        Outside,
        Church,
    }
}
