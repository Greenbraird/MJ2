using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fillAmountImg : MonoBehaviour {
    public float totalTime ;
    private float fillAmount = 1;
    private Image myImage;

    private void Awake() {
        totalTime = 60;// TimerScript.Timer;
        myImage = gameObject.GetComponent<Image>();
    }

    void Update() {
        if (TimerScript.updateTime) {
            if (fillAmount > 0) {
                myImage.fillAmount -= Time.deltaTime / totalTime;
            }
        }
    }
}
