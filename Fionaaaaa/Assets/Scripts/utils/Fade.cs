using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    static public Fade instance;

    public Image FadeImage;
    public Text FadeText;
    public float fadeSpeed = 1;
    public float waitingTime = 2f;

    private Coroutine actualCoroutine;
    private bool flagCoroutine;

    [Range(0,1)]
    public float alpha = 0;


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

    public void FadeIn()
    {
        checkActualCoroutine();
        actualCoroutine = StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        checkActualCoroutine();
        actualCoroutine = StartCoroutine(FadeOutCoroutine());
    }


    void checkActualCoroutine()
    {
        if (flagCoroutine)
        {
            StopCoroutine(actualCoroutine);
        }
    }


    IEnumerator FadeInCoroutine()
    {
        flagCoroutine = true;
        while (alpha < 1)
        {
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, alpha);
            FadeText.color = new Color(FadeText.color.r, FadeText.color.g, FadeText.color.b, alpha);
            alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        flagCoroutine = false;

        yield return new WaitForSecondsRealtime(waitingTime);
        SceneManager.LoadScene(0);


        FadeOut();
    }

    IEnumerator FadeOutCoroutine()
    {
        flagCoroutine = true;
        while (alpha > 0)
        {
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, alpha);
            FadeText.color = new Color(FadeText.color.r, FadeText.color.g, FadeText.color.b, alpha);
            alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        flagCoroutine = false;
    }




}
