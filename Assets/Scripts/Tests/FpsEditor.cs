using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FpsEditor : MonoBehaviour
{
    float count;
    IEnumerator Start()
    {
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void OnGUI()
    {
        GUIStyle size = new GUIStyle("label");
        size.fontSize = 30;
        GUI.Label(new Rect(1800, 50, 100, 100), "FPS: " + Mathf.Round(count),size);
    }
}

