using RoomStruct;
//using Unity.Mathematics; CHECK
using UnityEngine;

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
    private int[] tilePulledAngle = new int[3];


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



    // draws the very first tile - always a 4 path route
    public void DrawStart(int x, int y) // CHECK - needs to mimick Draw
    {
        // sets tile to drafted
        tilesSorted[x, y].tag = "drafted";

        // pulls the first tile in the deck (always a quad door room)
        tilePulled[0] = deckHandler.PullSelect(0);

        float tileAngle = 0; // always quad so no need to rotate

        // TEMP - set up the ahead tile to take data from the Room obj
        Room tileSelected = tilePulled[0];

        tilesSorted[x, y].GetComponent<RoomInfo>().SetRoom(tileSelected, tileAngle, false);

        // sets the tileSelected's sprite, sets and rotates it
        Sprite newSprite = sprites[tileSelected.spriteID];
        tilesSorted[x, y].GetComponent<SpriteRenderer>().sprite = newSprite;

        Debug.Log($"First Tile Drawn");
        // TEMP debug output
    }

    // checks if tile is drawn
    public bool CheckDrafted(int x, int y) // returns true if drafted, false if not
    {
        if (tilesSorted[x, y].tag == "drafted")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // draws any further tiles
    public void Draw(int x, int y, int approach) // approach is for side the player enters from: 1 from north, 2 from east, 
    {
        // sets tile to drafted
        tilesSorted[x, y].tag = "drafted";


        // ROOM SELECTOR
        for (int tile = 0; tile < 1; tile++)
        {
            tilePulled[tile] = deckHandler.PullRandom(false, false, 0);
            Debug.Log($"Pulled Tile with spriteID { tilePulled[tile].spriteID}");


            // ROTATION RANDOMISER
            approach--; // reduces by 1 as the passed in had 0 as no movement (which would never be passed)
            int doorCount = 0;
            for (int a = 0; a < 3; a++)
            {
                if (tilePulled[tile].doorways[a])
                {
                    doorCount++;
                }
            }
            int randResult = Random.Range(0, doorCount);

            tilePulledAngle[tile] = (approach * 90) + (randResult * 90); // sets angle to the approach * 90

            if (tilePulled[tile].doorways[2] && doorCount == 2) // for the straight room, as doors not adjacent
            {
                tilePulledAngle[tile] = (approach * 90) + (randResult * 180);
            }
            
        

        }



        // TEMP - set up the ahead tile to take data from the Room obj
        Room tileSelected = tilePulled[0];
        tileSelected.angle = tilePulledAngle[0];

        tilesSorted[x, y].GetComponent<RoomInfo>().SetRoom(tileSelected, tilePulledAngle[0], true); 

        // sets the tileSelected's sprite, sets and rotates it - TEMP SET TO [0] - use selector
        Sprite newSprite = sprites[tileSelected.spriteID];
        tilesSorted[x, y].GetComponent<SpriteRenderer>().sprite = newSprite;
        tilesSorted[x, y].transform.Rotate(0f, 0f, tilePulledAngle[0], Space.Self);

        Debug.Log($"Tile Swapped");
        // TEMP debug output
    }

    public void ClearAll()
    {
        // deck reset functions
        deckHandler.ResetDeck();

        // 6x6 grid clearer
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                // can select sprite from sprites[]
                Sprite newSprite = defaultSprite;
                tilesSorted[x, y].GetComponent<SpriteRenderer>().sprite = defaultSprite;
                tilesSorted[x, y].GetComponent<RoomInfo>().ClearRoom();

                Debug.Log($"Tile Swapped");

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
