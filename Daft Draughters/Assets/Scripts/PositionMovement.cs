using UnityEngine;

public class PositionMovement : MonoBehaviour
{

    private PlayerControls playerControls;

    public bool fullView = false; // bool for if zoomed in or out - public so 
    public bool userControl = true; // can be turned to false when animation is played

    

    private int playerX;
    private int playerY;

    public float speed;

    public DrawTile drawTile;
    private GameObject playerPin;
    private GameObject playerCamera;

    // walk testing variables
    Vector2[] walkTemp = {
        new Vector2(0,0),
        new Vector2(0,1),
        new Vector2(1,0),
        new Vector2(1,0),
        new Vector2(0,-1),
        new Vector2(1,0),
        new Vector2(-1,0),
        new Vector2(-1,0),
        new Vector2(0,1),
        new Vector2(-1,0),
        new Vector2(0,1),
        new Vector2(0,-1),
        new Vector2(1,0),
    };


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
        playerCamera = transform.GetChild(0).gameObject;
        drawTile = transform.GetChild(1).gameObject.GetComponent<DrawTile>();
        playerPin = transform.GetChild(2).gameObject;

        // TEMP start position - do random side, random 0-5 for end implement 
        playerX = 2;
        playerY = 0;
        drawTile.Draw(playerX, playerY); // does draw of first tile
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
                fullView = false; // sets to false toggle for movement enabling

            }
            else if ((playerControls.Walking.ZoomToggle.triggered && !fullView) || playerControls.Walking.Zoom.ReadValue<float>() < 0)
            {
                fullView = true; // sets to true toggle for movement blocking

            }






            // moving function temp - only 1 of X or Y can move at a time

            if (move.x > 0) // right
            {
                if (playerX < 5)
                {

                    playerX++;
                    drawTile.Draw(playerX, playerY);
                }
                else // backup stops the player escaping
                {
                    playerX = 5;
                }
            }
            else if (move.x < 0) // left
            {
                if (playerX > 0)
                {
                    playerX--;
                    drawTile.Draw(playerX, playerY);
                }
                else // backup stops the player escaping
                {
                    playerX = 0;
                }
            }
            else if (move.y > 0) // up
            {
                if (playerY < 5)
                {
                    playerY++;
                    drawTile.Draw(playerX, playerY);
                }
                else // backup stops the player escaping
                {
                    playerY = 5;
                }
            }
            else if (move.y < 0) // down
            {
                if (playerY > 0)
                {
                    playerY--;
                    drawTile.Draw(playerX, playerY);
                }
                else // backup stops the player escaping
                {
                    playerY = 0;
                }
            }

            playerPin.transform.position = new Vector3(playerX, playerY, -0.5f);

            // calls drawtile to do a new tile pick at the position

            //Debug.Log($"at X {playerX} Y {playerY}");

        }
    }

}
