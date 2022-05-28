using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public GameObject hitParticle;
    public int hitParticlePoolSize;

    private List<GameObject> hitParticlePool;

    private int activeHitParticleCount;
    // Start is called before the first frame update
    private void Awake()
    {
        for (int poolIndex = 0; poolIndex < hitParticlePoolSize; poolIndex++)
        {
            GameObject dummyParticle = Instantiate(hitParticle, transform.position, Quaternion.identity);
            dummyParticle.SetActive(false);
            hitParticlePool.Add(dummyParticle);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetHitParticle(Vector3 hitPosition)
    {
        if(activeHitParticleCount == hitParticlePoolSize)
        {
            var dummyParticle = Instantiate(hitParticle, hitPosition, Quaternion.identity);
            hitParticlePool.Add(dummyParticle);
        }
    }
}
