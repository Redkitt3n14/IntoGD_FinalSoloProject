using UnityEngine;

namespace RoomStruct
{
    public struct Interact // this stores anything in the room that the player needs to interact with
    {
        public bool type; // 0 is text, 1 is item
        public string readable; // text
        public int pickup; // item
    }

    public struct Room
    {
        // variable declaration
        public bool inDeck;
        public bool doorA;
        public bool doorB;
        public bool doorC;
        public bool doorD;
        public int spriteID;
        public Interact[] interacts;

    }

}
