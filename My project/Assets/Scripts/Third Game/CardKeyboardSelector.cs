using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class CardKeyboardSelector : MonoBehaviour
{
    public CardsController controller;
    private int currentIndex = 0;

    public List<AudioClip> focusSounds;
    private AudioSource audioSource;

    private void Start()
    {
        if (controller == null)
        {
            controller = FindObjectOfType<CardsController>();
        }
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (controller.allCards.Count > 0)
        {
            FocusCard(currentIndex);
        }
    }
    private void Update()
    {
        //Debug.Log("Update is running");
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int direction = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? -1:1 ;
            currentIndex = (currentIndex + direction + controller.allCards.Count) % controller.allCards.Count;

            if (controller.allCards[currentIndex]==null)
            {
                currentIndex = (currentIndex + direction + controller.allCards.Count) % controller.allCards.Count;
                audioSource.clip = focusSounds[currentIndex];
                audioSource.Play();
            }

            FocusCard(currentIndex);
        }
        if (Input.GetKeyDown(KeyCode.Return) ||Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Enter or Space pressed");
            controller.allCards[currentIndex].OnCardClick();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T key works...");
        }
    }

    private void FocusCard(int index)
    {
        for (int i = 0; i < controller.allCards.Count; i++)
        {
            if (controller.allCards[i] != null)
            {
                controller.allCards[i].SetFocus(i == index);
            }
            else
            {
                Debug.LogWarning($"Card at index {i} is null.");
            }
        }

        if (controller.allCards[index] != null)
        {
            //Debug.Log("Focused on card: " + controller.allCards[index].name);
            if (focusSounds != null && index < focusSounds.Count && focusSounds[index] != null)
            {
                audioSource.Stop();
                audioSource.clip = focusSounds[index];
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"Card at index {index} is null.");
        }
    }
}
