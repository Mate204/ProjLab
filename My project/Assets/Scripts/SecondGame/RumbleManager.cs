using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    private Gamepad _gamepad;

    public void SetRumble(float low, float high, float duration)
    {
        _gamepad = Gamepad.current;
        SetRumble(low, high);
        StartCoroutine(StopRumble(duration));
    }
    private IEnumerator StopRumble(float duration)
    {
        yield return new WaitForSeconds(duration);

        _gamepad?.SetMotorSpeeds(0, 0);
    }
    public void SetRumble(float low, float high)
    {
        _gamepad = Gamepad.current;
        _gamepad?.SetMotorSpeeds(low, high);
    }
}
