using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNum : MonoBehaviour
{
    public static Text map_num_text;
    void Start() {
        map_num_text = GameObject.Find("GameManager Canvas/Map number").GetComponent<Text>();
    }

    public static int stage_cnt = 1;

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        map_num_text.text = "Map " + stage_cnt.ToString();
    }
}
