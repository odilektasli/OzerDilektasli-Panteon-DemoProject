using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacleHandler : MonoBehaviour
{
    public ManagerSOScript managerSO;
    public GameObject dustParticleObject;

    private ParticleSystem dustParticle;

    private GameObject instantiatedParticleObject;
    // Start is called before the first frame update

    private void Awake()
    {
        instantiatedParticleObject = Instantiate(dustParticleObject, new Vector3(transform.position.x, transform.position.y, transform.position.z  - 0.5f), Quaternion.identity);
        instantiatedParticleObject.transform.localScale = new Vector3(2, 2, 2);
        dustParticleObject.transform.position = transform.position;
        dustParticle = instantiatedParticleObject.GetComponent<ParticleSystem>();
        dustParticle.Stop();
    }

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
            dustParticle.Play();

        }
    }
}
