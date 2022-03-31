using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sencesprit : MonoBehaviour
{
    // Start is called before the first frame update
   public void Go_To_Game_Sence()
    {
        SceneManager.LoadScene("Ex Scene");
    }
}
