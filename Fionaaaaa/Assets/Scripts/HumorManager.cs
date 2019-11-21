using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumorManager : MonoBehaviour
{
    static public HumorManager instance;

    [HideInInspector]
    public Text HumeurUI;

    public HumorSettings settingCalme;
    public HumorSettings settingColere;
    public HumorSettings settingAnxieuse;

    public GameSettings gameSettings;

    List<HumorState> historiqueHumeur = new List<HumorState>();

    public List<humeurScriptee> humeursScriptees;

    public HumorState actualHumor = HumorState.vide;

    public int numberOfDaysPassed = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }


    public void ChangeHumor()
    {
// if n is scripted, we set the humor of the day to this scripted humor
        foreach(humeurScriptee script in humeursScriptees)
        {
            if(script.jour == numberOfDaysPassed)
            {
                historiqueHumeur.Add(script.humeur);
                setActualHumor(script.humeur);
                return;
            }
        }


        List<humeurChance> possibilities = createListPossibilities();
       
// check if there is a n+1 scrypted humor, and remove it from the possibilities
        foreach(humeurScriptee script in humeursScriptees)
        {
            if(script.jour == numberOfDaysPassed + 1)
            {
                removeHumor(possibilities, script.humeur);
                //possibilities.Remove(script.humeur);
            }
        }

// check the n-1 humor and remove it from the possibilities
        if(historiqueHumeur.Count > 0)
        {
            removeHumor(possibilities, historiqueHumeur[historiqueHumeur.Count - 1]);
            //possibilities.Remove(historiqueHumeur[historiqueHumeur.Count - 1]);
        }

// attribute chance for each remaining humors
        float chanceValue = 0;
        foreach(humeurChance hc in possibilities)
        {
            hc.chance = chanceValue;
            chanceValue += 1 / (float)possibilities.Count;
        }

// choose the humor of the day
        float random = Random.Range(0f, 1f);
        for(int i = possibilities.Count - 1; i >= 0; i--)
        {
            if(random > possibilities[i].chance)
            {
                historiqueHumeur.Add(possibilities[i].humeur);
                setActualHumor(possibilities[i].humeur);
                return;
            }
        }

    }

    public int getInitialHumorEnergy()
    {
        switch (actualHumor)
        {
            case HumorState.anxieuse:
                return settingAnxieuse.energieDeDepart;

            case HumorState.calme:
                return settingCalme.energieDeDepart;

            case HumorState.colerique:
                return settingColere.energieDeDepart;

            default:
                Debug.LogWarning("actualHumor not initialized");
                return settingCalme.energieDeDepart;

        }
    }

    public float getTauxReussite()
    {
        switch (actualHumor)
        {
            case HumorState.anxieuse:
                return settingAnxieuse.tauxReussite;

            case HumorState.calme:
                return settingCalme.tauxReussite;

            case HumorState.colerique:
                return settingColere.tauxReussite;

            default:
                Debug.LogWarning("actualHumor not initialized");
                return settingCalme.tauxReussite;

        }
    }

    private void setActualHumor(HumorState state)
    {
        actualHumor = state;
        UpdateUI();
    }



    private List<humeurChance> createListPossibilities()
    {
        humeurChance anxieuse = new humeurChance(0, HumorState.anxieuse);
        humeurChance calme = new humeurChance(0, HumorState.calme);
        humeurChance colerique = new humeurChance(0, HumorState.colerique);

        List<humeurChance> list = new List<humeurChance> { anxieuse, calme, colerique };
        return list;
    }


    private bool removeHumor(List<humeurChance> list, HumorState humeur)
    {

        for(int i  = 0; i < list.Count; i++)
        {
            if(list[i].humeur == humeur)
            {
                list.RemoveAt(i);
                return true;
            }
        }
        return false;

    }

    private void UpdateUI()
    {
        switch (actualHumor)
        {
            case HumorState.anxieuse:
                HumeurUI.text = "Mood : anxious";
                Camera.main.backgroundColor = gameSettings.colorBackgroundAnxious;
                break;

            case HumorState.calme:
                HumeurUI.text = "Mood : calm";
                Camera.main.backgroundColor = gameSettings.colorBackgroundCalm;
                break;

            case HumorState.colerique:
                HumeurUI.text = "Mood : angry";
                Camera.main.backgroundColor = gameSettings.colorBackgroundAngry;
                break;

            default:
                HumeurUI.text = "Mood : bug";
                break;
        }
    }


    [System.Serializable]
    public struct humeurScriptee
    {
        public int jour;
        public HumorState humeur;
    }

    public class humeurChance
    {
        public float chance;
        public HumorState humeur;

        public humeurChance(float chance, HumorState humeur)
        {
            this.chance = chance;
            this.humeur = humeur;
        }
    }

   /* private void OnValidate()
    {
        UpdateUI();
    }*/

}
