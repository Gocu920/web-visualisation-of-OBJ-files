using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Xml.Linq;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine.EventSystems;

public class NewTestScript:InputTestFixture
{
    Mouse mouse;
    public override void Setup()
    {
        base.Setup();
        SceneManager.LoadScene("App Scene");
        mouse = InputSystem.AddDevice<Mouse>();
    }
    public void ClickUI(GameObject element)
    { 
        Move(Mouse.current.position, element.transform.position);
        Click(mouse.leftButton);
    }

    [UnityTest, Order(1)]
    public IEnumerator ErrorTest()
    {
        GameObject saveButton = GameObject.Find("Canvas/SaveFileButton");
        yield return new WaitForSeconds(2f);
        ClickUI(saveButton);
        yield return new WaitForSeconds(2f);
        GameObject errorInfo = GameObject.Find("Canvas/ErrorInfo");
        string result = errorInfo.GetComponent<TMP_Text>().text;
        string expectedResult = "Nothing to save";
        Assert.That(result, Is.EqualTo(expectedResult));

    }
    [UnityTest, Order(2)]

    public IEnumerator ErrorTest2()
    { 
        GameObject openButton = GameObject.Find("Canvas/OpenFileButton");
        GameObject saveButton = GameObject.Find("Canvas/SaveFileButton");
        ClickUI(openButton);
        yield return new WaitForSeconds(7f);
        ClickUI(saveButton);
        yield return new WaitForSeconds(2f);
        GameObject errorInfo = GameObject.Find("Canvas/ErrorInfo");
        string result = errorInfo.GetComponent<TMP_Text>().text;
        string expectedResult = "Nothing to save";
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [UnityTest, Order(3)]
    public IEnumerator CorrectLoadingAndSavingTest()
    {
        GameObject model3D;
        GameObject openButton = GameObject.Find("Canvas/OpenFileButton");
        ClickUI(openButton);
        yield return new WaitForSeconds(7f);
        model3D = GameObject.Find("WavefrontObject");
        string sceneName = SceneManager.GetActiveScene().name;
        Assert.That(sceneName, Is.EqualTo("App Scene"));
        Assert.That(model3D.name, Is.EqualTo("WavefrontObject"));
        GameObject saveButton = GameObject.Find("Canvas/SaveFileButton");
        yield return new WaitForSeconds(2f);
        ClickUI(saveButton);
        yield return new WaitForSeconds(7f);
    }
}
