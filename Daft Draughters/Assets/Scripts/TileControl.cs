using System;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.Experimental.GraphView.GraphView;
using RoomStruct;

public class TileControl : MonoBehaviour
{

    // tile array declare
    private GameObject[] tilesIn;
    public GameObject[,] tilesSorted;
    GameObject current;

    public Sprite[] sprites;
    public Sprite defaultSprite;

    // accesses the TileManagers DeckHandler script
    private DeckHandler deckHandler;

    // sets up the holder for the 3 current random tiles
    private Room[] tilePulled = new Room[3];


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // setting up the deck script and resetting to default
        deckHandler = GetComponent<DeckHandler>();
        deckHandler.ResetDeck();

        // gets the 6x6 grid tiles
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

    // draws any further tiles
    public void Draw(int x, int y)
    {
        // can select sprite from sprites[]
        for (int tile = 0; tile < 1; tile++)
        {
            tilePulled[tile] = deckHandler.PullRandom(false, false, 0);
            Debug.Log($"Pulled Tile with spriteID { tilePulled[tile].spriteID}");

        }
        // TEMP - set up the ahead tile to take data from the Room obj
        Sprite newSprite = sprites[tilePulled[0].spriteID];

        tilesSorted[x, y].GetComponent<SpriteRenderer>().sprite = newSprite;

        Debug.Log($"Tile Swapped");
        // TEMP debug output
    }

    public void ClearAll()
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                // can select sprite from sprites[]
                Sprite newSprite = defaultSprite;
                tilesSorted[x, y].GetComponent<SpriteRenderer>().sprite = defaultSprite;

                Debug.Log($"Tile Swapped");
                // TEMP debug output

                // deck reset functions
                deckHandler.ResetDeck();
            }
        }
        

    }


    // gets the exits of a specific tile
    public int GetExits(int x, int y, int direction, out bool[] exits, out Sprite newSprite)
    {
        exits = new bool[] { true, true, true, true }; // TEMP all set to true
        newSprite = null;
        return 0;
    }

    public int RoomSelector(int x, int y)
    {
        return 0;
    }

}
