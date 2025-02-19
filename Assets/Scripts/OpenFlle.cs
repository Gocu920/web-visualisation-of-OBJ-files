using SFB;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using Dummiesman;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System.Security.Cryptography;
using UnityEngine.TextCore.Text;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices.WindowsRuntime;

public class OpenFIle : MonoBehaviour, IPointerDownHandler 
{ 
    GameObject model;
    public TextMeshProUGUI error;
    public static MemoryStream textStream;
    static public Bounds bound;
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);
    public void OnPointerDown(PointerEventData eventData) 
    {
       UploadFile(gameObject.name, "OnFile", ".obj", false);
    }
    public void OnFile(string url)
    {
        StartCoroutine(OutputRoutineOpen(url));
    }
#endif

#if UNITY_EDITOR
    public void OnPointerDown(PointerEventData eventData) { }
    public void OnClickOpen()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        if (paths.Length > 0)
        {
            var urlArr = new List<string>(paths.Length);
            for (int i = 0; i < paths.Length; i++)
            {
                urlArr.Add(new System.Uri(paths[i]).AbsoluteUri);
            }
            StartCoroutine(OutputRoutineOpen(urlArr[0]));
        }
    }
#endif
    public IEnumerator OutputRoutineOpen(string url)
    {
        error.text = "";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success)
        {
            textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.downloadHandler.text));
            if (model != null)
            {
                Destroy(model);
            }
            model = new OBJLoader().Load(textStream);
            bound = GetBound(model);
            Camera.main.transform.position = bound.center;
            model.AddComponent<Spin>();
        }
        else
        {
            error.text ="Error occurred!";
            yield break;
        }
    }
    Bounds GetBound(GameObject gameObj)
    {
        Bounds bound = new Bounds(gameObj.transform.position, Vector3.zero);
        var rList = gameObj.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer r in rList)
        {
            bound.Encapsulate(r.bounds);
        }
        return bound;
    }
}