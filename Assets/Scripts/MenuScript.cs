using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
//Callandra Ruiter, GDD316 2024

//this script manages the menu and allows the player to choose which scene to load through buttons

public class MenuScript : MonoBehaviour
{

    //loads scene 1
    public void LoadScene1()
    {
        SceneManager.LoadScene("Scene 1");
    }

    //loads scene 2
    public void LoadScene2()
    {
        SceneManager.LoadScene("Scene 2");
    }

    //loads scene 3
    public void LoadScene3()
    {
        SceneManager.LoadScene("Scene 3");
    }

    //loads scene 4
    public void LoadScene4()
    {
        SceneManager.LoadScene("Scene 4");
    }

    //loads scene 5
    public void LoadScene5()
    {
        SceneManager.LoadScene("Scene 5");
    }
}
