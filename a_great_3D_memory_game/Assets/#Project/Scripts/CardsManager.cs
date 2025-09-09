using UnityEngine;
using System.Collections.Generic;

public class CardsManager : MonoBehaviour
{
    private List<CardBehaviour> deck;
    private Color[] colors;
    private int maxCardsPerColor;
    private CardBehaviour memoCard = null;

    [SerializeField] float delayBeforeFaceDown;

    public void Initialize(List<CardBehaviour> deck, Color[] colors, int maxCardsPerColor)
    {
        this.colors = colors;
        this.deck = deck;
        this.maxCardsPerColor = maxCardsPerColor;
        memoCard = null;

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
        card.FaceUp();

        if (memoCard != null) // In Unity, not possible to use "is not null" rather than != because != also verifies objects BEING destroyed (but not yet destroyed)
        {
            if (card.IndexColor == memoCard.IndexColor)
            {
                Debug.Log("GG");
            }
            else
            {
                Debug.Log("raté grosse noob");
                memoCard.FaceDown(delayBeforeFaceDown);
                card.FaceDown(delayBeforeFaceDown);
            }

            memoCard = null;
        }
        else
        {
            memoCard = card;
        }
    }

}
