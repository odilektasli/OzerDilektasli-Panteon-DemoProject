using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class OpponentCharacterHandler : MonoBehaviour
{

    public ManagerSOScript managerSO;
    public int dragMagnitude;
    public int platformXOffset;
    public float movementSpeed;

    private Animator playerAnim;
    private Rigidbody playerRigidBody;
    public GameObject targetArea;
    public bool canMove, isRotationFix, isRotatingPlatformFinished, isLerpEnabled, isRotationLepEnabled, isRotatingPlatformArea;
    private float platformAngularVelocity;

    Vector3 mousePosition;
    float initialMousePositionX;
    float currentMousePositionX;
    float mouseDragDistance;
    private int rotationDirection;
    private Vector3 rotationAroundPosition;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private NavMeshAgent navMeshRef;
    NavMeshPath path;
    LineRenderer lineRenderer;

    private float lerpPositionX;


    private RaycastHit raycastHit;
    private Ray ray;
    public int rayCastRange;

    private void Awake()
    {

        playerAnim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
        navMeshRef = GetComponent<NavMeshAgent>();
        movementSpeed = navMeshRef.speed;

        initialPosition = transform.position;
        initialRotation = transform.rotation;


    }
    // Start is called before the first frame update

    void Start()
    {

        canMove = true;
        playerAnim.SetBool("isRunState", true);

        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLerpEnabled && canMove && !isRotatingPlatformArea)
        {
            Debug.Log("aloo?");
            navMeshRef.destination = targetArea.transform.position;
        }

        if (isLerpEnabled)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(lerpPositionX, transform.position.y, transform.position.z), Time.deltaTime * 8f);


        }


        if (isLerpEnabled && Vector3.Distance(transform.position, new Vector3(lerpPositionX, transform.position.y, transform.position.z)) < 0.5f || Vector3.Distance(transform.position, new Vector3(lerpPositionX, transform.position.y, transform.position.z)) < -0.5f)
        {
            isLerpEnabled = false;
        }

        if(isRotatingPlatformArea)
        {
            navMeshRef.Warp(transform.position);

            if (transform.rotation.z > 0.3 || transform.rotation.z < -0.3)
            {
                isRotationFix = true;
            }

            else
            {
                isRotationFix = false;
            }
        }

        if (isRotationFix)
        {
            transform.RotateAround(rotationAroundPosition, rotationDirection * -1 * Vector3.forward, 300f * Time.deltaTime);


        }

        //Debug.Log(Vector3.Distance(transform.position, new Vector3(lerpPositionX, transform.position.y, transform.position.z)));
    }

    private void FixedUpdate()
    {
        //Setting of character velocity and camera positions.

        if (!isRotatingPlatformArea)
        {
            //ray = new Ray(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Vector3.forward);
            //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Vector3.forward * rayCastRange, Color.red);

            //Debug.DrawRay(transform.position, Vector3.forward);
            //Debug.DrawWireCube(transform.position + transform.forward * rayCastRange, transform.lossyScale);
            //if (Physics.BoxCast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.lossyScale, Vector3.forward * rayCastRange, out raycastHit, transform.rotation, rayCastRange) && !isLerpEnabled)
            //{
            //    Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Vector3.forward * rayCastRange, Color.yellow);
            //    Debug.Log(raycastHit.collider.transform.position);
            //    Debug.Log(raycastHit.collider.bounds.extents.x);
            //    if (raycastHit.collider.name.Contains("Horizontal"))
            //    {
            //        Debug.Log("gorduuuuuuuuuum");

            //        if (platformXOffset - raycastHit.collider.transform.position.x < raycastHit.collider.transform.position.x - (platformXOffset * -1))
            //        {
            //            lerpPositionX = platformXOffset;
            //        }

            //        else
            //        {
            //            lerpPositionX = platformXOffset * -1;
            //        }

            //        isLerpEnabled = true;
            //    }
            //}


        }

        else if(isRotatingPlatformArea)
        {
            playerRigidBody.velocity = new Vector3(0, 0, movementSpeed);

        }




    }


    private void StaticObstacleHit()
    {
        navMeshRef.speed = 0;
        canMove = false;
        playerAnim.SetBool("isFallState", true);
        playerAnim.SetBool("isRunState", false);
        StartCoroutine(WaitFallAnimation());


        //transform.position = new Vector3(0, 0, 0);
    }

    IEnumerator WaitFallAnimation()
    {
        playerRigidBody.detectCollisions = false;
        yield return new WaitForSeconds(1f);

        managerSO.GetPooledObject(transform.position, 1);
        transform.position = initialPosition;
        playerAnim.SetBool("isFallState", false);
        playerAnim.SetBool("isRunState", true);
        navMeshRef.speed = movementSpeed;
        canMove = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RotatingPlatform")
        {
            movementSpeed = 2f;
            platformAngularVelocity = other.attachedRigidbody.angularVelocity.z;
            rotationAroundPosition = other.transform.position;
            //StartCoroutine(FixRotation());

        }

        else if (other.gameObject.tag == "RotatingPlatformArea")
        {
            navMeshRef.updatePosition = false;
            isRotatingPlatformArea = true;
            canMove = false;
            isLerpEnabled = true;
            lerpPositionX = 0;
        }

        else if (other.gameObject.tag == "FinishArea")
        {
            canMove = false;
            //managerSO.ActivatePaintingWall();
        }

        else if(other.gameObject.tag == "HorizontalObstacle")
        {
            managerSO.GetPooledObject(transform.position, 0);
      
            StaticObstacleHit();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "RotatingPlatform")
        {
            if (platformAngularVelocity > 0)
            {
                rotationDirection = 1;
            }
            else { rotationDirection = -1; }

            if (!isRotationFix)
            transform.RotateAround(rotationAroundPosition, rotationDirection * Vector3.forward, 10f * Time.deltaTime);


            if (transform.rotation.z > 0.5 || transform.rotation.z < -0.5)
            {
                managerSO.GetPooledObject(transform.position, 1);
                transform.position = initialPosition;
                isRotatingPlatformFinished = true;

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RotatingPlatform")
        {

            isRotationFix = false;
            isRotatingPlatformFinished = true;
        }

        if (other.gameObject.tag == "RotatingPlatformArea")
        {
            movementSpeed = 5f;
            lerpPositionX = 0;
            //transform.rotation = initialRotation;
            isRotatingPlatformFinished = false;
            //navMeshRef.enabled = true;
            isRotatingPlatformArea = false;
            isRotationLepEnabled = true;

            navMeshRef.updatePosition = true;

            canMove = true;

        }
    }

}
