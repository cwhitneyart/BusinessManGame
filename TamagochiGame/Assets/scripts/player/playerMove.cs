using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {

    #region public
    //behaviour stuff
    public enum currState { wandering, eating, sitting, sleeping };
    public float behaviourDuration = 10F;
    public float durationAtDestination = 5F;

    //points that the Player will wander to on it's own
    public Vector3[] wanderPoints;
    public GameObject[] destinationObjects;
    #endregion public

    #region private
    private bool setDestination = false;
    private bool destinationChanged = false;
    private bool playerIdle = true;
    private bool destinationReached = false;
    private bool moving = false;
    currState CurrentState;
    #endregion private

    //get player navMesh for movement
    NavMeshAgent playerAgent;

    void Start()
    {
        //get wandering points
        wanderPoints = new Vector3[destinationObjects.Length];
        for (int i = 0; i < destinationObjects.Length; ++i)
        {
            wanderPoints[i] = destinationObjects[i].transform.position;
        }

        //get navmeshAgent
        playerAgent = gameObject.GetComponent<NavMeshAgent>();
        CurrentState = currState.wandering;

        //set bools to initial state
        setDestination = false;
        playerIdle = true;
        destinationReached = false;

    }

    void Update()
    {
        if (CurrentState == currState.wandering)
        {
            if(setDestination == false)
            {
                StartCoroutine(waitAtDestination());
            }

            if(moving == true)
            {
                MoveToDestination();
            }

            if(destinationReached == true)
            {
                setDestination = false;
                destinationReached = false;
            }
            //set logic for if set destination is true here:
            //TODO: add logic here
        }
    }

    #region movementstuff
    void startWandering()
    {

        if (setDestination == false)
        {
            Debug.Log("I'm going to move");
            playerAgent.SetDestination(wanderPoints[Random.Range(0, wanderPoints.Length)]);
            setDestination = true;
        }
        
      
    }

    private void MoveToDestination()
    {
        // Check if we've reached the destination
        if (!playerAgent.pathPending)
        {
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                if (!playerAgent.hasPath || playerAgent.velocity.sqrMagnitude <= 0.1f)
                {
                    setDestination = false;
                    moving = false;
                    Debug.Log("Destination Reached");
  
                    //anim.SetFloat("walking", 0);
                    // Done
                }
            }
        }

        if (playerAgent.velocity.sqrMagnitude > 0.2f)
        {
            //Debug.Log("player is moving");
            // anim.SetFloat("walking", 1);
        }
    }

    IEnumerator waitAtDestination()
    {
        setDestination = true;
        yield return new WaitForSeconds(durationAtDestination);
        Debug.Log("stop waiting at destination");
        startWandering();
        moving = true;
        setDestination = false;
        //setDestination = true;
        // setDestination = false;
        yield break;
    }

    #endregion movementstuff

}
