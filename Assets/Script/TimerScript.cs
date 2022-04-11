using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimerScript : MonoBehaviour
{
    float timer;
    void Start()
    {
        timer = 10;
        Timer();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);   
    }

    void Timer()
    {
        DOTween.To(() => timer, x => timer = x, 0, 10);
    }

}
