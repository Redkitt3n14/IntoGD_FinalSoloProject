using UnityEngine;
using System.Collections.Generic; // for rooms list
using RoomStruct;

public class DeckHandler : MonoBehaviour
{
    // tracks number of rooms available
    private int roomTotal;

    // list of all rooms that can be drafted
    private List<Room> rooms;

    // tracks which list items are currently out for selection
    private int[] randTracker = { -1, -1, -1 };
    private int randNum = -1;


    // Functions ----------------------------------------------------------------

    // PullRandom drafts you a room - which of the 3 rooms is currently beign drawn, and does it have to be an indoor / outdoor tile
    Room PullRandom(bool outLock, bool inLock, int pullTracker)
    {
        randNum = Random.Range(0, rooms.Count); // takes a random int from 0 to number of available rooms

        randTracker[pullTracker] = randNum; // sets the tracker at the pull position to the list position - since 3 rooms are pulled
        return rooms[randNum];
    }

    void RemoveFromDeck(int pullTracker) // removes the tile that the user selected from deck
    {
        rooms.RemoveAt(pullTracker);

        // resets 
        randTracker[0] = -1;
        randTracker[1] = -1;
        randTracker[2] = -1;
    }
    
    // functions to add more tiles to deck if player has the tools to draft them
    void AddProtractor() // adds tiles that require protractor to deck
    {

    }

    void AddStraightEdge() // adds tiles that require protractor to deck
    {

    }

    void ResetDeck() // refills deck to basic version, removes items from player
    {
        
        CompileDeck();
    }


    // DECK LIST ----------------------------------------------------------------------

    void CompileDeck() {

        // reset list and room counter, and random trackers
        roomTotal = 0;
        List<Room> rooms = new List<Room>();
        randTracker[0] = -1;
        randTracker[1] = -1;
        randTracker[2] = -1;
        randNum = -1;

        for (; roomTotal < roomTotal+6; roomTotal++)  // adds 5 blank quad rooms to room total
        {

            new Room() // standard quad room
            {
                inDeck = true,
                doorA = true,
                doorB = true,
                doorC = true,
                doorD = true,
                spriteID = 0,
                interacts = { },
            };
        }

        for (; roomTotal < roomTotal+10; roomTotal++)  // adds 8 blank branch rooms to room total
        {

            new Room() // standard quad room
            {
                inDeck = true,
                doorA = true,
                doorB = true,
                doorC = true,
                doorD = false,
                spriteID = 1,
                interacts = { },
            };
        }

        for (; roomTotal < roomTotal + 10; roomTotal++) // adds 8 blank corner rooms to room total
        {

            new Room() // standard quad room
            {
                inDeck = true,
                doorA = true,
                doorB = true,
                doorC = false,
                doorD = false,
                spriteID = 2,
                interacts = { },
            };
        }

        for (; roomTotal < roomTotal + 8; roomTotal++) // adds 6 blank straight rooms to room total
        {

            new Room() // standard quad room
            {
                inDeck = true,
                doorA = true,
                doorB = false,
                doorC = true,
                doorD = false,
                spriteID = 3,
                interacts = { },
            };
        }

        for (; roomTotal < roomTotal + 10; roomTotal++) // adds 8 blank end rooms to room total
        {

            new Room() // standard quad room
            {
                inDeck = true,
                doorA = true,
                doorB = false,
                doorC = false,
                doorD = false,
                spriteID = 4,
                interacts = { },
            };
        }
    }
    
}
