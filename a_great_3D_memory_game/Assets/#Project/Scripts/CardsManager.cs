using UnityEngine;
using System.Collections.Generic;

public class CardsManager : MonoBehaviour
{
    List<CardBehaviour> deck;
    Color[] colors;
    public void Initialize(List<CardBehaviour> deck, Color[] colors)
    {
        this.colors = colors;
        this.deck = deck;
        AssignColors();
    }

    public void AssignColors()
    {
        int rndColorIndex;

        for (int i = 0; i < deck.Count; i++)
        {
            rndColorIndex = Random.Range(0, colors.Length);
            Color color = colors[rndColorIndex];
            deck[i].Initialize(color, i, this);
        }

    }
}
