using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public ManagerSOScript managerSO;

    private ParticleSystem particle;

    // Start is called before the first frame update
    private void OnEnable()
    {
        particle.Play();

    }
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayParticle()
    {
    }
}
