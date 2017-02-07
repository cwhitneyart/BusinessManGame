using UnityEngine;
using System.Collections;

public class clampCamera : MonoBehaviour
{

   public float minX = -3;
    public float maxX = 5;
    float minZ = -8;
    float maxZ = 7;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
     Mathf.Clamp(transform.position.x, minX, maxX),transform.position.y, transform.position.z);
    // transform.position = new Transform.( Mathf.Clamp(transform.position.z, minX, maxX), 0, 0);
    }
}