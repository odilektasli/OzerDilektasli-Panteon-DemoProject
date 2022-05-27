using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacleHandler : MonoBehaviour
{
    public ManagerSOScript managerSO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Character")
        {
            managerSO.HitByObstacle();
        }
    }
}
