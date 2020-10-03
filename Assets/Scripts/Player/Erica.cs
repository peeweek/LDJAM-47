using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Erica : MonoBehaviour
{
    public static Erica instance;
    public Text Text;

    public void OnEnable()
    {
        if (instance != null)
            Debug.LogWarning("Already found Erica in scene");
        instance = this;
        SetDialogueText("");
    }

    public void OnDisable()
    {
        if (instance == this)
            instance = null;
        else
            Debug.LogWarning("Already found Erica in scene");
    }

    public void SetDialogueText(string text)
    {
        Text.text = text;
        Text.Rebuild(CanvasUpdate.Layout);
    }

}
