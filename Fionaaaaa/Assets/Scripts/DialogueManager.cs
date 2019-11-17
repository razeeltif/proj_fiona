using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    static public DialogueManager instance;

    private DialogueSettings settings;

    int indexChoice = -1;
    int state = 0;


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



    public void OnClickDialogue()
    {

        if (state == 0)
        {
            DialogueVisual.instance.startNewSentence(false, getDialogueFromHumor()[indexChoice].reactionSol);
            state++;
        }
        else if (state == 1)
        {
            EndDialogue();
        }

    }

    public void BeginDialogue(DialogueSettings settings)
    {
        GameManager.instance.gameTimer.pause();
        GameManager.instance.gameState = GameState.inChoice;
        this.settings = settings;
        DialogueVisual.instance.gameObject.SetActive(true);

        DialogueVisual.instance.startNewSentence(false, settings.phraseSol);

        DialogueVisual.instance.CreateChoices(getDialogueFromHumor());
    }


    public void ChoiceMade(int indexChoosenButton)
    {
        indexChoice = indexChoosenButton;
        DialogueVisual.instance.startNewSentence(true, getDialogueFromHumor()[indexChoice].reponseNola);
        DialogueVisual.instance.EndChoice();
        GameManager.instance.gameState = GameState.inDialogue;
    }


    public void EndDialogue()
    {
        GameManager.instance.gameState = GameState.free;
        GameManager.instance.gameTimer.continu();
        GameManager.instance.addEnergyForNola(getDialogueFromHumor()[indexChoice].impactEnergie);
        DialogueVisual.instance.EndDialogue();
        DialogueVisual.instance.gameObject.SetActive(false);
        state = 0;
        indexChoice = -1;
    }

    List<reponse> getDialogueFromHumor()
    {

        switch (HumorManager.instance.actualHumor)
        {
            case HumorState.anxieuse:
                return settings.dialogueAnxieuse;

            case HumorState.calme:
                return settings.dialogueCalme;

            case HumorState.colerique:
                return settings.dialogueColerique;

            default:
                return settings.dialogueCalme;

        }
    }
}
