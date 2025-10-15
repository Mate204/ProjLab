using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardsController : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] sprites;
    [SerializeField] AudioClip[] sounds;

    private List<Sprite> spritePairs;
    private List<AudioClip> soundPairs;

    Card firstSelected;
    Card secondSelected;

    int matchCounts;


    private void Start()
    {
        PreparePairs();
        CreateCards();
    }

    private void PreparePairs()
    {
        spritePairs = new List<Sprite>();
        soundPairs = new List<AudioClip>();

        for (int i = 0; i < sprites.Length; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);

            soundPairs.Add(sounds[i]);
            soundPairs.Add(sounds[i]);
        }

        //Shuffle both lists together using a shared index
        List<int> indicies = new List<int>();
        for (int i = 0; i < spritePairs.Count; i++)
        {
            indicies.Add(i);
            ShuffleSprites(indicies);
        }

        //Apply shuffle to both lists in sync
        List<Sprite> shuffledSprites = new List<Sprite>();
        List<AudioClip> shuffledSounds = new List<AudioClip>();
        foreach (int i in indicies)
        {
            shuffledSprites.Add(spritePairs[i]);
            shuffledSounds.Add(soundPairs[i]);
        }

        spritePairs = shuffledSprites;
        soundPairs = shuffledSounds;
    }

    //private void PreapareSprites()
    //{
    //    spritePairs = new List<Sprite>();
    //    for (int i = 0; i < sprites.Length; i++)
    //    {
    //        //adding sprite 2 times to make it pair
    //        spritePairs.Add(sprites[i]);
    //        spritePairs.Add(sprites[i]);
    //    }
    //    ShuffleSprites(spritePairs);
    //}

    //private void PreapareSounds()
    //{
    //    soundPairs = new List<AudioClip>();
    //    for (int i = 0; i < sounds.Length; i++)
    //    {
    //        soundPairs.Add(sounds[i]);
    //        soundPairs.Add(sounds[i]);
    //    }
    //    ShuffleSprites(soundPairs);
    //}

    void CreateCards()
    {
        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform);
            card.SetIconSprite(spritePairs[i]);
            card.SetCardSound(soundPairs[i]);
            card.Hide();
            Debug.Log($"Card {i + 1} created with sprite: {spritePairs[i].name}");
            card.controller = this;
            allCards.Add(card);
        }
    }

    public void SetSelected(Card card)
    {
        if(card.isSelected == false)
        {
            card.Show();

            if (firstSelected == null)
            {
                firstSelected = card;
                return;
            }
            if (secondSelected == null)
            {
                secondSelected = card;
                StartCoroutine(CheckMatching(firstSelected, secondSelected));
                firstSelected = null;
                secondSelected = null;
            }
        }
    }

    IEnumerator CheckMatching(Card a, Card b)   
    {
        yield return new WaitForSeconds(0.3f);

        if (a.cardSound == b.cardSound)
        {
            //Matched
            a.found = true;
            b.found = true;
            matchCounts++;
            if (matchCounts>=soundPairs.Count/2)
            {
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            Debug.Log("Cards matched by sound!");

        }
        else
        {
            //flip them back
            yield return new WaitForSeconds(1);
            a.Hide();
            b.Hide();
            Debug.Log("Cards did not match.");
        }
    }

    //Method to shuffle a list of sprites
    void ShuffleSprites<T>(List<T> spriteList)
    {
        for (int i = spriteList.Count - 1; i > 0 ; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            //Swap the elements at i and randomIndex
            T temp = spriteList[i];
            spriteList[i] = spriteList[randomIndex];
            spriteList[randomIndex] = temp;
        }
    }

}
