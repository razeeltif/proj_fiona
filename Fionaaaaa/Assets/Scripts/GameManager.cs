using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{

    static public GameManager instance;

    //[HideInInspector]
    public GameState gameState;
    public Move Nola;
    public Text energieUI;
    public Text HumeurUI;
    public Text timeUI;

    public PostProcessVolume outlineCamera;
    public Color colorInteractable;
    public Color colorActionable;

    GameObject interactableMOuseOverGameObject;

    public UTimer gameTimer;

    public float timeForADay = 10f;

    private int energyNola = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            gameState = GameState.free;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        gameTimer = UTimer.Initialize(timeForADay, this, DayFailed);
        DialogueVisual.instance.gameObject.SetActive(false);
        HumorManager.instance.HumeurUI = HumeurUI;
        HumorManager.instance.ChangeHumor();
        addEnergyForNola(HumorManager.instance.getInitialHumorEnergy());
        gameTimer.start();
    }


    // Update is called once per frame
    void Update()
    {

        timeUI.text = "Time left : " + (int)gameTimer.getTimeLeft();

        GameObject result = getInteractableGameObjectMouseOver();
        if (result != null)
        {
            if(result != interactableMOuseOverGameObject)
            {
                if( interactableMOuseOverGameObject != null)
                    changeLayerForAllChildren(interactableMOuseOverGameObject, "Default");

                interactableMOuseOverGameObject = result;


                PostProcessOutline pr = (PostProcessOutline)(outlineCamera.profile.settings[0]);
                if(result.tag == "interactable")
                {
                    pr.color.value = colorInteractable;
                }
                else
                {
                    pr.color.value = colorActionable;
                }
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
        energieUI.text = "Energy : " + energyNola;
        if(energyNola <= 0)
        {
            DayFailed();
        }
    }

    private void DayFailed()
    {
        endOfDay("Nola is exhausted, end of the day");
    }

    public void DaySuccess()
    {
        endOfDay("Nola go to work \\o/");
    }

    private void endOfDay(string messageEndOfDay)
    {
        gameTimer.pause();
        gameState = GameState.end;
        Nola.GetComponent<Move>().stop();
        Fade.instance.FadeText.text = messageEndOfDay;
        HumorManager.instance.numberOfDaysPassed++;
        Fade.instance.FadeIn();
    }


    private GameObject getInteractableGameObjectMouseOver()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500) && (hit.collider.tag == "interactable" || hit.collider.tag == "dialogable" || hit.collider.tag == "actionable"))
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

    public bool testAction()
    {
        float result = Random.Range(0, 100);
        return (result <= HumorManager.instance.getTauxReussite());
    }
    
   
}
