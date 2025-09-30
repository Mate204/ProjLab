using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private AudioSource AudioSource;
    
    public Sprite hiddeniconSprite;
    public Sprite iconSprite;
    public AudioClip cardSound;

    public bool isSelected;
    public CardsController controller;

    public void OnCardClick()
    {
        PlaySound();
        controller.SetSelected(this);
    }

    public void SetIconSprite(Sprite sp)
    {
        iconSprite = sp;
    }
    public void SetCardSound(AudioClip clip)
    {
        cardSound = clip;
        if (AudioSource != null)
        {
            AudioSource.clip = cardSound;
        }
    }

    public void Show()
    {
        iconImage.sprite = iconSprite;
        isSelected = true;
    }

    public void Hide()
    {
        iconImage.sprite = hiddeniconSprite;
        isSelected = false;
    }

    private void PlaySound()
    {
        if (AudioSource != null && cardSound != null)
        {
            AudioSource.Play();
        }
    }

}
