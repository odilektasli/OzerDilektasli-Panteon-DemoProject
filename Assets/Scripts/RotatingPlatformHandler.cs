using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformHandler : MonoBehaviour
{
    public float Speed;
    public float AngularSpeed;
    public int rotationDirection;

    private Rigidbody platformRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        platformRigidBody = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        platformRigidBody.angularVelocity = new Vector3(0, 0, Time.deltaTime * 100f * rotationDirection);
        //platformRigidBody.AddTorque(Vector3.forward * 5 * 4);

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    other.gameObject.transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * 100f);
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    other.gameObject.transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * 100f);
    //}
}
