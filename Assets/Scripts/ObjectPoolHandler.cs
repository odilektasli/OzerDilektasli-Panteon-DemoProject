using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolHandler : MonoBehaviour
{
    public int poolSize;
    public ManagerSOScript managerSO;

    private List<GameObject> pool = new List<GameObject>();
    private int activeObjectCount;
    private GameObject objectType;
    private GameObject instantiatedObject;

    /// <summary>
    /// Instantation of the pool object by given object type.
    /// </summary>
    /// <param name="poolObject"></param>
    public void InstatiatePool(GameObject poolObject)
    {
        objectType = poolObject;
        for (int poolIndex = 0; poolIndex < poolSize; poolIndex++)
        {
            InstatiateObject();
            instantiatedObject.SetActive(false);
        }
    }

    /// <summary>
    /// Enabling of the object at given position. If there is not suffucient object to get when requested, it instantiates new object to meet the demand.
    /// </summary>
    /// <param name="hitPosition"></param>
    public void GetObject(Vector3 hitPosition)
    {
       
        if (activeObjectCount == poolSize)
        {
            InstatiateObject();
            ActivateObject(instantiatedObject, hitPosition);
            poolSize++;
        }

        else
        {
            for (int poolIndex = 0; poolIndex < poolSize; poolIndex++)
            {
                if (!pool[poolIndex].activeSelf)
                {
                    ActivateObject(pool[poolIndex], hitPosition);
                    return;
                }
            }
        }

        activeObjectCount++;

    }
    /// <summary>
    /// Instantiates the pooling object.
    /// </summary>
    private void InstatiateObject()
    {
        instantiatedObject = Instantiate(objectType, transform.position, Quaternion.identity);
        instantiatedObject.transform.parent = transform;
        pool.Add(instantiatedObject);
    }
    /// <summary>
    /// Activation of given object with transform.
    /// </summary>
    /// <param name="activationObject"></param>
    /// <param name="particlePositon"></param>
    private void ActivateObject(GameObject activationObject, Vector3 particlePositon)
    {
        activationObject.SetActive(true);
        activationObject.transform.position = particlePositon;
        StartCoroutine(DisableParticle(activationObject));
    }

    IEnumerator DisableParticle(GameObject activeParticle)
    {
        yield return new WaitForSeconds(managerSO.particleLifeSpan);
        activeParticle.SetActive(false);
        activeObjectCount--;
    }
}
