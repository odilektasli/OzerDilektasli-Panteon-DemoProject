using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    public ManagerSOScript managerSO;
    public GameObject poolingComponentRef;
    public GameObject paintingWallComponentRef;

    public List<GameObject> poolingObjects;
    public List<ObjectPoolHandler> poolList = new List<ObjectPoolHandler>();
    public GameObject[] opponentCharacters;
    public GameObject playerRef;
    private ObjectPoolHandler instantiatedPoolObject;
    private List<GameObject> characterRankingArray = new List<GameObject>();
    private int playerRanking;

    private bool isGameStopped;

    private GameObject dummyArrayElement;
    private void Awake()
    {
        //Instantiation of object pools which is assigned by prefabs from the editor.
        managerSO.PoolingGetEvent += GetPooledObject;
        managerSO.PaintingWallActivationEvent += ActivatePaintingWall;

        for (int objectIndex = 0; objectIndex < poolingObjects.Count; objectIndex++)
        {
            instantiatedPoolObject = Instantiate(poolingComponentRef).GetComponent<ObjectPoolHandler>();
            instantiatedPoolObject.InstatiatePool(poolingObjects[objectIndex]);
            poolList.Insert(objectIndex, instantiatedPoolObject);
        }

    }

    private void Start()
    {
        for(int arrayIndex = 0; arrayIndex < opponentCharacters.Length ; arrayIndex++)
        {
            characterRankingArray.Add(opponentCharacters[arrayIndex]);
        }
        characterRankingArray.Add(playerRef);
        StartCoroutine(CheckPlayerRanking());
    }

    IEnumerator CheckPlayerRanking()
    {
        while(!isGameStopped)
        {
            yield return new WaitForSeconds(0.01f);

            for (int iterationCount = 0; iterationCount < characterRankingArray.Count - 1; iterationCount++)
            {
                for (int arrayIndex = 0; arrayIndex < characterRankingArray.Count - 1; arrayIndex++)
                {
                    if (characterRankingArray[arrayIndex].transform.position.z < characterRankingArray[arrayIndex + 1].transform.position.z)
                    {
                        //Debug.Log(characterRankingArray[arrayIndex].transform.position.z);
                        dummyArrayElement = characterRankingArray[arrayIndex];
                        characterRankingArray[arrayIndex] = characterRankingArray[arrayIndex + 1];
                        characterRankingArray[arrayIndex + 1] = dummyArrayElement;

                    }
                }
            }
            if(playerRanking != (characterRankingArray.IndexOf(playerRef) + 1))
            {
                playerRanking = (characterRankingArray.IndexOf(playerRef) + 1);
                managerSO.UpdatePlayerRanking(playerRanking);
            }

        }
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Provides getting of the object with position and index from the pool
    /// </summary>
    /// <param name="hitPosition"></param>
    /// <param name="objectIndex"></param>
    private void GetPooledObject(Vector3 hitPosition, int objectIndex)
    {
        poolList[objectIndex].GetObject(hitPosition);
    }

    private void ActivatePaintingWall()
    {
        isGameStopped = true;
        paintingWallComponentRef.SetActive(true);
        managerSO.LerpToPosition(paintingWallComponentRef.transform.position);
    }
}
