using RoomStruct;
using System.Collections.Generic; // for rooms list
using UnityEngine;

public class DeckHandler : MonoBehaviour
{
    // tracks number of rooms available
    private int roomTotal;

    // list of all rooms that can be drafted
    private List<Room> rooms;
    Room room;

    // tracks which list items are currently out for selection
    private int[] randTracker = { -1, -1, -1 };
    private int randNum = -1;


    // Functions ----------------------------------------------------------------

    // PullRandom drafts you a room - which of the 3 rooms is currently beign drawn, and does it have to be an indoor / outdoor tile
    public Room PullRandom(bool outLock, bool inLock, int pullTracker)
    {
        randNum = Random.Range(0, rooms.Count); // takes a random int from 0 to number of available rooms

        randTracker[pullTracker] = randNum; // sets the tracker at the pull position to the list position - since 3 rooms are pulled
        return rooms[randNum];
    }

    public Room PullSelect(int select) // pulls a select room (such as for initial quad room)
    {
        return rooms[select];
    }

    public void RemoveFromDeck(int pullTracker) // removes the tile that the user selected from deck
    {

        int removal = randTracker[pullTracker];
        rooms.RemoveAt(removal);

        // resets 
        randTracker[0] = -1;
        randTracker[1] = -1;
        randTracker[2] = -1;
    }

    // functions to add more tiles to deck if player has the tools to draft them
    public void AddProtractor() // adds tiles that require protractor to deck
    {

    }

    public void AddStraightEdge() // adds tiles that require protractor to deck
    {

    }

    public void ResetDeck() // refills deck to basic version, removes items from player
    {
        
        CompileDeck();
    }


    // DECK LIST ----------------------------------------------------------------------


    private void CompileDeck() { // called by reset deck


        // reset list and room counter, and random trackers
        roomTotal = 0;
        int roomLock = 0;
        rooms = new List<Room>();
        randTracker[0] = -1;
        randTracker[1] = -1;
        randTracker[2] = -1;
        randNum = -1;

        
        room = new Room() // standard quad room
        {
            inDeck = true,
            doorways = new bool[4]{ true, true, true, true },
            spriteID = 0,
            interacts = new Interact[0],
        };
        roomLock = roomTotal + 5;
        for (; roomTotal < roomLock; roomTotal++)  // adds 5 blank quad rooms to room total
        {
            rooms.Add(room);
        }


        room = new Room() // standard branch room
        {
            inDeck = true,
            doorways = new bool[4] { true, true, true, false },
            spriteID = 1,
            interacts = new Interact[0],
        };
        roomLock = roomTotal + 10;
        for (; roomTotal < roomLock; roomTotal++)  // adds 8 blank branch rooms to room total
        {
            rooms.Add(room);
        }


        room = new Room() // standard corner room
        {
            inDeck = true,
            doorways = new bool[4] { true, true, false, false },
            spriteID = 2,
            interacts = new Interact[0],
        };
        roomLock = roomTotal + 10;
        for (; roomTotal < roomLock; roomTotal++) // adds 8 blank corner rooms to room total
        {
            rooms.Add(room);
        }


        room = new Room() // standard straight room
        {
            inDeck = true,
            doorways = new bool[4] { true, false, true, false },
            spriteID = 3,
            interacts = new Interact[0],
        };
        roomLock = roomTotal + 8;
        for (; roomTotal < roomLock; roomTotal++) // adds 6 blank straight rooms to room total
        {
            rooms.Add(room);
        }
        

        room = new Room() // standard end room
        {
            inDeck = true,
            doorways = new bool[4] { true, false, false, false },
            spriteID = 4,
            interacts = new Interact[0],
        };
        roomLock = roomTotal + 10;
        for (; roomTotal < roomLock; roomTotal++) // adds 8 blank end rooms to room total
        {
            rooms.Add(room);
        }

    }
    
}
