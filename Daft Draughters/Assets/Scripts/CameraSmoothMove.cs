using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraSmoothMove : MonoBehaviour
{

    public float speedDivBase = 1.5f;
    public float speedDivRamp = 1.5f;
    private float speedDiv;

    // the Y positions it aims for
    private float endX = 0f;
    private float endY = 0f;
    private float endZ = 0f;
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
    public void BeginCamMove(float endX, float endY, bool zoomOut)
    {
        
        moving = true;
        timePass = 0;

        endZ = -10f;

        if (zoomOut)
        {
            endZ = -26f;
        }

        speedDiv = speedDivBase;

        if (moving) // increases speed if it hadnt reached previous destination yet
        {
            speedDiv = speedDivBase / speedDivRamp;
        }

        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z); // current pos
        endPos = new Vector3(endX, endY, endZ); // position passed in

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

            }
        }
    }

}
