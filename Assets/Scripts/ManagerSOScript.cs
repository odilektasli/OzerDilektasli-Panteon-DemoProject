using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ManagerSOScript")]
[Serializable]
public class ManagerSOScript : ScriptableObject
{

    public float platformXOffset;

    public delegate void ObstacleHitDelegate();
    public delegate void CameraPositionUpdateDelegate(bool isInitializationFinished);

    public event ObstacleHitDelegate ObstacleHitEvent;
    public event CameraPositionUpdateDelegate InitializeCameraPositionEvent;


    public void HitByObstacle()
    {
        ObstacleHitEvent?.Invoke();
    }

    public void InitializeCameraPosition(bool isInitializationFinished)
    {
        InitializeCameraPositionEvent?.Invoke(isInitializationFinished);
    }
}
