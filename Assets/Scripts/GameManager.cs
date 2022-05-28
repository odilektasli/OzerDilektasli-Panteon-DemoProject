using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ManagerSOScript managerSO;
    public GameObject hitParticle;
    public int hitParticlePoolSize;

    private List<GameObject> hitParticlePool = new List<GameObject>();
    private GameObject instantiatedParticleObject;

    private int activeHitParticleCount;
    // Start is called before the first frame update
    private void Awake()
    {
        managerSO.GetHitParticleEvent += GetHitParticle;

        for (int poolIndex = 0; poolIndex < hitParticlePoolSize; poolIndex++)
        {
            instantiatedParticleObject = Instantiate(hitParticle, transform.position, Quaternion.identity);
            instantiatedParticleObject.SetActive(false);
            hitParticlePool.Add(instantiatedParticleObject);
        }

    }

    private void GetHitParticle(Vector3 hitPosition)
    {
        if (activeHitParticleCount == hitParticlePoolSize)
        {
            GameObject dummyParticle = Instantiate(hitParticle, hitPosition, Quaternion.identity);
            hitParticlePool.Add(dummyParticle);
            ActivateParticle(dummyParticle, hitPosition);
            hitParticlePoolSize++;
        }

        else
        {
            for (int poolIndex = 0; poolIndex < hitParticlePoolSize; poolIndex++)
            {
                if(!hitParticlePool[poolIndex].activeSelf)
                {
                    ActivateParticle(hitParticlePool[poolIndex], hitPosition);
                    return;
                }
            }
        }

        activeHitParticleCount++;

    }

    private void ActivateParticle(GameObject particle, Vector3 particlePositon)
    {
        particle.SetActive(true);
        particle.transform.position = particlePositon;
        StartCoroutine(DisableParticle(particle));
    }

    IEnumerator DisableParticle(GameObject activeParticle)
    {
        yield return new WaitForSeconds(managerSO.particleLifeSpan);
        activeParticle.SetActive(false);
        activeHitParticleCount -- ;
    }
}
