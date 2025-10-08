using UnityEngine;

public class EnemyFather : MonoBehaviour
{
    [SerializeField] private SecondGameState _gameState;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Vector3 _tileSize = Vector3.one;
    private void OnEnable()
    {
        _gameState.OnStep += Step;
    }
    private void OnDisable()
    {
        _gameState.OnStep -= Step;
    }

    private void Step()
    {
        transform.forward = _player.transform.position - transform.position;
        Vector3 extreme = new(
            Mathf.Round(transform.forward.x),
            Mathf.Round(transform.forward.y),
            Mathf.Round(transform.forward.z)
            );
        Vector3 moveDir = Vector3.Scale(extreme,_tileSize);
        transform.position += moveDir;
    }
}
