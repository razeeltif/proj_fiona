using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.PostProcessing;

public class InputManager : MonoBehaviour
{

    public Move move;
    //public PostProcessVolume cam;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(GameManager.instance.gameState == GameState.end)
        {
            return;
        }

      /*  if (Input.GetKeyDown(KeyCode.A))
        {
            HumorManager.instance.ChangeHumor();
            HumorManager.instance.numberOfDaysPassed++;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PostProcessOutline pr = (PostProcessOutline)(cam.profile.settings[0]);
            pr.color.value = Color.red;
        }*/


        if (GameManager.instance.gameState == GameState.free)
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
        {
            if (Input.GetMouseButtonDown(0))
            {
                MemoryManager.instance.OnClickMemory();
            }
        }

        if (GameManager.instance.gameState == GameState.inAction)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ActionManager.instance.OnClickAction();
            }
        }
    }
}
