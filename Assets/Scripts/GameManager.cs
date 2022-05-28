using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    public ManagerSOScript managerSO;
    public GameObject hitParticle;
    public int hitParticlePoolSize;
    public GameObject poolingComponentRef;
    public static int initialParticlePoolSize;

    public List<GameObject> poolingObjects;
    public List<ObjectPoolHandler> poolList = new List<ObjectPoolHandler>();
    private ObjectPoolHandler instantiatedPoolObject;
    private void Awake()
    {
        //Instantiation of object pools which is assigned by prefabs from the editor.
        managerSO.PoolingGetEvent += GetPooledObject;
        Debug.Log(poolingObjects.Count);
        for (int objectIndex = 0; objectIndex < poolingObjects.Count; objectIndex++)
        {
            instantiatedPoolObject = Instantiate(poolingComponentRef).GetComponent<ObjectPoolHandler>();
            instantiatedPoolObject.InstatiatePool(poolingObjects[objectIndex], transform.position);
            poolList.Insert(objectIndex, instantiatedPoolObject);
        }

    }

    /// <summary>
    /// Provides getting of the object with position and index from the pool
    /// </summary>
    /// <param name="hitPosition"></param>
    /// <param name="objectIndex"></param>
    private void GetPooledObject(Vector3 hitPosition, int objectIndex)
    {
        poolList[objectIndex].GetObject(hitPosition);
    }

}
