using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private readonly float[] linePositionsY = { -1f, -4.5f, -7.5f };
    private int currentLineIndex = 1; // Start in the middle line
    private float laneChangeSpeed = 10f;
    private Vector3 targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = transform.position;
        targetPosition.y = linePositionsY[currentLineIndex];
        transform.position = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && currentLineIndex > 0)
        {
            currentLineIndex--;
            targetPosition.y = linePositionsY[currentLineIndex];
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && currentLineIndex < linePositionsY.Length - 1)
        {
            currentLineIndex++;
            targetPosition.y = linePositionsY[currentLineIndex];
        }
        // Smoothly move to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * laneChangeSpeed);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collided with obstacle!");
            GameManager.Instance.GameOver();
            //gameObject.SetActive(false);

        }
    }
}
