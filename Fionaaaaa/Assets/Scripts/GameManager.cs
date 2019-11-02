using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static public GameManager instance;
    public GameState gameState = GameState.free;

    GameObject interactableMOuseOverGameObject;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        DialogueVisual.instance.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        GameObject result = getInteractableGameObjectMouseOver();
        if (result != null)
        {
            if(result != interactableMOuseOverGameObject)
            {
                if( interactableMOuseOverGameObject != null)
                    interactableMOuseOverGameObject.layer = LayerMask.NameToLayer("Default");

                interactableMOuseOverGameObject = result;
                interactableMOuseOverGameObject.layer = LayerMask.NameToLayer("outline");
            }

        }
        else
        {
            if(interactableMOuseOverGameObject != null)
            {
                interactableMOuseOverGameObject.layer = LayerMask.NameToLayer("Default");
                interactableMOuseOverGameObject = null;
            }
        }

    }

    private GameObject getInteractableGameObjectMouseOver()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500) && hit.collider.tag == "interactable")
        {
            return hit.collider.gameObject;
        }
        return null;
    }


}
