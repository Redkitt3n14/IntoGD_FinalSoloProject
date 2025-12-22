using UnityEngine;
using RoomStruct;

public class RoomInfo : MonoBehaviour
{
    // sets the true angle, the 90 rot count, and itself room details
    private Room self;


    // room setter uppers
    public void SetRoom(Room roomIn, float angleIn, bool doOffset)
    {
        // basic setup
        self = roomIn;
        self.angle = angleIn;
        self.doorways = new bool[4];

        self.rotation = Mathf.FloorToInt(angleIn / 90) % 4; //  mod 4 stops it from being too high if >360 rotation

        if (doOffset)
        {
            // this offsets the self room's saved doors by the rotation from original
            for (int r = 0; r < 4; r++)
            {
                if (r + self.rotation < 4) // only if not out range
                {
                    self.doorways[r + self.rotation] = roomIn.doorways[r];
                }
                else // stops it going out of range
                {
                    self.doorways[r + self.rotation - 4] = roomIn.doorways[r];
                }
            }
        }
        else // version with no rotating for if taken from a prerotated room object
        {
            self.doorways = roomIn.doorways;
        }

    }
    public void SetAngle(int angleIn)
    {
        self.angle = angleIn;
        self.rotation = angleIn / 90;
    }
    public void ClearRoom()
    {
        self = new Room();
    }


    // getters

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
 
    public float GetRotation()
    {
        return self.rotation;
    }
    public float GetAngle()
    {
        return self.angle;
    }
    public int GetSpriteID()
    {
        return self.spriteID;
    }
}
