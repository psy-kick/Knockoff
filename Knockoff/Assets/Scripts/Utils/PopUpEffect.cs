using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PopUpEffect : MonoBehaviour
{
    [SerializeField] private GameObject popUp;
    [SerializeField] private float effectValue = 0.5f;
    [SerializeField] private float duration = 0.5f;

    private void Start()
    {
        if (effectValue == 0)
            ClosePopUP();
    }

    private void OnEnable()
    {
        OpenPopUp();
    }

    private void OnDisable()
    {
        ClosePopUP();
    }

    public void OpenPopUp()
    {
        popUp.GetComponent<Image>().DOFade(0f, duration);
    }

    public void ClosePopUP()
    {
        popUp.GetComponent<Image>().DOFade(0f, duration);
    }
}
