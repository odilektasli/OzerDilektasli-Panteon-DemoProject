﻿using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public int dragMagnitude;
    public int platformXOffset;
    public float movementSpeed;
    public ManagerSOScript managerSO;

    private Animator playerAnim;
    private Rigidbody playerRigidBody;
    private bool canMove = true;
    private Vector3 initialCameraPosition;
    Vector3 mousePosition;
    float initialMousePositionX;
    float currentMousePositionX;
    float mouseDragDistance;


    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
        initialCameraPosition = Camera.main.transform.position;

        managerSO.ObstacleHitEvent += StaticObstacleHit;

    }
    // Start is called before the first frame update

    void Start()
    {
        playerAnim.SetBool("isRunState", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//Here we take first position of mouse when clicked as world point
        {
            initialMousePositionX = GetWorldMousePositionX();
        }

        if (Input.GetMouseButton(0))//After first click we look for if mouse is dragged on x axis. 
        {
            currentMousePositionX = GetWorldMousePositionX();

            mouseDragDistance = (currentMousePositionX - initialMousePositionX) * dragMagnitude; // To change location faster we need a multiplier.

            if (mouseDragDistance != 0)//If there is difference between after first click and current position of mouse we change the location of character on x axis.
            {
                transform.position = new Vector3( Mathf.Clamp(transform.position.x + mouseDragDistance, managerSO.platformXOffset * -1, managerSO.platformXOffset), transform.position.y, transform.position.z);
                initialMousePositionX = currentMousePositionX; //When character location is changed that means inital position is now current position
            }

        }

        //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 10f), 2.0f * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //Setting of character velocity and camera positions.
        if(canMove)
        {

            playerRigidBody.velocity = new Vector3(0, 0, movementSpeed);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z - 10);
        }

    }

    /// <summary>
    /// Provides to convert mouse screen position, and returns mouse world mouse position of x
    /// </summary>
    private float GetWorldMousePositionX()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition).x;
    }

    private void StaticObstacleHit()
    {
        playerRigidBody.velocity = new Vector3(0, 0, 0);
        playerAnim.SetBool("isFallState", true);
        StartCoroutine(WaitFallAnimation());


        //transform.position = new Vector3(0, 0, 0);
        //Camera.main.transform.position = initialCameraPosition;
    }

    IEnumerator WaitFallAnimation()
    {
        canMove = false;
        yield return new WaitForSeconds(1f);

        transform.position = new Vector3(0, 0, 0);
        managerSO.InitializeCameraPosition(true);
        yield return new WaitForSeconds(2f);
        playerAnim.SetBool("isFallState", false);
        playerAnim.SetBool("isRunState", true);
        managerSO.InitializeCameraPosition(false);
        canMove = true;


    }

    private void OnDisable()
    {
        managerSO.ObstacleHitEvent -= StaticObstacleHit;
    }
}
