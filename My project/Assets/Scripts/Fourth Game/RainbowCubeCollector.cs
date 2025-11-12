using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RainbowCubeCollector : MonoBehaviour
{
    public float minX = -25f;
    public float maxX = 25f;
    public float minZ = -25f;
    public float maxZ = 25f;

    public float safeRadius = 25f;

    public TextMeshProUGUI touchCounterText;
    public TextMeshProUGUI timerText;

    private int touchCount = 0;
    private float countdownTime = 60f;

    public AudioSource collectedAudio;
    public AudioSource detectorAudio;

    private Transform player;

    private void Start()
    {
        touchCounterText.text = "Coins Collected: " + touchCount;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }


        detectorAudio.loop = true;
        detectorAudio.Play();


        StartCoroutine(CountdownTimer());
    }

    private void Update()
    {
        if (player != null && detectorAudio != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            float pitch = Mathf.Lerp(2f, 0.5f, distance / 50f);
            detectorAudio.pitch = pitch;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchCount++;
            UpdateUI();
            if (collectedAudio != null)
            {
                collectedAudio.Play();
            }
            Teleport();
        }

        void Teleport()
        {
            Vector3 newPos;
            float currentyY = transform.position.y;
            Vector3 currentPos = transform.position;

            do
            {
                float randomX = Random.Range(minX, maxX);
                float randomZ = Random.Range(minZ, maxZ);

                newPos = new Vector3(randomX, currentyY, randomZ);
            } while (Vector3.Distance(newPos, currentPos) < safeRadius);

            transform.position = newPos;
        }
    }

    void UpdateUI()
    {
        if (touchCounterText != null)
        {
            touchCounterText.text = "Coins Collected: " + touchCount;
        }
    }

    IEnumerator CountdownTimer()
    {
        float timeLeft = countdownTime;

        while (timeLeft > 0)
        {
            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.Ceil(timeLeft).ToString();
            }
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }
        //Destroy(gameObject);
        timerText.text = "Time: 0";
        detectorAudio.Stop();
        transform.position = new(9999f, 9999f, 9999f);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
