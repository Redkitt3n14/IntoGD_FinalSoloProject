using UnityEngine;
using RoomStruct;

public class RoomInfo : MonoBehaviour
{
    // sets the true angle, the 90 rot count, and itself room details
    private float angle;
    private int rotation;
    private Room self;


    // room setter uppers
    public void SetRoom(Room roomIn, float angleIn)
    {
        // basic setup
        self = roomIn;
        angle = angleIn;

        rotation = Mathf.FloorToInt(angleIn / 90);

        // this offsets the self room's saved doors by rotation from original
        for (int r = 0; r < 4; r++) 
        {
            if (r + rotation < 4) // only if not out range
            {
                self.doorways[r + rotation] = roomIn.doorways[r];
            }
            else // stops it going out of range
            {
                self.doorways[r + rotation - 4] = roomIn.doorways[r];
            }
        }

    }

    // the 4 doorway getters
    public bool GetNorth()
    {
        return self.doorways[0];
    }
    public bool GetEast()
    {
        return self.doorways[1];
    }
    public bool GetSouth()
    {
        return self.doorways[2];
    }
    public bool GetWest()
    {
        return self.doorways[3];
    }
    public int GetSpriteID()
    {
        return self.spriteID;
    }
}
