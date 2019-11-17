using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{

    private NavMeshAgent navMesh;

    public float speed = 0.5f;
    public float minimumDistanceToValidateDestination = 2;

    private GameObject interactableObject;
    private GameObject dialogableObject;
    private GameObject actionableObject;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        navMesh.speed = speed;
    }

    public void getMouseClickPosition()
    {
        Vector3 pos = new Vector3();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 500))
        {
            pos = hit.point;
            navMesh.destination = pos;

            if (hit.collider.tag == "interactable")
            {
                interactableObject = hit.collider.gameObject;
            }
            else if(hit.collider.tag == "dialogable")
            {
                dialogableObject = hit.collider.gameObject;
            }
            else if(hit.collider.tag == "actionable")
            {
                actionableObject = hit.collider.gameObject;
            }
            else
            {
                interactableObject = null;
                dialogableObject = null;
            }
            
        }


    }

    public void stop()
    {
        navMesh.destination = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == interactableObject)
        {
            MemoryManager.instance.BeginMemory(interactableObject.GetComponent<Interactable>().settings);
            navMesh.destination = this.transform.position;
            actionWhenTrigger(other.gameObject);
        }

        if(other.gameObject == dialogableObject)
        {
            DialogueManager.instance.BeginDialogue(dialogableObject.GetComponent<Dialogable>().settings);
            actionWhenTrigger(other.gameObject);
        }

        if(other.gameObject == actionableObject)
        {
            ActionManager.instance.BeginAction(actionableObject.GetComponent<Actionable>());
            navMesh.destination = this.transform.position;
            interactableObject = null;
            dialogableObject = null;
            actionableObject = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == interactableObject)
        {
            MemoryManager.instance.BeginMemory(interactableObject.GetComponent<Interactable>().settings);
            navMesh.destination = this.transform.position;
            actionWhenTrigger(other.gameObject);
        }

        if (other.gameObject == dialogableObject)
        {
            DialogueManager.instance.BeginDialogue(dialogableObject.GetComponent<Dialogable>().settings);
            actionWhenTrigger(other.gameObject);
        }

        if (other.gameObject == actionableObject)
        {
            ActionManager.instance.BeginAction(actionableObject.GetComponent<Actionable>());
            navMesh.destination = this.transform.position;
            interactableObject = null;
            dialogableObject = null;
            actionableObject = null;
        }
    }

    private void actionWhenTrigger(GameObject disable)
    {
        interactableObject = null;
        dialogableObject = null;
        actionableObject = null;
        disable.tag = "Untagged";
    }


    private void OnValidate()
    {
        if(navMesh != null)
            navMesh.speed = speed;
    }
}
