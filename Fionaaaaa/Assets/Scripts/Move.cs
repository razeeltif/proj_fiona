using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{

    private NavMeshAgent navMesh;

    public float speed = 0.5f;
    public float minimumDistanceToValidateDestination = 2;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        navMesh.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            navMesh.destination  = getMouseClickPosition();

        }
        
    }


    private Vector3 getMouseClickPosition()
    {
        Vector3 pos = new Vector3();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 500))
        {
            pos = hit.point;
        }

        return pos;
    }



    private void OnValidate()
    {
        if(navMesh != null)
            navMesh.speed = speed;
    }
}
