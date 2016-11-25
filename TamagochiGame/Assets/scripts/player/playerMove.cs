using UnityEngine;
using System.Collections;
//This class will handle behaviour of the business man, and his routine.
public class playerMove : MonoBehaviour {

    #region public
    //behaviour stuff
    public enum currState { wandering, eating, sitting, sleeping }; //the current state of the AI
    currState CurrentState;

    public float behaviourDuration = 10F; //duration that behaviours will last for. This might be replaced with the rolldieBehaviourChange float at some point.
    public float durationAtDestination = 5F; //Duration that AI will remain at destination for. This can be set as a random range for some added variety

    public float rollDieBehaviourChange;     //used to randomly change behaviours if number lands within certain range. 

    
    public Vector3[] wanderPoints; //points that the Player will wander to on it's own
    public GameObject[] destinationObjects; //Grabs points that are put into the inspector. Used to grab vector3 points to set navmesh destinations
    public GameObject[] seats; //seats that the player can sit on
    public Vector3[] seatPoints;
    #endregion public

    #region private
    private bool setDestination = false; //checks if destination has been set yet. If this is true the destination will not be set again until set back to false
    private bool destinationChanged = false; //checks if the destination has been changed. I don't know if this is redundant or not yet. 
    private bool playerIdle = true; //later on we'll check if the player has not interacted with the business man. If they haven't he will start doing his own thing
    private bool destinationReached = false; //checks if destination has been reached. Probably redundant
    private bool moving = false; //Checks if player is currently moving or not.
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
        
        //get vector3 of all seats
        seatPoints = new Vector3[seats.Length];
        for(int i = 0; i < seats.Length; ++i)
        {
            seatPoints[i] = seats[i].transform.position;
        }

        //get navmeshAgent
        playerAgent = gameObject.GetComponent<NavMeshAgent>();
        CurrentState = currState.wandering;

        //set bools to initial state
        setDestination = false;
        playerIdle = true;
        destinationReached = false;

        //start wandering
        StartCoroutine(waitAtDestination());
        StartCoroutine(resetBehaviourCheck());
    }

    void Update()
    {
        //if the businessman is wandering, allow these behaviours to occur
        if (CurrentState == currState.wandering)
        {
            //if player is moving, check that destination has been reached every frame
            if(moving == true)
            {
                MoveToDestination();
            }
            //if the destination has been reached, turn set destination off so that the destination can be changed again.
            if(destinationReached == true)
            {
                setDestination = false;
                destinationReached = false;
            }
        }
       else if(CurrentState == currState.sitting)
        {
            if(moving == true)
            {
                moveToSeat();
            }
        }
    }

    IEnumerator resetBehaviourCheck() //check if behaviour should be changed depending on "die roll"
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            rollDieBehaviourChange = Random.Range(0, 10);
            if(rollDieBehaviourChange <= 2)
            {
                CurrentState = currState.sitting;
                StartCoroutine(goAndSit());
                Debug.Log("Player is going to sit");
            }
            if (rollDieBehaviourChange >= 3)
            {
                CurrentState = currState.wandering; //behaviour changed to wandering
                StartCoroutine(waitAtDestination());
            }
            if (rollDieBehaviourChange >= 5)
            {
                CurrentState = currState.eating; //behaviour changed to eating
            }
            if (rollDieBehaviourChange >= 8)
            {
                CurrentState = currState.sleeping; //behaviour changed to sleeping
            }
        }
    }

    #region wanderingStuff
    void startWandering()
    {
        //call a new destination once only
        //this could probably be a coroutine instead if it needs to be neater
        if (setDestination == false)
        {
            Debug.Log("I'm going to move");
            playerAgent.SetDestination(wanderPoints[Random.Range(0, wanderPoints.Length)]);
            setDestination = true;
        }
        
      
    }

    private void MoveToDestination()
    {
        if(CurrentState == currState.wandering)
        {
            // Check if we've reached the destination
            if (!playerAgent.pathPending)
            {
                if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
                {
                    if (!playerAgent.hasPath || playerAgent.velocity.sqrMagnitude <= 0.1f)
                    {
                        StartCoroutine(waitAtDestination());
                        setDestination = false;
                        moving = false;
                        //change animator back to idle here:

                    }
                }
            }
            //check if player is moving
            if (playerAgent.velocity.sqrMagnitude > 0.2f)
            {
                //set walking animation here: 
            }
        }
       
    }

    //wait for predetermined set of seconds, then set a new destination 
    IEnumerator waitAtDestination()
    {
        if (CurrentState == currState.wandering)
        {
            setDestination = true;
            yield return new WaitForSeconds(durationAtDestination);
            Debug.Log("stop waiting at destination");
            //startWandering();
            moving = true;
            setDestination = false;
            startWandering();
            yield break;
        }
        yield break;  
    }

    #endregion wanderingStuff
    #region sleepingStuff
    //TODO: Set behaviours for what happens when the businessman is sleeping here:



    #endregion sleepingStuff
    #region sittingStuff
    IEnumerator goAndSit()
    {
        playerAgent.SetDestination(seatPoints[0]);
        yield break;
    }

    void moveToSeat()
    {
        // Check if we've reached the destination
        if (!playerAgent.pathPending)
        {
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                if (!playerAgent.hasPath || playerAgent.velocity.sqrMagnitude <= 0.1f)
                {
                    moving = false;
                    //change animator back to idle here:

                }
            }
        }
        //check if player is moving
        if (playerAgent.velocity.sqrMagnitude > 0.2f)
        {
            //set walking animation here: 
        }
    }


    #endregion sittingStuff

}
