using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public ManagerSOScript managerSO;
    public float initializationSpeed;

    private bool isPoisitonInitialization;
    private Vector3 initialPozition;
    // Start is called before the first frame update

    private void Awake()
    {
        managerSO.InitializeCameraPositionEvent += StartPositionInitialization;
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
    }

    private void StartPositionInitialization(bool isInitialization)
    {
        if(isInitialization)
        {
            isPoisitonInitialization = true;

        }

        else
        {
            isPoisitonInitialization = false;
        }
    }

    private void OnDisable()
    {
        managerSO.InitializeCameraPositionEvent -= StartPositionInitialization;
    }
}
