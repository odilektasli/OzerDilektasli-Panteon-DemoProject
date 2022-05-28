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
    public delegate void PoolingDelegate(Vector3 objectTransform, int poolingObjectIndex);
    public delegate void ParticlePoolDelegate();

    public event ObstacleHitDelegate ObstacleHitEvent;
    public event CameraPositionUpdateDelegate InitializeCameraPositionEvent;
    public event PoolingDelegate PoolingGetEvent;
    public event ParticlePoolDelegate ParticlePlayEvent;


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

    public void GetPooledObject(Vector3 objectTransform, int poolingObjectIndex)
    {
        PoolingGetEvent?.Invoke(objectTransform, poolingObjectIndex);
    }
}
