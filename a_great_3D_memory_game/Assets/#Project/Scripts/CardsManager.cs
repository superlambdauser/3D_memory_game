using UnityEngine;
using System.Collections.Generic;

public class CardsManager : MonoBehaviour
{
    List<CardBehaviour> deck;
    Color[] colors;
    int maxCardsPerColor;
    List<Color> cardsPerColor;

    public void Initialize(List<CardBehaviour> deck, Color[] colors, int maxCardsPerColor)
    {
        this.colors = colors;
        this.deck = deck;
        this.maxCardsPerColor = maxCardsPerColor;
        AssignColors();
    }

    public void AssignColors()
    {
        int rndColorIndex;

        for (int i = 0; i < deck.Count; i++)
        {
            rndColorIndex = Random.Range(0, colors.Length);
            Color randomColor = colors[rndColorIndex];

            int colorCounter = 0;

            if (cardsPerColor != null)
            {
                for (int j=0; j < cardsPerColor.Count; j++)
                {
                    if (cardsPerColor[j] == randomColor)
                    {
                        colorCounter += 1;

                        if (colorCounter >= maxCardsPerColor)
                        {
                            i -= 1;
                            rndColorIndex = Random.Range(0, colors.Length);
                            randomColor = colors[rndColorIndex];
                            colorCounter = 0;
                        }
                        else
                        {
                            cardsPerColor.Add(randomColor);
                        }
                    }
                }
            }

            deck[i].Initialize(randomColor, i, this);
        }
    }
}
