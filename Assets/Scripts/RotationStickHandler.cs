using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationStickHandler : MonoBehaviour
{

    private Rigidbody rotatorRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rotatorRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotatorRigidBody.angularVelocity = new Vector3(0, Time.deltaTime * 100f, 0);
    }
}
