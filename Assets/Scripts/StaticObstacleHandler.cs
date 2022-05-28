using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacleHandler : MonoBehaviour
{
    public ManagerSOScript managerSO;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            managerSO.HitByObstacle();
            managerSO.GetPooledObject(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), 0);

        }
    }

}
