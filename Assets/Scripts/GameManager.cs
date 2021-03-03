using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadSceneAsync("2MainMenu", LoadSceneMode.Additive);
    }
}
