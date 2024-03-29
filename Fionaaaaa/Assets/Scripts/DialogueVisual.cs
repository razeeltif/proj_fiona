﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueVisual : MonoBehaviour
{

    static public DialogueVisual instance;

    public DialogueVisualSettings settings;
    public GameObject NolaTextPrefab;
    public GameObject SolTextPrefab;
    public GameObject buttonPrefab;
    public GameObject choiceCanvas;

    private List<GameObject> listButtons = new List<GameObject>();
    List<GameObject> sentences = new List<GameObject>();

    private Coroutine typeCoroutine;
    private bool flagTypeCoroutine;

    private string savedSentence;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        choiceCanvas.SetActive(false);
    }


    public void EndDialogue()
    {

        foreach(GameObject go in sentences)
        {
            Destroy(go);
        }
        sentences.Clear();
        StopCoroutine(typeCoroutine);
        flagTypeCoroutine = false;

    }



    public void startNewSentence(bool isNola, string sentence)
    {

        checkTypeCoroutine();

        AddNewTextToDialogueBox(isNola, sentence);

    }

    public void startNewSentence(bool isNola, string sentence, Color fancyColor)
    {

        checkTypeCoroutine();

        AddNewTextToDialogueBox(isNola, sentence, fancyColor);

    }

    private void checkTypeCoroutine()
    {
        // if the typing coroutine is running, we stop it and we directly write the end of the sentence
        if (flagTypeCoroutine)
        {
            StopCoroutine(typeCoroutine);
            sentences[sentences.Count - 1].GetComponent<TextMeshProUGUI>().text = savedSentence;
        }
    }

    private void AddNewTextToDialogueBox(bool isNola, string sentence)
    {
        GameObject newText;
        Vector3 pos;
        if (isNola)
        {
            newText = Instantiate(NolaTextPrefab, this.gameObject.GetComponentInChildren<Image>().transform);
            pos = NolaTextPrefab.GetComponent<RectTransform>().position;
        }
        else
        {
            newText = Instantiate(SolTextPrefab, this.gameObject.GetComponentInChildren<Image>().transform);
            pos = SolTextPrefab.GetComponent<RectTransform>().position;
        }
        newText.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(pos.x, sentences.Count * -settings.espacementEntreChaqueLigne, pos.z);

        sentences.Add(newText);
         

        typeCoroutine = StartCoroutine(TypeSentence(newText.GetComponent<TextMeshProUGUI>(), sentence));

        savedSentence = sentence;
    }

    private void AddNewTextToDialogueBox(bool isNola, string sentence, Color fancyColor)
    {
        GameObject newText;
        Vector3 pos;
        if (isNola)
        {
            newText = Instantiate(NolaTextPrefab, this.gameObject.GetComponentInChildren<Image>().transform);
            pos = NolaTextPrefab.GetComponent<RectTransform>().position;
        }
        else
        {
            newText = Instantiate(SolTextPrefab, this.gameObject.GetComponentInChildren<Image>().transform);
            pos = SolTextPrefab.GetComponent<RectTransform>().position;
        }
        newText.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(pos.x, sentences.Count * -settings.espacementEntreChaqueLigne, pos.z);

        sentences.Add(newText);

        newText.GetComponent<TextMeshProUGUI>().color = fancyColor;
        typeCoroutine = StartCoroutine(TypeSentence(newText.GetComponent<TextMeshProUGUI>(), sentence));

        savedSentence = sentence;
    }


    public void CreateChoices(List<reponse> listReponse)
    {
        choiceCanvas.SetActive(true);
        for (int i = 0; i < listReponse.Count; i++)
        {
            CreateButton(i, listReponse[i].reponseNola, 135 - 50 * i);
        }
    }

    private void CreateButton(int indexValue, string textValue, float offset)
    {

        GameObject buttonInstance = Instantiate(buttonPrefab, this.transform);


        Vector3 pos = buttonPrefab.GetComponent<RectTransform>().anchoredPosition3D;

        buttonInstance.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(pos.x, offset, pos.z);

        buttonInstance.GetComponent<Button>().onClick.AddListener(delegate { ChoiceButtonHandler(indexValue); });
        buttonInstance.GetComponentInChildren<TextMeshProUGUI>().text = textValue;

        listButtons.Add(buttonInstance);
    }

    public void ChoiceButtonHandler(int index)
    {
        DialogueManager.instance.ChoiceMade(index);
    }

    public void EndChoice()
    {
        foreach (GameObject go in listButtons)
        {
            Destroy(go);
        }
        listButtons.Clear();
        choiceCanvas.SetActive(false);
    }


    IEnumerator TypeSentence(TextMeshProUGUI dialogueText, string sentence)
    {
        flagTypeCoroutine = true;
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        flagTypeCoroutine = false;
    }
}
