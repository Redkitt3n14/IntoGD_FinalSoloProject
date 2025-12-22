using UnityEngine;
using UnityEngine.Timeline;

public class PlayerActionHandler : MonoBehaviour
{

    private PlayerControls playerControls;

    [SerializeField] private bool fullView = false; // bool for if zoomed in or out - public so 
    [SerializeField] private bool userControl = true; // can be turned to false when animation is played



    private int playerX;
    private int playerY;
    private int moved = 0;

    [SerializeField] private float speed;

    [SerializeField] private TileControl tileControl;
    private GameObject playerPin;
    private Camera playerCamera;

    [SerializeField] private Animator nailsAnim;
    [SerializeField] private GameObject gameGrid;
    [SerializeField] private GameObject pseudoGrid;


    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    // control enablers & disablers
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // gets the camera, the script of tileControl, and the player position pin
        playerCamera = transform.GetChild(0).GetComponent<Camera>();
        tileControl = transform.GetChild(1).gameObject.GetComponent<TileControl>();
        playerPin = transform.GetChild(2).gameObject;

        // TEMP start position - do random side, random 0-5 for end implement 
        int startPos = Random.Range(0, 5);
        switch(Random.Range(0, 3))
        {
            case 0: playerX = 0; playerY = startPos; break;
            case 1: playerX = 5; playerY = startPos; break;
            case 2: playerX = startPos; playerY = 0; break;
            case 3: playerX = startPos; playerY = 5; break;
        }
        
