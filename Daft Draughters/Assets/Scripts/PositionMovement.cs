using UnityEngine;

public class PositionMovement : MonoBehaviour
{

    private PlayerControls playerControls;

    public bool fullView = false; // bool for if zoomed in or out - public so 
    public bool userControl = true; // can be turned to false when animation is played

    

    private int playerX;
    private int playerY;
    private int moved = 0;

    public float speed;

    public DrawTile drawTile;
    private GameObject playerPin;
    private Camera playerCamera;


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
        // gets the camera, the script of drawtile, and the player position pin
        playerCamera = transform.GetChild(0).GetComponent<Camera>();
        drawTile = transform.GetChild(1).gameObject.GetComponent<DrawTile>();
        playerPin = transform.GetChild(2).gameObject;

        // TEMP start position - do random side, random 0-5 for end implement 
        playerX = 2;
        playerY = 0;
        drawTile.DrawStart(playerX, playerY); // does draw of first tile
    }




    // Update is called once per frame
    void Update()
    {

        if (userControl) // ignores movement if locked (ie before a previous move finishes)
        {

            Vector2 move = new Vector2();

            if (playerControls.Walking.Interact.triggered)
            {
                move = playerControls.Walking.ChangeRoom.ReadValue<Vector2>();
                //Debug.Log("move {move.x} {move.y}");
            }


            if (playerControls.Walking.Interact.triggered || playerControls.Walking.Zoom.ReadValue<float>() > 0)
            {
                Debug.Log("Successful Interact Trigger"); // sets to false toggle for movement enabling
                Debug.Log($"at X {playerX} Y {playerY}");
            }


            // zoom in / out function
            if ((playerControls.Walking.ZoomToggle.triggered && fullView) || playerControls.Walking.Zoom.ReadValue<float>() > 0)
            {
                fullView = false; // sets to false toggle for movement enabling - now in zoomed view

                playerCamera.transform.position = new Vector3(playerX, playerY, -10);
                playerCamera.fieldOfView = 12;

            }
            else if ((playerControls.Walking.ZoomToggle.triggered && !fullView) || playerControls.Walking.Zoom.ReadValue<float>() < 0)
            {
                fullView = true; // sets to true toggle for movement blocking - now in full map view

                playerCamera.transform.position = new Vector3(2.5f, 2.5f, -26);
                playerCamera.fieldOfView = 16;

            }






            // moving function temp - only 1 of X or Y can move at a time

            if (move.y > 0) // up / north
            {
                if (playerY < 5)
                {
                    playerY++;
                    moved = 1;
                }
                else // backup stops the player escaping
                {
                    playerY = 5;
                }
            }
            else if (move.y < 0) // down / south
            {
                if (playerY > 0)
                {
                    playerY--;
                    moved = 2;
                }
                else // backup stops the player escaping
                {
                    playerY = 0;
                }
            }
            else if (move.x > 0) // right / east
            {
                if (playerX < 5)
                {

                    playerX++;
                    moved = 3;
                }
                else // backup stops the player escaping
                {
                    playerX = 5;
                }
            }
            else if (move.x < 0) // left / west
            {
                if (playerX > 0)
                {
                    playerX--;
                    moved = 4;
                }
                else // backup stops the player escaping
                {
                    playerX = 0;
                }
            }
            

            playerPin.transform.position = new Vector3(playerX, playerY, -0.5f);

            // calls drawtile to do a new tile pick at the position

            //Debug.Log($"at X {playerX} Y {playerY}");

            if (moved > 0) // calls tile draw if move attempt (no = 0, up = 1, down = 2, left = 3, right = 4)
            {
                drawTile.Draw(playerX, playerY);


                playerCamera.transform.position = new Vector3(playerX, playerY, -10);
                playerCamera.fieldOfView = 12;
                

                moved = 0; // unsets for next use
            }

        }
    }

}
