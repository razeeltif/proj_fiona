using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Game setting")]
public class GameSettings : ScriptableObject
{
    public float timeForADay = 10f;

    public Color colorInteractable;
    public Color colorActionable;

    public string TextDaySuccess = "Nola go to work \\o/";
    public string TextDayFailure = "Nola is exhausted, end of the day";

    [Header("Actions")]
    public Color colorTextWhenActionSucessful;
    public Color colorTextWhenActionFailed;
    public string TextWhenActionSucessful = "action successfully done.";
    public string TextWhenActionFailed = "action failed.";

    [Header("mood background")]
    public Color colorBackgroundCalm;
    public Color colorBackgroundAngry;
    public Color colorBackgroundAnxious;
}
