using UnityEngine;

[RequireComponent(typeof(Renderer))] // Safety for use : object HAS TO have a renderer in order to apply a CardBehaviour script to it


public class CardBehaviour : MonoBehaviour
{
    private Vector3 memoScale;
    private Color color;
    private int indexColor;
    private CardsManager manager;

    [SerializeField] private Vector3 scaleOnFocus = Vector3.one * 1.5f;


    private void OnMouseEnter()
    {
        memoScale = transform.localScale;
        transform.localScale = scaleOnFocus;
    }

    private void OnMouseExit()
    {
        transform.localScale = memoScale;
    }

    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public void Initialize(Color color, int indexColor, CardsManager manager)
    {
        this.color = color;
        this.indexColor = indexColor;
        this.manager = manager;

        // Temporary overview
        ChangeColor(color);
    }
}
