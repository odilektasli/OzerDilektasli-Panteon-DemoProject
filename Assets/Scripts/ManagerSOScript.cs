using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ManagerSOScript")]
[Serializable]
public class ManagerSOScript : ScriptableObject
{
    public delegate void ObstacleHitDelegate();

    public event ObstacleHitDelegate ObstacleHitEvent;

    public void HitByObstacle()
    {
        ObstacleHitEvent?.Invoke();
    }
}
