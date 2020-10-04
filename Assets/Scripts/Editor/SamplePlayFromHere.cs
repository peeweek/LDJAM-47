using UnityEditor;
using UnityEngine;
using GameplayIngredients.Editor;

static class SamplePlayFromHere
{
    [InitializeOnLoadMethod]
    static void SetupPlayFromHere()
    {
        PlayFromHere.OnPlayFromHere += PlayFromHere_OnPlayFromHere;
    }

    private static void PlayFromHere_OnPlayFromHere(Vector3 position, Vector3 forward)
    {
    }
}
