using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ManagerSOScript")]
[Serializable]
public class ManagerSOScript : ScriptableObject
{

    public float platformXOffset;
    public float particleLifeSpan;

    public delegate void ObstacleHitDelegate();
    public delegate void CameraPositionUpdateDelegate(bool isInitializationFinished);
    public delegate void ParticlePoolDelegate(Vector3 particleTransform);
    public delegate void ParticleDelegate();

    public event ObstacleHitDelegate ObstacleHitEvent;
    public event CameraPositionUpdateDelegate InitializeCameraPositionEvent;
    public event ParticlePoolDelegate GetHitParticleEvent;
    public event ParticleDelegate ParticlePlayEvent;


    public void HitByObstacle()
    {
        ObstacleHitEvent?.Invoke();
    }

    public void InitializeCameraPosition(bool isInitializationFinished)
    {
        InitializeCameraPositionEvent?.Invoke(isInitializationFinished);
    }

    public void PlayParticle()
    {
        ParticlePlayEvent?.Invoke();
    }

    public void GetHitParticle(Vector3 particleTransform)
    {
        GetHitParticleEvent?.Invoke(particleTransform);
    }
}
