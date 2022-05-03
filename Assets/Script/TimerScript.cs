using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    //Text Mesh Pro UHUI를 불러온다.
    private TextMeshProUGUI textmash;
    public int Timer;
    int delayTime;

    void Awake()
    {
        //textmash를 정의 해준다
        textmash = GetComponent<TextMeshProUGUI>();
        Timer = 40;
        delayTime = Timer;
    }
    void Update()
    {
        //Debug.Log(Timer);
        //매번 fram 마다 textmash의 text를 초에 맞게 맞추어 준다.
        textmash.text = Timer.ToString();

        //Timer이 0이라면 TimerScene씬을 다시 시작한다.
        if (Timer == 0)
        {
            SceneManager.LoadScene("TimerScene");
        }
    }

    public void TimerStart()
    {
        //코루딘을 통해 3초를 기다려 준다.
        StartCoroutine(TimerStratco());
    }

    IEnumerator TimerStratco()
    {
        yield return new WaitForSeconds(3);
        //타이머가 되는 직접적인 스크립트
        DOTween.To(() => Timer, x => Timer = x, 0, delayTime);
    }
}
