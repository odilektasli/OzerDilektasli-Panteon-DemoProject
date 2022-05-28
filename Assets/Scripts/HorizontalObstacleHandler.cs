using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacleHandler : MonoBehaviour
{
    public ManagerSOScript managerSO;

    public float movementSpeed;
    public float directionCheckTreshold;

    private Rigidbody obstacleRigidBody;

    private bool shouldObstacleMoveLeft;

    private void Awake()
    {
        obstacleRigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        if(transform.position.x < 0)
        {
            shouldObstacleMoveLeft = true;
        }

        else
        {
            shouldObstacleMoveLeft = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (managerSO.platformXOffset - transform.position.x <= directionCheckTreshold)
        {
            shouldObstacleMoveLeft = false;
        }

        if (transform.position.x - (managerSO.platformXOffset * -1) <= directionCheckTreshold)
        {
            shouldObstacleMoveLeft = true;
        }
    }

    private void FixedUpdate()
    {


        if (shouldObstacleMoveLeft)
        {
            obstacleRigidBody.velocity = new Vector3(movementSpeed, 0, 0);
        }

        else
        {
            obstacleRigidBody.velocity = new Vector3(movementSpeed * -1, 0, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            managerSO.HitByObstacle();
            managerSO.GetPooledObject(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), 0);

        }
    }
}
