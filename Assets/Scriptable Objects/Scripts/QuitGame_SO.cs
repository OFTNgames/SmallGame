using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quit")]
public class QuitGame_SO : ScriptableObject
{
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
