using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CardsManager : MonoBehaviour
{
    private List<CardBehaviour> deck;
    private Color[] colors;
    private int maxCardsPerColor;
    private CardBehaviour memoCard = null;
    private int cardsCounter = 0;

    [SerializeField] private float sceneLoaderDelay = 1f;
    [SerializeField] private float delayBeforeFaceDown = 1f;


    private void VictorySceneManager()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void Initialize(List<CardBehaviour> deck, Color[] colors, int maxCardsPerColor)
    {
        this.colors = colors;
        this.deck = deck;
        this.maxCardsPerColor = maxCardsPerColor;

        memoCard = null;
        cardsCounter = 0;

        AssignColors();
    }

    public void AssignColors()
    {
        int colorIndex;
        int cardIndex;

        List<int> colorsAlreadyPicked = new(); // List of indexes
        List<CardBehaviour> discard = new(deck); // Creates a clone of ou deck (since it is passed as an argument.) 
        //  /!\ It takes more instantiations than interations which is a good choice in an Initialize() but maybe not in an Updat() function.

        for (int _ = 0; _ < deck.Count / maxCardsPerColor; _++)
        {
            colorIndex = Random.Range(0, colors.Length); // .Length car array
            int c = 0;

            while (colorsAlreadyPicked.Contains(colorIndex) && c++ < 100)
            {
                colorIndex = Random.Range(0, colors.Length);
            }

            colorsAlreadyPicked.Add(colorIndex);

            for (int __ = 0; __ < maxCardsPerColor; __++) //
            {
                cardIndex = Random.Range(0, discard.Count);
                discard[cardIndex].Initialize(colors[colorIndex], colorIndex, this);
                discard.RemoveAt(cardIndex); // RemoveAt() = Pop() in python        
            }

        }
    }

    public void CardIsClicked(CardBehaviour card)
    {
        if (card.IsFaceUp) return; // 

        card.FaceUp();

        if (memoCard != null) // In Unity, not possible to use "is not null" rather than != because != also verifies objects BEING destroyed (but not yet destroyed)
        {
            if (card.IndexColor != memoCard.IndexColor)
            {
                memoCard.FaceDown(delayBeforeFaceDown);
                card.FaceDown(delayBeforeFaceDown);
            }
            else
            {
                cardsCounter++;
                Debug.Log($"Found pairs : {cardsCounter}");

                if (cardsCounter >= deck.Count/2) // /!\ not working with my maxCardsPerColor variable rn !! find the formula !
                {
                    Debug.Log("VICTORY !");
                    Invoke("VictorySceneManager", sceneLoaderDelay);
                }
            }
            memoCard = null;
        }
        else
        {
            memoCard = card;
        }
    }

}
