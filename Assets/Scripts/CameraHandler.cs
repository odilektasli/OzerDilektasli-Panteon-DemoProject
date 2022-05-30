using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public ManagerSOScript managerSO;
    public GameObject playerRef;

    public float initializationSpeed;

    private bool isPoisitonInitialization;
    private bool trackPlayer;
    private bool isParticlePlaced;
    private bool isGoToPosition;
    private bool isRotationLerp;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 lerpPosition;
    // Start is called before the first frame update

    private void Awake()
    {
        managerSO.InitializeCameraPositionEvent += StartPositionInitialization;
        managerSO.TrackPlayerEvent += ActivatePlayerTracking;
        managerSO.LerpToPositionEvent += GoToPosition;
    }
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPoisitonInitialization)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, initializationSpeed * Time.deltaTime);
        }

        if(transform.position.z <= 0.1 && isPoisitonInitialization && !isParticlePlaced)
        {
            managerSO.GetPooledObject(playerRef.transform.position, 1);
            isParticlePlaced = true;
        }

        if (transform.position.z - initialPosition.z <= 0.1f && isPoisitonInitialization)
        {
            isPoisitonInitialization = false;
            managerSO.CameraUpdateFinished();
        }

        if(isGoToPosition)
        {
            transform.position = Vector3.Lerp(transform.position, lerpPosition, initializationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, initializationSpeed * Time.deltaTime);

        }


    }

    private void FixedUpdate()
    {
        if (trackPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, playerRef.transform.position.z - 3), 2.0f * Time.deltaTime);
        }
    }


    private void StartPositionInitialization()
    {
        isPoisitonInitialization = true;
        isParticlePlaced = false;
        trackPlayer = false;

    }

    private void ActivatePlayerTracking(bool isTrackMode)
    {
        if (isTrackMode) { trackPlayer = true; }
        else { trackPlayer = false; }
    }

    private void GoToPosition(Vector3 position)
    {
        Debug.Log(position);
        trackPlayer = false;
        lerpPosition = position;
        isGoToPosition = true;
    }

    private void OnDisable()
    {
        managerSO.InitializeCameraPositionEvent -= StartPositionInitialization;
        managerSO.TrackPlayerEvent -= ActivatePlayerTracking;
        managerSO.LerpToPositionEvent -= GoToPosition;
    }
}
