using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private Outline outline;
    
    public Sprite hiddeniconSprite;
    public Sprite iconSprite;
    public AudioClip cardSound;

    public bool isSelected;
    public CardsController controller;
    public bool found;

    public void OnCardClick()
    {
        if (!found)
        {
            PlaySound();
            controller.SetSelected(this);
        }
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
        StartCoroutine(DelayedShow());
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

    private IEnumerator DelayedShow()
    {
        yield return new WaitForSeconds(0.1f);
        iconImage.sprite = iconSprite;
        isSelected = true;
    }

    public void SetFocus(bool isFocused)
    {
        if (outline != null)
        {
            outline.enabled = isFocused;
            //Debug.Log($"Outline {(isFocused ? "enabled":"disabled")} on card: {name}");
        }
        //else
        //{
        //    Debug.LogWarning("Outline is NULL on card: " + name);
        //}
    }

}
