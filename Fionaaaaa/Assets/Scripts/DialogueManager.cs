using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    static public DialogueManager instance;

    public DialogueSettings settings;

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
        if(state == 0)
        {
            BeginDialogue(settings);
            state++;
        }

        else if(state == 1)
        {
            DialogueVisual.instance.startNewSentence(true, settings.dialogueCalme[0].reponseNola);
            state++;
        }

        else if (state == 2)
        {
            DialogueVisual.instance.startNewSentence(false, settings.dialogueCalme[0].reactionSol);
            state++;
        }
        else if (state == 3)
        {
            EndDialogue();
        }

    }

    public void BeginDialogue(DialogueSettings settings)
    {
        GameManager.instance.gameState = GameState.inDialogue;
        this.settings = settings;
        DialogueVisual.instance.gameObject.SetActive(true);

        DialogueVisual.instance.startNewSentence(false, settings.phraseSol);
    }





    public void EndDialogue()
    {
        DialogueVisual.instance.EndDialogue();
        DialogueVisual.instance.gameObject.SetActive(false);
        state = 0;
        GameManager.instance.gameState = GameState.free;
    }
}
