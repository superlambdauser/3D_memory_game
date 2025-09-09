using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))] // Safety for use : object HAS TO have a renderer in order to apply a CardBehaviour script to it


public class CardBehaviour : MonoBehaviour
{
    private Vector3 memoScale;
    private Color color;
    private CardsManager manager;

    [SerializeField] private Color baseColor = Color.gray;
    [SerializeField] private Vector3 scaleOnFocus = Vector3.one * 1.5f;
    [SerializeField] private float colorChangeTime = 1f;

    public bool IsFaceUp { get; private set; } = false; // Public read-only property
    public int IndexColor { get; private set; } 


    private void OnMouseDown()
    {
        manager.CardIsClicked(this);

        // if (isFaceUp == false)
        // {
        //     FaceDown();
        // }
    }
    private void OnMouseEnter()
    {
        memoScale = transform.localScale;
        transform.localScale = scaleOnFocus;
    }

    private void OnMouseExit()
    {
        transform.localScale = memoScale;
    }

    private IEnumerator LerpChangeColor(Color color, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float chrono = 0f;
        Color startingColor  = GetComponent<Renderer>().material.color;

        while (chrono < colorChangeTime)
        {
            chrono += Time.deltaTime;
            ChangeColor(Color.Lerp(startingColor , color, chrono/colorChangeTime));

            yield return new WaitForEndOfFrame(); // this is exactly the same as 
            // yield return null;
        }

        ChangeColor(color); // To prevent chrono goes too far from the coolorChangeTime and returns a color value that is too far from the color required

    }

    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public void Initialize(Color color, int indexColor, CardsManager manager)
    {
        this.color = color;
        IndexColor = indexColor;
        this.manager = manager;

        ChangeColor(baseColor);
        IsFaceUp = false;
    }

    public void FaceUp()
    {
        StartCoroutine(LerpChangeColor(color));
        IsFaceUp = true;
    }

    public void FaceDown(float delay = 0f)
    {
        StartCoroutine(LerpChangeColor(baseColor, delay));
        IsFaceUp = false;
    }

}
