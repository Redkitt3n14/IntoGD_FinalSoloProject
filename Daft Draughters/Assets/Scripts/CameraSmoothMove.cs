using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraSmoothMove : MonoBehaviour
{

    public float speedDiv = 3.0f;

    // the Y positions it aims for
    public float endX = 0f;
    public float endY = 0f;
    private Vector3 startPos;
    private Vector3 endPos;

    // tracks the time of descent
    private float timePass;
    private float drag;
    private float partMove;
    // is it moving or disabled (for avoiding unnecesary updating)
    private bool moving;

    private PlayerActionHandler playerActHan;


    void Awake()
    {
        playerActHan = transform.parent.GetComponent<PlayerActionHandler>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BeginCamMove(int endX, int endY)
    {
        Debug.Log($"CAMERA MOVE CALL");
        GetComponent<Renderer>().enabled = true; // makes visible
        moving = true;
        timePass = 0;

        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z); // current pos
        endPos = new Vector3(endX, endY, transform.position.z); // position passed in

        //transform.position = startPos;
    }

    // Update is called once per frame - if moving, smoothsteps to get a psuedodrag timing, then applies it to lerp between start and end for smooth descent
    void Update()
    {
        if (moving)
        {

            timePass += Time.deltaTime / speedDiv;

            drag = Mathf.SmoothStep(0f, 1f, timePass);

            transform.position = Vector3.Lerp(startPos, endPos, drag);

            if (timePass >= 1) // overshoot protection
            {
                transform.position = endPos;
                moving = false;

                playerActHan.ReenableControl();
            }
        }
    }

}
