using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndDialogue()
    {

        foreach(GameObject go in sentences)
        {
            Destroy(go);
        }
        sentences.Clear();

    }



    public void startNewSentence(bool isNola, string sentence)
    {

        checkTypeCoroutine();

        AddNewTextToDialogueBox(isNola, sentence);

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
