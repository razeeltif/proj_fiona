using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public Move move;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            HumorManager.instance.ChangeHumor();
            GameManager.instance.numberOfDaysPassed++;
        }
        

        if(GameManager.instance.gameState == GameState.free)
        {
            if (Input.GetMouseButtonDown(0))
            {
                move.getMouseClickPosition();
            }
        }


        if(GameManager.instance.gameState == GameState.inDialogue)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DialogueManager.instance.OnClickDialogue();
            }
        }

        if(GameManager.instance.gameState == GameState.inMemory)
            if (Input.GetMouseButtonDown(0))
            {
                MemoryManager.instance.OnClickMemory();
            }

    }
}
