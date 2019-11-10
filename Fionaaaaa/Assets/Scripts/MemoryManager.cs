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


    public void OnClickMemory()
    {
        if(state < getMemoriesEnergies().memories.Length)
        {
            DialogueVisual.instance.startNewSentence(true, getMemoriesEnergies().memories[state]);
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

        DialogueVisual.instance.startNewSentence(true, getMemoriesEnergies().memories[0]);
        state++;
    }

    void EndMemory()
    {
        GameManager.instance.addEnergyForNola(getMemoriesEnergies().impactEnergie);
        DialogueVisual.instance.EndDialogue();
        DialogueVisual.instance.gameObject.SetActive(false);
        state = 0;
        GameManager.instance.gameState = GameState.free;
    }

    memoriesEnergies getMemoriesEnergies()
    {

        switch (HumorManager.instance.actualHumor)
        {
            case HumorState.anxieuse:
                return settings.memoireAnxieuse;

            case HumorState.calme:
                return settings.memoireCalme;

            case HumorState.colerique:
                return settings.memoireColerique;

            default:
                return settings.memoireCalme;

        }


    }
}
