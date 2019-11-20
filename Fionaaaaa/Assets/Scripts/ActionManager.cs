﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{

    static public ActionManager instance;
    public GameSettings settings;

    public List<Actionable> listActionsAFaire;

    private int state;
    private Actionable actualAction;
    private bool successAction;

    private void Awake()
    {
        if (instance == null)
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

    }


    public void BeginAction(Actionable actionedObject)
    {

        foreach(Actionable ac in listActionsAFaire)
        {
            if(ac == actionedObject)
            {
                actualAction = ac;
            }
        }


        GameManager.instance.gameTimer.pause();
        GameManager.instance.gameState = GameState.inAction;
        DialogueVisual.instance.gameObject.SetActive(true);

        DialogueVisual.instance.startNewSentence(true, actualAction.settings.intituleAction);
        state++;
    }

    public void OnClickAction()
    {

        switch (state)
        {
            case 1:
                DialogueVisual.instance.startNewSentence(true, actualAction.settings.impactEnergie + " energy");
                state++;
                break;

            case 2:
                successAction = GameManager.instance.testAction();
                if (successAction)
                {
                    DialogueVisual.instance.startNewSentence(true, settings.TextWhenActionSucessful, settings.colorTextWhenActionSucessful);
                }
                else
                {
                    DialogueVisual.instance.startNewSentence(true, settings.TextWhenActionFailed, settings.colorTextWhenActionFailed);
                }
                state++;
                break;

            case 3:
                GameManager.instance.gameState = GameState.free;
                GameManager.instance.gameTimer.continu();
                GameManager.instance.addEnergyForNola(actualAction.settings.impactEnergie);
                DialogueVisual.instance.EndDialogue();
                DialogueVisual.instance.gameObject.SetActive(false);
                state = 0;

                if (successAction)
                {
                    listActionsAFaire.Remove(actualAction);
                    actualAction.gameObject.tag = "Untagged";
                    if(listActionsAFaire.Count == 0)
                    {
                        GameManager.instance.DaySuccess();
                    }
                }

                

                break;

            default:
                break;
        }

    }

}
