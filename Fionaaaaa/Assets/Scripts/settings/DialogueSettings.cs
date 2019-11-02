using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class DialogueSettings : ScriptableObject
{

    public string phraseSol;
    public List<reponse> dialogueCalme;
    public List<reponse> dialogueAnxieuse;
    public List<reponse> dialogueColerique;



}

[System.Serializable]
public struct reponse
{
    public string reponseNola;
    public string reactionSol;
    public int impactEnergie;
}
