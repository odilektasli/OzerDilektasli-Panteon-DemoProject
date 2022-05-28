using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolHandler : MonoBehaviour
{
    public GameObject hitParticle;
    public int hitParticlePoolSize;

    private List<GameObject> hitParticlePool = new List<GameObject>();

    private int activeHitParticleCount;
    private ParticleSystem hitParticleSystem;
    // Start is called before the first frame update
    private void Awake()
    {

        for (int poolIndex = 0; poolIndex < hitParticlePoolSize; poolIndex++)
        {
            GameObject dummyParticle = Instantiate(hitParticle, transform.position, Quaternion.identity);
            dummyParticle.SetActive(false);
            hitParticlePool.Add(dummyParticle);
        }

        hitParticleSystem = hitParticle.GetComponent<ParticleSystem>();
    }

    private void GetHitParticle(Vector3 hitPosition)
    {
        if(activeHitParticleCount == hitParticlePoolSize)
        {
            GameObject dummyParticle = Instantiate(hitParticle, hitPosition, Quaternion.identity);
            hitParticlePool.Add(dummyParticle);
        }
    }
}
