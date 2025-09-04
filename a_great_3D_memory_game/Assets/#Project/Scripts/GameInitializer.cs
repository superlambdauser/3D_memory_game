
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    const float CARDSIZE = 1f;
    [SerializeField] private int columns = 2;
    [SerializeField] private int rows = 3;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private float gapBetweenCards = 0.5f;

    private void Start() // private because no inheritance needed
    {
        if ((rows * columns) % 2 != 0) // Checking if deck is even
        {
            Debug.LogError("Deck number is odd. Needs to be even.");
            return; // Stop the Start() func.
        }

        GenerateBoard();
    }

    private void GenerateCard(Vector3 clonePosition)
    {
        
        GameObject cardClone = Instantiate(cardPrefab, clonePosition, Quaternion.identity); // Quaternion.identity = (x=0, y=0, z=0, w=0) = no rotation
    }

    private void GenerateBoard()
    {
        Vector3 position;
        for (float x = 0f; x < columns * (CARDSIZE + gapBetweenCards); x += CARDSIZE + gapBetweenCards) // The for loop does the calculations
        {
            for (float y = 0f; y < rows * (CARDSIZE + gapBetweenCards); y += CARDSIZE + gapBetweenCards)
            {
                position = transform.position; // get current position
                position += Vector3.right * x; // set position to x in the right direction (Vector3.right = (1,0,0))

                position += Vector3.up * y; // same for y (Vector3.up = (0,1,0))

                GenerateCard(position);
            }
        }
    }

}

