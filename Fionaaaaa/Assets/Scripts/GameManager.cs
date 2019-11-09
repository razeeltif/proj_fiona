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
                    changeLayerForAllChildren(interactableMOuseOverGameObject, "Default");
                //interactableMOuseOverGameObject.layer = LayerMask.NameToLayer("Default");

                interactableMOuseOverGameObject = result;

                changeLayerForAllChildren(interactableMOuseOverGameObject, "outline");
                //interactableMOuseOverGameObject.layer = LayerMask.NameToLayer("outline");

            }

        }
        else
        {
            if(interactableMOuseOverGameObject != null)
            {
                changeLayerForAllChildren(interactableMOuseOverGameObject, "Default");
                //interactableMOuseOverGameObject.layer = LayerMask.NameToLayer("Default");
                interactableMOuseOverGameObject = null;
            }
        }

    }

    private GameObject getInteractableGameObjectMouseOver()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500) && (hit.collider.tag == "interactable" || hit.collider.tag == "dialogable"))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private void changeLayerForAllChildren(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);
        foreach(Transform child in obj.GetComponentsInChildren<Transform>())
        {
            if(child != obj.transform)
                changeLayerForAllChildren(child.gameObject, layerName);
        }
        
    }

}
