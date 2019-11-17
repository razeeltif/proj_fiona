using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image FadeImage;


    private Coroutine actualCoroutine;
    private bool flagCoroutine;

    [Range(0,1)]
    public float alpha = 0;

    void FadeIn()
    {
        checkActualCoroutine();
        actualCoroutine = StartCoroutine(FadeInCoroutine());
    }

    void FadeOut()
    {
        checkActualCoroutine();
        actualCoroutine = StartCoroutine(FadeInCoroutine());
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
        while(alpha < 1)
        {
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, alpha);
            alpha += Time.deltaTime;
            yield return null;
        }


    }

    IEnumerator FadeOutCoroutine()
    {
        while (alpha > 0)
        {
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, alpha);
            alpha -= Time.deltaTime;
            yield return null;
        }
    }




}
