using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ManagerSOScript")]
[Serializable]
public class ManagerSOScript : ScriptableObject
{

    public float platformXOffset;
    public float particleLifeSpan;

    public delegate void ObstacleHitDelegate();
    public delegate void CameraPositionUpdateDelegate();
    public delegate void CameraTrackModeDelegate(bool isTracking);
    public delegate void PoolingDelegate(Vector3 objectTransform, int poolingObjectIndex);
    public delegate void ParticlePoolDelegate();

    public event ObstacleHitDelegate ObstacleHitEvent;
    public event CameraPositionUpdateDelegate InitializeCameraPositionEvent;
    public event CameraPositionUpdateDelegate CameraUpdateFinishedEvent;
    public event CameraTrackModeDelegate TrackPlayerEvent;
    public event PoolingDelegate PoolingGetEvent;
    public event ParticlePoolDelegate ParticlePlayEvent;


    public void HitByObstacle()
    {
        ObstacleHitEvent?.Invoke();
    }

    public void InitializeCameraPosition()
    {
        InitializeCameraPositionEvent?.Invoke();
    }

    public void PlayParticle()
    {
        ParticlePlayEvent?.Invoke();
    }

    public void GetPooledObject(Vector3 objectTransform, int poolingObjectIndex)
    {
        PoolingGetEvent?.Invoke(objectTransform, poolingObjectIndex);
    }

    public void CameraUpdateFinished()
    {
        CameraUpdateFinishedEvent?.Invoke();
    }

    public void CameraTrackPlayerMode(bool isTracking)
    {
        TrackPlayerEvent?.Invoke(isTracking);
    }
}