        tileControl.DrawStart(playerX, playerY); // does draw of first tile
        // zooms camera on initial tile
        playerCamera.transform.position = new Vector3(playerX, playerY, -10);
        playerCamera.fieldOfView = 12;
    }




    // Update is called once per frame
    void Update()
    {

        if (userControl == true) // ignores movement if locked (ie before a previous move finishes)
        {

            Vector2 move = new Vector2();

            if (playerControls.Walking.Interact.triggered)
            {
<<<<<<< Updated upstream
                move = playerControls.Walking.ChangeRoom.ReadValue<Vector2>();
                //Debug.Log("move {move.x} {move.y}");
            }
=======
                Vector2 move = new Vector2();

                if (playerControls.Walking.Interact.triggered)
                {
                    move = playerControls.Walking.ChangeRoom.ReadValue<Vector2>();
                    
                }
>>>>>>> Stashed changes



            // zoom in / out function
            if ((playerControls.Walking.ZoomToggle.triggered && fullView) || playerControls.Walking.Zoom.ReadValue<float>() > 0)
            {
                ZoomIn();

            }
            else if ((playerControls.Walking.ZoomToggle.triggered && !fullView) || playerControls.Walking.Zoom.ReadValue<float>() < 0)
            {
                ZoomOut();

            }






            // moving function temp - only 1 of X or Y can move at a time

            if (move.y > 0 && tileControl.tilesSorted[playerX, playerY].GetComponent<RoomInfo>().GetNorth()) // up / north (only if there is a north door)
            {
                if (playerY < 5)
                {
                    if (!tileControl.CheckDrafted(playerX, playerY + 1)) // is the tile above not drafted?
                    {
                        playerY++;
                        moved = 3;
                        Debug.Log("North, undrafted");
                    }
                    else if (tileControl.tilesSorted[playerX, playerY + 1].GetComponent<RoomInfo>().GetSouth()) // if drafted, does it have south doora
                    {
                        playerY++;
                        moved = 3;
                        Debug.Log("North, drafted");
                    }
                }
                else if (playerY > 5) // backup stops the player escaping
                {
                    playerY = 5;
                }
            }
            else if (move.y < 0 && tileControl.tilesSorted[playerX, playerY].GetComponent<RoomInfo>().GetSouth()) // down / south (only if there is a south door)
            {
                if (playerY > 0)
                {
                    if (!tileControl.CheckDrafted(playerX, playerY - 1)) // is the tile below not drafted?
                    {
                        playerY--;
                        moved = 1;
                        Debug.Log("South, undrafted");
                    }
                    else if (tileControl.tilesSorted[playerX, playerY - 1].GetComponent<RoomInfo>().GetNorth()) // if drafted, does it have north door
                    {
                        playerY--;
                        moved = 1;
                        Debug.Log("South, drafted");
                    }
                }
                else if (playerY < 0) // backup stops the player escaping
                {
                    playerY = 0;
                }
            }
            else if (move.x > 0 && tileControl.tilesSorted[playerX, playerY].GetComponent<RoomInfo>().GetEast()) // right / east (only if there is a east door)
            {
                if (playerX < 5)
                {
                    if (!tileControl.CheckDrafted(playerX + 1, playerY)) // is the tile to right not drafted?
                    {
                        playerX++;
                        moved = 2;
                        Debug.Log("East, undrafted");
                    }
                    else if (tileControl.tilesSorted[playerX + 1, playerY].GetComponent<RoomInfo>().GetWest()) // if drafted, does it have west door
                    {
                        playerX++;
                        moved = 2;
                        Debug.Log("East, drafted");
                    }
                }
                else if (playerX > 5)// backup stops the player escaping
                {
                    playerX = 5;
                }
            }
            else if (move.x < 0 && tileControl.tilesSorted[playerX, playerY].GetComponent<RoomInfo>().GetWest()) // left / west (only if there is a west door)
            {
                if (playerX > 0)
                {
                    if (!tileControl.CheckDrafted(playerX - 1, playerY)) // is the tile to west not drafted?
                    {
                        playerX--;
                        moved = 4;
                        Debug.Log("West, undrafted");
                    }
                    else if (tileControl.tilesSorted[playerX - 1, playerY].GetComponent<RoomInfo>().GetEast()) // if drafted, does it have east door
                    {
                        playerX--;
                        moved = 4;
                        Debug.Log("West, drafted");
                    }
                }
                else if (playerX < 0) // backup stops the player escaping
                {
                    playerX = 0;
                }
            }


            playerPin.transform.position = new Vector3(playerX, playerY, -0.5f);

            // calls tileControl to do a new tile pick at the position

            //Debug.Log($"at X {playerX} Y {playerY}");

            if (moved > 0) // calls tile draw if move attempt (move direction: no = 0, down = 1, left = 2, up = 3, right = 4)
            {
                if (!tileControl.CheckDrafted(playerX, playerY)) // if not, the tile is undrafted
                {
                    tileControl.Draw(playerX, playerY, moved);
                }
                


                if (!fullView) // adjusts camera to new room if player is in zoomed view
                {
                    playerCamera.transform.position = new Vector3(playerX, playerY, -10);
                    playerCamera.fieldOfView = 12;
                }

                moved = 0; // unsets for next use
            }



            // this function will reset the level to empty
            if (playerControls.Walking.Pause.triggered) //TEMP on ESC key for nail testing
            {
                ResetLevel();
            }

        }
    }







    // drops the previous map and makes a new one
    // this function drops out the real map, drops in a fake new map, clears real map, then teleports real map back up and hides fake new map
    void ResetLevel()
    {
        userControl = false;
        ZoomOut();

        RemoveNail();

        Invoke(nameof(DropPage), 2.25f);
        
        Invoke(nameof(RecoverPage), 4.5f);

        Invoke(nameof(SetNail), 3.25f); // not sure why but the nail anims take ages to trigger
        Invoke(nameof(ResetPlayer), 6.5f);

        // set nail also sets userControl back to true


        // TEMP start position - do random side, random 0-5 for end implement 
        int startPos = Random.Range(0, 6);
        switch (Random.Range(0, 4))
        {
            case 0: playerX = 0; playerY = startPos; break;
            case 1: playerX = 5; playerY = startPos; break;
            case 2: playerX = startPos; playerY = 0; break;
            case 3: playerX = startPos; playerY = 5; break;
        }

    }




    // these functions are all to do with resetting the grid --------------------------------
    void DropPage() // drops the game screen by activating it's rigidbody, and tells fake page to descend
    {
        gameGrid.GetComponent<Rigidbody>().useGravity = true;

        // makes the pseudo grid begin its controlled descent
        pseudoGrid.GetComponent<PseudoDescentControl>().BeginDescent();
    }

    void RecoverPage() // pulls the real page back out of the void - turns off gravity
    {
        tileControl.ClearAll(); // resets original board

        gameGrid.GetComponent<Rigidbody>().useGravity = false;
        gameGrid.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        gameGrid.transform.position = new Vector3(0, 0, 0);

        // makes the pseudo grid disappear now real is back
        pseudoGrid.GetComponent<PseudoDescentControl>().ResetPseudo();
    }

    void RemoveNail()
    {
        nailsAnim.SetBool("RemoveNail", true);

        playerPin.transform.position = new Vector3(playerX, playerY, 2.5f); // hides the pin
    }

    void SetNail() // starts the nail reapplication animation
    {
        nailsAnim.SetBool("RemoveNail", false);

        
    }

    void ResetPlayer()
    {
        tileControl.DrawStart(playerX, playerY);
        userControl = true; // give user control now page has returned
    }

    // end of grid resetting functions --------------------------------------------------------

    // functions to do with camera control ----------------------------------------------------

    void ZoomIn()
    {
        fullView = false; // sets to false toggle for movement enabling - now in zoomed view

        playerCamera.transform.position = new Vector3(playerX, playerY, -10);
        playerCamera.fieldOfView = 12;
    }
    void ZoomOut()
    {
        fullView = true; // sets to true toggle for movement blocking - now in full map view

        playerCamera.transform.position = new Vector3(2.5f, 2.5f, -26);
        playerCamera.fieldOfView = 16;
    }

    void CameraMove()
    {

    }

    // end of camera move functions ---------------------------------------------------------
}
