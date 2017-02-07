using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script will pan the camera and move it
//TODO: Add a moveToPlayer function when a UI button is clicked

public class CameraPanScript : MonoBehaviour {

    float minX = -10;
    float maxX = 10;
    float minY = -10;
    float maxY = 10;


    public int scrollDistance = 20;
    public float scrollSpeed = 70;

    //mouseZoom
    public float minFov = 15f;
    public float maxFov = 90f;
    public float sensitivity = 10f;

    public GameObject cameraPanObject;
    GameObject pannerParentObject;
    float MIN_X = 1;
    float MAX_X = -11;

    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        pannerParentObject = this.gameObject.transform.parent.gameObject;
    }
	
	
    // Update is called once per frame
    void Update()
    {
        mouseZoom();
        panCamera();
        //keyboardScroll();

    }

    private void mouseZoom()
    {
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    private void panCamera()
    {
        Vector3 cameraPanDirection = getPanDirectionFromMousePosition();
        pannerParentObject.transform.Translate(cameraPanDirection * scrollSpeed * Time.deltaTime);

    }

    private Vector3 getPanDirectionFromMousePosition()
    {
        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;

        Vector3 cameraPanDirection = Vector3.zero;



        //Left screen edge
        if (mousePosX < scrollDistance)
        {
            //transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
            cameraPanDirection += Vector3.back;
        }

        //Right screen edge
        if (mousePosX >= Screen.width - scrollDistance)
        {
            //transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
            cameraPanDirection += Vector3.forward;
        }

        //bottom screen edge
        if (mousePosY <= scrollDistance)
        {
            //transform.Translate(transform.forward * -scrollSpeed * Time.deltaTime);
            cameraPanDirection += Vector3.right;
        }

        //top screen edge
        if (mousePosY >= Screen.height - scrollDistance)
        {
            //transform.Translate(transform.forward * scrollSpeed * Time.deltaTime);
            cameraPanDirection += Vector3.left;
        }

        cameraPanDirection.Normalize();

        return cameraPanDirection;
    }

}
