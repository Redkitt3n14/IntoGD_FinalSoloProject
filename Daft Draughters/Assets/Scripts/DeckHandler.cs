using UnityEngine;
using System.Collections.Generic; // for rooms list
using RoomStruct;

public class DeckHandler : MonoBehaviour
{    
    private int roomTotal;

    List<Room> rooms = new List<Room>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Room PullRandom(bool isOutdoor)
    {
        return rooms[0];
    }

    void RemoveFromDeck(int removePos)
    {

    }
    
    
    void AddProtractor()
    {

    }

    void AddStraightEdge()
    {

    }

    void ResetDeck()
    {

    }


    // DECK LIST ----------------------------------------------------------------------

    void CompileDeck() {

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
