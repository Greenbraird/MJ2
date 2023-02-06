using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; //텍스트 깔끔하게 만들어주는 툴

public class GMTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI GMtextmash;

    public static float GMtimer = 3f;
    public static bool GMupdateTime = false;
    void Awake() {
        GMtextmash = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        GMtimer = 3f;
    }

    void Update()
    {
        /// Game Start Timer
        if (GMupdateTime) {
            GMtimer -= Time.deltaTime;
            GMtextmash.text = ((int)GMtimer + 1).ToString();
            if (GMtimer <= 0) {
                GMtimer = 3f;
                GMupdateTime = false;
            }
        }
    }
}
