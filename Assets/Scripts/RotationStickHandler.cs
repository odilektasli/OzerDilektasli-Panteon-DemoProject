using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationStickHandler : MonoBehaviour
{

    private Rigidbody rotatorRigidBody;
    Rigidbody characterRb;
    Collider hitCollider;
    // Start is called before the first frame update
    void Start()
    {
        rotatorRigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        characterRb = other.attachedRigidbody;
        hitCollider = other;


        Debug.Log(rotatorRigidBody.name);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rotatorRigidBody.angularVelocity = new Vector3(0, Time.deltaTime * 100f, 0);

        if (characterRb)
        {
            Vector3 forceDirection = transform.position - hitCollider.transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            characterRb.AddForceAtPosition(forceDirection * 5f, hitCollider.transform.position, ForceMode.Impulse);
        }
    }
}
