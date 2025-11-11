using UnityEngine;
using UnityEngine.UI;

public class MovingBackgroundWBCK7D : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RawImage rawImage;
    public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rawImage.uvRect = new Rect(
        rawImage.uvRect.x + scrollSpeedX * Time.deltaTime,
        rawImage.uvRect.y + scrollSpeedY * Time.deltaTime,
        rawImage.uvRect.width,
        rawImage.uvRect.height);
    }
}
