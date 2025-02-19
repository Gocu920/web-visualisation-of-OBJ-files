using SFB;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class SaveFile : MonoBehaviour, IPointerDownHandler
{
    public OpenFIle openFileClass;
#if UNITY_WEBGL && !UNITY_EDITOR
    //WEBGL
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string fileName, byte[] byteArray, int byteArraySize);
    public void OnPointerDown(PointerEventData eventData) 
    {
        if(OpenFIle.textStream==null)
        {
            openFileClass.error.text="Nothing to save";
        }
        else
        {
            var bytes = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(OpenFIle.textStream.ToArray()));
            DownloadFile(gameObject.name, "OnFileDownload", "model.obj", bytes, bytes.Length);
        }
    }
    public void OnFileDownload() { }
#endif
#if UNITY_EDITOR
    public void OnPointerDown(PointerEventData eventData) { }

    public void OnClickSave()
    {
        if (OpenFIle.textStream == null)
        {
            openFileClass.error.text = "Nothing to save";
        }
        else
        {
            string path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "model", "obj");
            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, Encoding.UTF8.GetString(OpenFIle.textStream.ToArray()));
            }
        }
    }
#endif
}
