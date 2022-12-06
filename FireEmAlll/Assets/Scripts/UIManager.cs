using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{   
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI finishText;

    [Header("Steps Panels")]
    public GameObject step1;
    public GameObject step2;

    GameManager gameManager;

    Vector3 messageTextStartPos;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        messageTextStartPos = messageText.transform.position;
    }

    public void PressHireButton()
    {
        gameManager.Step1Choice(true);

        MessageTextAnimation("Correct!!", true, 1);

        gameManager.ManageStarFallVFX(true);

        StartCoroutine(gameManager.FemaleExitMove());
    }

    public void PressDismissButton()
    {
        gameManager.Step1Choice(false);

        MessageTextAnimation("Bad Decision!!", false, 1);

        StartCoroutine(gameManager.PlayEmoji(0));

        StartCoroutine(gameManager.FemaleExitMove());
    }

    public void PressFireButton()
    {
        gameManager.Step2Choice(true);

        MessageTextAnimation("Good Job!!", true, 2);

        StartCoroutine(gameManager.PlayEmoji(1));

        StartCoroutine(gameManager.MaleExitMove());
    }

    public void PressPromoteButton()
    {
        gameManager.Step2Choice(false);

        MessageTextAnimation("Bad Choice!!", false, 2);

        StartCoroutine(gameManager.MaleExitMove());
    }

    public IEnumerator ShowStep1()
    {
        yield return new WaitForSeconds(3f);

        step1.transform.localScale = Vector3.zero;

        step1.SetActive(true);

        step1.transform.DOScale(1f, .8f).SetEase(Ease.InOutQuad);
    }

    public IEnumerator ShowStep2()
    {
        yield return new WaitForSeconds(3.5f);

        step2.transform.localScale = Vector3.zero;

        step2.SetActive(true);

        step2.transform.DOScale(1f, .8f).SetEase(Ease.InOutQuad);
    }

    public void MessageTextAnimation(string text, bool correct, int step)
    {
        messageText.transform.position = messageTextStartPos;
        messageText.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        messageText.alpha= 1f;

        messageText.gameObject.SetActive(true);

        messageText.text = text;

        if (correct)
        {   
            messageText.color = Color.green;          
        }
        else
        {
            messageText.color = Color.red;
        }

        messageText.rectTransform.DOAnchorPosY(1200, 2f).SetEase(Ease.InBack);
        messageText.transform.DOScale(1.5f, 2f).SetEase(Ease.InOutExpo);
        messageText.DOFade(0f, 2f).SetEase(Ease.InExpo);

        if(step == 1)
            step1.transform.DOScale(0, 1f).SetEase(Ease.InOutQuad).OnComplete(() => step1.SetActive(false));
        else if(step == 2)
            step2.transform.DOScale(0, 1f).SetEase(Ease.InOutQuad).OnComplete(() => step2.SetActive(false));

    }

    public IEnumerator FinishTextAnimation()
    {
        finishText.gameObject.SetActive(true);

        finishText.rectTransform.DOAnchorPosY(200, 1f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(2f);

        finishText.rectTransform.DOAnchorPosY(-400, 1f).SetEase(Ease.InBack).OnComplete(() => finishText.gameObject.SetActive(false));


    }

}
