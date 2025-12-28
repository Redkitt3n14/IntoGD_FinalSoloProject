using RoomStruct;
using UnityEngine;

public class TileControl : MonoBehaviour
{

    // tile array declare
    private GameObject[] tilesIn;
    public GameObject[,] tilesSorted; // NOTE: this is the main publically stored variable in the project - the core 6x6 gameobject storage
    GameObject current;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Sprite defaultSprite;

    // declare access to the TileManagers DeckHandler script
    private DeckHandler deckHandler;

    // declarations for the holder for the 3 current random tiles
    private Room[] tilePulled = new Room[3];
    private int[] tilePulledAngle = new int[3];

    // gui variable declarations
    [SerializeField] private GameObject guiGroup;
    private GameObject[] guiTiles = new GameObject[3];
   

    // Awake is called before start
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

            tilesSorted[X,Y] = current;
        }

        // any additional initial handling for the tiles can be done here
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {

                
            }
        }
    }

    // start is called once before update - after all other stuff done building
    private void Start()
    {
        // gui Tiles setup
        for (int i = 0; i < 3; i++)
        {
            guiTiles[i] = guiGroup.transform.GetChild(i).gameObject;
        }
    }



    // draws the very first tile - always a 4 path route
    public void DrawStart(int x, int y) // mimick Draw but pulls 0th tile from pile (always a quad room)
    {
        // sets tile to drafted
        tilesSorted[x, y].tag = "drafted";

        // pulls the first tile in the deck (always a quad door room)
        tilePulled[0] = deckHandler.PullSelect(0);

        float tileAngle = 0; // always quad so no need to rotate

        // set up the ahead tile to take data from the Room obj
        Room tileSelected = tilePulled[0];

        tilesSorted[x, y].GetComponent<RoomInfo>().SetRoom(tileSelected, tileAngle, false);

        // sets the tileSelected's sprite, sets and rotates it
        Sprite newSprite = sprites[tileSelected.spriteID];
        tilesSorted[x, y].GetComponent<SpriteRenderer>().sprite = newSprite;

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
    public void Pull3Random(int x, int y, int approach) // approach is for side the player enters from: 1 from north, 2 from east, 
    {
        // sets tile to drafted
        tilesSorted[x, y].tag = "drafted";

        // reduces by 1 as the passed in had 0 as no movement (which would never be passed)
        approach--; 


        // ROOM SELECTOR
        for (int tile = 0; tile < 3; tile++)
        {
            tilePulled[tile] = deckHandler.PullRandom(false, false, tile);


            // ROTATION RANDOMISER
            int doorCount = 0;
            for (int a = 0; a < 4; a++)
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

            // GUI tile setter
            Sprite newSprite = sprites[tilePulled[tile].spriteID];
            guiTiles[tile].GetComponent<SpriteRenderer>().sprite = newSprite;
            guiTiles[tile].transform.Rotate(0f, 0f, tilePulledAngle[tile], Space.Self);

        }

    }

    public void Draw(int x, int y, int select) { // call after Pull3Random, returns the select of the 3

        Sprite newSprite;

        // set up the ahead tile to take data from the Room obj
        Room tileSelected = tilePulled[select];
        tileSelected.angle = tilePulledAngle[select];

        tilesSorted[x, y].GetComponent<RoomInfo>().SetRoom(tileSelected, tilePulledAngle[select], true); 

        // sets the tileSelected's sprite, sets and rotates it
        newSprite = sprites[tileSelected.spriteID];
        tilesSorted[x, y].GetComponent<SpriteRenderer>().sprite = newSprite;
        tilesSorted[x, y].transform.Rotate(0f, 0f, tilePulledAngle[select], Space.Self);

        // clear GUI tiles
        newSprite = defaultSprite;
        for (int tile = 0; tile < 3; tile++)
        {
            guiTiles[tile].GetComponent<SpriteRenderer>().sprite = defaultSprite;
            guiTiles[tile].GetComponent<RoomInfo>().ClearRoom();
            guiTiles[tile].transform.rotation = Quaternion.identity;
        }

        // removes selected tile from the deck
        deckHandler.RemoveFromDeck(select);

    }

    public void ClearAll()
    {
        Sprite newSprite = defaultSprite;

        // deck reset functions
        deckHandler.ResetDeck();

        // 6x6 grid clearer
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                // can select sprite from sprites[]
                tilesSorted[x, y].GetComponent<SpriteRenderer>().sprite = defaultSprite;
                tilesSorted[x, y].GetComponent<RoomInfo>().ClearRoom();
                tilesSorted[x, y].transform.rotation = Quaternion.identity;
                tilesSorted[x, y].tag = "undrafted";
            }

        }

        // clear GUI tiles
        for (int tile = 0; tile < 3; tile++)
        {
            guiTiles[tile].GetComponent<SpriteRenderer>().sprite = defaultSprite;
            guiTiles[tile].GetComponent<RoomInfo>().ClearRoom();
            guiTiles[tile].transform.rotation = Quaternion.identity;
        }

    }


    // gets the exits of a specific tile - these were not yet implemented in final build
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
