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
    private Vector3 initialPozition;
    // Start is called before the first frame update

    private void Awake()
    {
        managerSO.InitializeCameraPositionEvent += StartPositionInitialization;
        managerSO.TrackPlayerEvent += ActivatePlayerTracking;
    }
    void Start()
    {
        initialPozition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPoisitonInitialization)
        {
            transform.position = Vector3.Lerp(transform.position, initialPozition, initializationSpeed * Time.deltaTime);
        }

        if(transform.position.z <= 0.1 && isPoisitonInitialization && !isParticlePlaced)
        {
            managerSO.GetPooledObject(playerRef.transform.position, 1);
            isParticlePlaced = true;
        }

        if (transform.position.z - initialPozition.z <= 0.1f && isPoisitonInitialization)
        {
            isPoisitonInitialization = false;
            managerSO.CameraUpdateFinished();
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

    private void OnDisable()
    {
        managerSO.InitializeCameraPositionEvent -= StartPositionInitialization;
        managerSO.TrackPlayerEvent -= ActivatePlayerTracking;
    }
}
