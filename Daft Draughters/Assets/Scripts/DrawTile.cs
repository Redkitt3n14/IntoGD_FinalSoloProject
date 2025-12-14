using System;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DrawTile : MonoBehaviour
{

    // tile array declare
    private GameObject[] tilesIn;
    public GameObject[,] tilesSorted;
    GameObject current;

    public Sprite[] sprites;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tilesIn = GameObject.FindGameObjectsWithTag("undrafted");

        tilesSorted = new GameObject[6, 6];

        // sorts tilesIn into 2D array by X and Y position for easier later use
        for (int w = 0; w < 36; w++)
        {
            current = tilesIn[w];
            // sorts it into the position of the tiles current X and Y pos, then moved to 6x6 array
            int X = Mathf.RoundToInt(current.transform.localPosition.x);
            int Y = Mathf.RoundToInt(current.transform.localPosition.y);
            //Debug.Log($"Tile {current} at pos {X},{Y}");
            //Destroy(current);
            tilesSorted[X,Y] = current;
        }

        // any additional initial handling for the tiles can be done here
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                Debug.Log($"Tile {tilesSorted[x,y]} at pos {x},{y}");
                // moves all tiles by 2 on x as a test
                //Destroy(tilesSorted[x,y]);
                //tilesSorted[x,y].transform.Translate(1f, 0, 0);
                
            }
        }


    }

    private GameObject FindGameObjectsWithTag(string v)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // any additional initial handling for the tiles can be done here
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                Debug.Log($"Tile {tilesSorted[x, y]} at pos {x},{y}");
                // moves all tiles by 2 on x as a test
                //Destroy(tilesSorted[x,y]);
                tilesSorted[x, y].transform.Translate(0.01f, 0, 0);

            }
        }*/

    }

    public void Draw(int x, int y)
    {
        // can select sprite from sprites[]
        Sprite newSprite = sprites[0];
        tilesSorted[x, y].GetComponent<SpriteRenderer>().sprite = newSprite;

        Debug.Log($"Tile Swapped");
        // TEMP debug output
    }

    // draws the very first tile - always a 4 path route
    public void DrawStart(int x, int y)
    {
        // can select sprite from sprites[]
        Sprite newSprite = sprites[0];
        tilesSorted[x, y].GetComponent<SpriteRenderer>().sprite = newSprite;

        Debug.Log($"Tile Swapped");
        // TEMP debugs
    }

}
