using System;
using UnityEngine;

public class DrawTile : MonoBehaviour
{

    // tile array declare
    private GameObject[] tilesIn;
    public GameObject[,] tilesSorted;
    GameObject current;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
                tilesSorted[x,y].transform.Translate(0.1f, 0, 0);
                
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
        }

    }
}
