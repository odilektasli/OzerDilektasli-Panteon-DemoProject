using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public ManagerSOScript managerSO;
    public int dragMagnitude;
    public int platformXOffset;
    public float movementSpeed;

    private Animator playerAnim;
    private Rigidbody playerRigidBody;
    public bool canMove, isCharacterRotating, isRotatingPlatformFinished;
    private float platformAngularVelocity;

    Vector3 mousePosition;
    float initialMousePositionX;
    float currentMousePositionX;
    float mouseDragDistance;
    private int rotationDirection;
    private Vector3 rotationAroundPosition;

    private Vector3 initialPosition;
    private Quaternion initialRotation;


    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();

        managerSO.ObstacleHitEvent += StaticObstacleHit;
        managerSO.CameraUpdateFinishedEvent += CameraUpdateFinished;

        initialPosition = transform.position;
        initialRotation = transform.rotation;

    }
    // Start is called before the first frame update

    void Start()
    {
        playerAnim.SetBool("isRunState", true);
        managerSO.CameraTrackPlayerMode(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove)//Here we take first position of mouse when clicked as world point
        {
            initialMousePositionX = GetWorldMousePositionX();
        }

        if (Input.GetMouseButton(0) && canMove && !isCharacterRotating)//After first click we look for if mouse is dragged on x axis. 
        {
            currentMousePositionX = GetWorldMousePositionX();

            mouseDragDistance = (currentMousePositionX - initialMousePositionX) * dragMagnitude; // To change location faster we need a multiplier.

            if (mouseDragDistance != 0)//If there is difference between after first click and current position of mouse we change the location of character on x axis.
            {
                transform.position = new Vector3( Mathf.Clamp(transform.position.x + mouseDragDistance, managerSO.platformXOffset * -1, managerSO.platformXOffset), transform.position.y, transform.position.z);
                initialMousePositionX = currentMousePositionX; //When character location is changed that means inital position is now current position
            }

        }

        if (Input.GetMouseButton(0) && isCharacterRotating)//After first click we look for if mouse is dragged on x axis. 
        {
            currentMousePositionX = GetWorldMousePositionX();

            mouseDragDistance = (currentMousePositionX - initialMousePositionX) * dragMagnitude; // To change location faster we need a multiplier.

            if (mouseDragDistance != 0)//If there is difference between after first click and current position of mouse we change the location of character on x axis.
            {
                transform.RotateAround(rotationAroundPosition, rotationDirection * -1 * Vector3.forward, 200f * Time.deltaTime);
                initialMousePositionX = currentMousePositionX; //When character location is changed that means inital position is now current position
            }

        }

        //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 10f), 2.0f * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //Setting of character velocity and camera positions.
        if (canMove)
        {
            playerRigidBody.velocity = new Vector3(0,0 , movementSpeed);

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
        playerAnim.SetBool("isRunState", false);
        StartCoroutine(WaitFallAnimation());


        //transform.position = new Vector3(0, 0, 0);
        //Camera.main.transform.position = initialCameraPosition;
    }

    IEnumerator WaitFallAnimation()
    {
        canMove = false;
        playerRigidBody.detectCollisions = false;
        managerSO.CameraTrackPlayerMode(false);
        yield return new WaitForSeconds(1f);

        managerSO.GetPooledObject(transform.position, 1);
        managerSO.InitializeCameraPosition();
        transform.position = initialPosition;
        playerAnim.SetBool("isFallState", false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RotatingPlatform")
        {
            movementSpeed = 2f;
            platformAngularVelocity = other.attachedRigidbody.angularVelocity.z;
            rotationAroundPosition = other.transform.position;
        }

        else if(other.gameObject.tag == "RotatingPlatformArea")
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(initialPosition.x, initialPosition.y, transform.position.z), 100f);
            
        }

        else if(other.gameObject.tag == "FinishArea")
        {
            canMove = false;
            managerSO.ActivatePaintingWall();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "RotatingPlatform")
        {
           if(platformAngularVelocity > 0)
           {
                rotationDirection = 1;
           }
           else { rotationDirection = -1; }

           if(!Input.GetMouseButtonDown(0))
           transform.RotateAround(rotationAroundPosition, rotationDirection * Vector3.forward, 50f * Time.deltaTime);

            isCharacterRotating = true;

            if (transform.rotation.z > 0.5 || transform.rotation.z < -0.5)
            {
                managerSO.GetPooledObject(transform.position, 1);
                managerSO.InitializeCameraPosition();
                transform.position = initialPosition;
                isRotatingPlatformFinished = true;

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RotatingPlatform")
        {

            isCharacterRotating = false;
            isRotatingPlatformFinished = true;
        }

        if(other.gameObject.tag == "RotatingPlatformArea")
        {
            movementSpeed = 5f;
            transform.position = new Vector3(0, 0, transform.position.z);
            transform.rotation = initialRotation;
            isRotatingPlatformFinished = false;
        }
    }

    private void CameraUpdateFinished()
    {
        playerAnim.SetBool("isRunState", true);
        canMove = true;
        managerSO.CameraTrackPlayerMode(true);
        playerRigidBody.detectCollisions = true;
    }
    private void OnDisable()
    {
        managerSO.ObstacleHitEvent -= StaticObstacleHit;
        managerSO.CameraUpdateFinishedEvent -= CameraUpdateFinished;
    }
}
