using UnityEngine;

public class PositionMovement : MonoBehaviour
{

    private PlayerControls playerControls;
    public bool fullView = false; // bool for if zoomed in or out - public so 
    public bool userControl = true; // can be turned to false when animation is played

    private int playerX;
    private int playerY;

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
        playerX = 2;
        playerY = 0;
    }




    // Update is called once per frame
    void Update()
    {
        Vector2 move = playerControls.Walking.ChangeRoom.ReadValue<Vector2>();
        Debug.Log(move);

        if (userControl) { 

        // zoom in / out function
        if ((playerControls.Walking.ZoomToggle.ReadValue<bool>() && fullView) || playerControls.Walking.Zoom.ReadValue<float>() > 0)
        {
            fullView = false; // sets to false toggle for movement enabling

        }
        else if ((playerControls.Walking.ZoomToggle.ReadValue<bool>() && !fullView) || playerControls.Walking.Zoom.ReadValue<float>() < 0)
        {
            fullView = true; // sets to true toggle for movement blocking
        }






        // moving function temp - only 1 of X or Y can move at a time
        if (move.x > 0) // right
            {
                if (playerX < 6)
                {
                    playerX++;
                }
                else // backup stops the player escaping
                {
                    playerX = 6; 
                }
            }
        else if (move.x < 0) // left
            {
                if (playerY > 0)
                {
                    playerX--;
                }
                else // backup stops the player escaping
                {
                    playerX = 0;
                }
            }
        else if (move.y > 0) // up
            {
                if (playerY < 6)
                {
                    playerY++;
                }
                else // backup stops the player escaping
                {
                    playerY = 6;
                }
            }
         else if (move.x < 0) // down
            {
                if (playerY > 0)
                {
                    playerY--;
                }
                else // backup stops the player escaping
                {
                    playerY = 0;
                }
            }


        }
}
