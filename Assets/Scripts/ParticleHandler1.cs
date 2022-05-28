using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler1 : MonoBehaviour
{
    public ManagerSOScript managerSO;

    private ParticleSystem particle;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Debug.Log("Aloooooooooooooo");
        particle.Play();

    }
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        Debug.Log(particle);
        
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
