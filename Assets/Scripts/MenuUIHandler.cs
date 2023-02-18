using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Remember to encase using UnityEditor in a compiler if, UnityEditor can't be imported into a build
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        MainManager.instance.teamColor = color;
    }

    //These methods make the color selection buttons work
    public void SaveColorClicked()
    {
        MainManager.instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        MainManager.instance.LoadColor();
    }
    
    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;
        //In case the player did not change the color, this will select the color loaded into the MainManager by the Save/Load functionality
        ColorPicker.SelectColor(MainManager.instance.teamColor);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        //Saves the current color before exiting
        MainManager.instance.SaveColor();
        //The lines preceded by # are compiler instructions
#if (UNITY_EDITOR)
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
