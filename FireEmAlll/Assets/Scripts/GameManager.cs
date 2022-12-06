using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Chars")]
    public GameObject femaleChar;
    public GameObject maleChar;

    Animator femaleAnimator;
    Animator maleAnimator;

    [Header("Particles")]
    public ParticleSystem starFall;
    public ParticleSystem moneyFall;
    public ParticleSystem sadEmoji;
    public ParticleSystem angryEmoji;

    UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

        femaleAnimator = femaleChar.GetComponent<Animator>();
        maleAnimator= maleChar.GetComponent<Animator>();

        StartCoroutine(StartFemaleMove());
    }

    public IEnumerator StartFemaleMove()
    {
        yield return new WaitForSeconds(1f);

        femaleAnimator.SetTrigger("begin");

        femaleChar.transform.DOMove(new Vector3(femaleChar.transform.position.x, femaleChar.transform.position.y, -1.5f)
            , 1f).OnComplete(() =>
            {
                femaleAnimator.SetTrigger("idle");
                femaleChar.transform.DORotate(new Vector3(0, 90, 0),.5f).OnComplete(() =>
                    {
                        femaleAnimator.SetTrigger("start");

                        StartCoroutine(uiManager.ShowStep1());  
                    });
            });
    }

    public IEnumerator FemaleExitMove()
    {
        yield return new WaitForSeconds(4);

        ManageStarFallVFX(false);

        femaleAnimator.SetTrigger("end");

        yield return new WaitForSeconds(2f);

        femaleChar.transform.DORotate(new Vector3(0, 0, 0),
                    .5f).OnComplete(() =>
                    {
                        femaleAnimator.SetTrigger("exit");

                        femaleChar.transform.DOMove(new Vector3(femaleChar.transform.position.x
                            , femaleChar.transform.position.y, 1.5f), 2f).OnComplete(() =>
                            {
                                femaleChar.SetActive(false);

                                StartCoroutine(StartMaleMove());
                            });
                    });
    }

    public IEnumerator StartMaleMove()
    {
        yield return new WaitForSeconds(.2f);

        maleAnimator.SetTrigger("begin");

        maleChar.transform.DOMove(new Vector3(maleChar.transform.position.x, maleChar.transform.position.y, -1.5f)
            , 2f).OnComplete(() =>
            {
                maleAnimator.SetTrigger("idle");
                maleChar.transform.DORotate(new Vector3(0, 90, 0),.5f).OnComplete(() =>
                    {
                        maleAnimator.SetTrigger("start");

                        StartCoroutine(uiManager.ShowStep2());
                    });
            });
    }

    public IEnumerator MaleExitMove()
    {
        yield return new WaitForSeconds(4);

        ManageStarFallVFX(false);

        maleAnimator.SetTrigger("end");

        yield return new WaitForSeconds(2f);

        maleChar.transform.DORotate(new Vector3(0, 0, 0),
                    .5f).OnComplete(() =>
                    {
                        maleAnimator.SetTrigger("exit");

                        maleChar.transform.DOMove(new Vector3(maleChar.transform.position.x
                            , maleChar.transform.position.y, 1.5f), 2f).OnComplete(() =>
                            {
                                maleChar.SetActive(false);

                                //ShakeCam();

                                ManageMoneyFallVFX(true);

                                StartCoroutine(uiManager.FinishTextAnimation());
                            });
                    });
    }

    public void ShakeCam()
    {
        Camera cam = FindObjectOfType<Camera>();

        cam.DOShakePosition(.5f, 1, 10);
    }

    public void Step1Choice(bool choice)
    {
        if(choice)
        {
            femaleAnimator.SetTrigger("right");
        }
        else
        {
            femaleAnimator.SetTrigger("wrong");
        }
    }

    public void Step2Choice(bool choice)
    {
        if (choice)
        {
            maleAnimator.SetTrigger("right");
        }
        else
        {
            maleAnimator.SetTrigger("wrong");
        }
    }

    public void ManageStarFallVFX(bool play)
    {
        if(play)
        {
            if(!starFall.isPlaying)
            {
                starFall.Play();
            }  
        }
        else
        {
            if(starFall.isPlaying)
            {
                starFall.Stop();
            }
        }     
    }

    public void ManageMoneyFallVFX(bool play)
    {
        if (play)
        {
            if (!moneyFall.isPlaying)
            {
                moneyFall.Play();
            }
        }
        else
        {
            if (moneyFall.isPlaying)
            {
                moneyFall.Stop();
            }
        }
    }

    public IEnumerator PlayEmoji(int i)
    {
        yield return new WaitForSeconds(.8f);

        if(i == 0)
            sadEmoji.Play();
        else if(i == 1)
            angryEmoji.Play();
    }

}
