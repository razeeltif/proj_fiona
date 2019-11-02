using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    static public MemoryManager instance;


    private MemorySettings settings;
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
        if(state < settings.memoireCalme.memories.Length)
        {
            DialogueVisual.instance.startNewSentence(true, settings.memoireCalme.memories[state]);
            state++;
        }
        else
        {
            EndMemory();
        }
        
    }

    public void BeginMemory(MemorySettings settings)
    {
        GameManager.instance.gameState = GameState.inMemory;
        this.settings = settings;
        DialogueVisual.instance.gameObject.SetActive(true);

        DialogueVisual.instance.startNewSentence(true, settings.memoireCalme.memories[0]);
        state++;
    }

    void EndMemory()
    {
        DialogueVisual.instance.EndDialogue();
        DialogueVisual.instance.gameObject.SetActive(false);
        state = 0;
        GameManager.instance.gameState = GameState.free;
    }
}
