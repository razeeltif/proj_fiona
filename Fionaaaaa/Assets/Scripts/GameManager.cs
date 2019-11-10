using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    static public GameManager instance;

    [HideInInspector]
    public GameState gameState = GameState.free;
    public Text energieUI;

    GameObject interactableMOuseOverGameObject;

    public int numberOfDaysPassed = 0;

    private int energyNola = 0;

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
        HumorManager.instance.ChangeHumor();
        addEnergyForNola(HumorManager.instance.getInitialHumorEnergy());
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

                interactableMOuseOverGameObject = result;

                changeLayerForAllChildren(interactableMOuseOverGameObject, "outline");

            }

        }
        else
        {
            if(interactableMOuseOverGameObject != null)
            {
                changeLayerForAllChildren(interactableMOuseOverGameObject, "Default");
                interactableMOuseOverGameObject = null;
            }
        }

    }

    public void addEnergyForNola(int addEnergy)
    {
        energyNola += addEnergy;
        energieUI.text = "energy : " + energyNola;
        if(energyNola <= 0)
        {
            DayFailed();
        }
    }

    private void DayFailed()
    {
        Debug.Log("Nola is exhausted, end of the day");
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
