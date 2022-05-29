using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutMovingStickHandler : MonoBehaviour
{
    public ManagerSOScript managerSO;
    public float startOffsetX;
    public float endOffsetX;

    private bool isLerping;
    private Vector3 newLocation;
    private float randomXOffset;
    private float randomMovementSpeed;
    private bool isEndPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /////////////////////////////////Setting of the stick position with randomized offset
        //if (!isLerping)
        //{
        //    randomXOffset = Random.Range(endOffsetX, startOffsetX);
        //    randomMovementSpeed = Random.Range(10f, 30f);
        //    newLocation = new Vector3(randomXOffset, transform.localPosition.y, transform.localPosition.z);
        //    isLerping = true;
        //}
        //else if (isLerping)
        //{
        //    transform.localPosition = Vector3.Lerp(transform.localPosition, newLocation, randomMovementSpeed * Time.deltaTime);
        //}


        //if (Vector3.Distance(transform.localPosition, newLocation) < 0.0001f || Vector3.Distance(transform.localPosition, newLocation) < -0.0001f)
        //{
        //    isLerping = false;
        //}
        /////////////////////////////////Can be used if there occurs a change on design idea. Current version looks better...

        //Set of the x position of stick with randomized speed using lerp.
        if (!isLerping)
        {
            isEndPoint = !isEndPoint;
            if(isEndPoint)
            {
                randomXOffset = endOffsetX;
            }
            else
            {
                randomXOffset = startOffsetX;
            }

            randomMovementSpeed = Random.Range(10f, 30f);
            newLocation = new Vector3(randomXOffset, transform.localPosition.y, transform.localPosition.z);
            isLerping = true;
        }
        else if (isLerping)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, newLocation, randomMovementSpeed * Time.deltaTime);
        }


        if (Vector3.Distance(transform.localPosition, newLocation) < 0.0001f || Vector3.Distance(transform.localPosition, newLocation) < -0.0001f)
        {
            isLerping = false;
        }

    }

}
