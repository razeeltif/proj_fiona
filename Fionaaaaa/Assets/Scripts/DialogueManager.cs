using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    static public DialogueManager instance;

    public DialogueSettings settings;

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
            DialogueVisual.instance.startNewSentence(false, settings.dialogueCalme[indexChoice].reactionSol);
            state++;
        }
        else if (state == 1)
        {
            EndDialogue();
        }

    }

    public void BeginDialogue(DialogueSettings settings)
    {
        GameManager.instance.gameState = GameState.inChoice;
        this.settings = settings;
        DialogueVisual.instance.gameObject.SetActive(true);

        DialogueVisual.instance.startNewSentence(false, settings.phraseSol);

        DialogueVisual.instance.CreateChoices(settings.dialogueCalme);
    }


    public void ChoiceMade(int indexChoosenButton)
    {
        indexChoice = indexChoosenButton;
        DialogueVisual.instance.startNewSentence(true, settings.dialogueCalme[indexChoice].reponseNola);
        DialogueVisual.instance.EndChoice();
        GameManager.instance.gameState = GameState.inDialogue;
    }




    public void EndDialogue()
    {
        DialogueVisual.instance.EndDialogue();
        DialogueVisual.instance.gameObject.SetActive(false);
        state = 0;
        indexChoice = -1;
        GameManager.instance.gameState = GameState.free;
    }
}
