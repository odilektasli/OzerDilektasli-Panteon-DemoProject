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
    public delegate void LerpToPositionDelegate(Vector3 lerPosition);
    public delegate void PoolingDelegate(Vector3 objectTransform, int poolingObjectIndex);
    public delegate void ParticlePoolDelegate();
    public delegate void PaintingWallDelegate();
    public delegate void GUINumberUpdateDelegate(int updateNumber);

    public event ObstacleHitDelegate ObstacleHitEvent;
    public event CameraPositionUpdateDelegate InitializeCameraPositionEvent;
    public event CameraPositionUpdateDelegate CameraUpdateFinishedEvent;
    public event CameraTrackModeDelegate TrackPlayerEvent;
    public event LerpToPositionDelegate LerpToPositionEvent;
    public event PoolingDelegate PoolingGetEvent;
    public event ParticlePoolDelegate ParticlePlayEvent;
    public event PaintingWallDelegate PaintingWallActivationEvent;
    public event GUINumberUpdateDelegate UpdatePaintingPercantageEvent;
    public event GUINumberUpdateDelegate UpdatePlayerRankingEvent;


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
    /// <summary>
    /// Get object from the pool by giving the transform of the object where you want spawn and object index from pool list in game manager.
    /// </summary>
    /// <param name="objectTransform"></param>
    /// <param name="poolingObjectIndex"></param>
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

    public void ActivatePaintingWall()
    {
        PaintingWallActivationEvent?.Invoke();
    }

    public void LerpToPosition(Vector3 lerpPosition)
    {
        LerpToPositionEvent?.Invoke(lerpPosition);
    }

    public void UpdatePaintingPercentage(int updateNum)
    {
        UpdatePaintingPercantageEvent?.Invoke(updateNum);
    }

    public void UpdatePlayerRanking(int updateNum)
    {
        UpdatePlayerRankingEvent?.Invoke(updateNum);
    }
}
