using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Memory")]
public class MemorySettings : ScriptableObject
{

    public memoriesEnergies memoireCalme;
    public memoriesEnergies memoireAnxieuse;
    public memoriesEnergies memoireColerique;



}

[System.Serializable]
public struct memoriesEnergies
{
    public string[] memories;
    public int impactEnergie;
}
