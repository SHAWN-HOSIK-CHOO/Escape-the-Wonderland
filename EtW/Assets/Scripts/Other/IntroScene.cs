using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    public TextMeshProUGUI start;
    public TextMeshProUGUI exit;
    public Animator logo;

    public void StartSceneMove() {
        logo.SetTrigger("start");
        StartCoroutine("SceneMove");
    }

    public void Exit() {
        Application.Quit();
    }

    IEnumerator SceneMove() {
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene("DungeonScene00");
    }

    private bool IsPointerOverUIElement(out string uiElementName)
    {
        uiElementName = null;

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject != null)
            {
                uiElementName = result.gameObject.name;
                return true;
            }
        }

        return false;
    }

    private void Update() {
    if (IsPointerOverUIElement(out string uiElementName))
    {
        if (uiElementName == "start") {
            start.color = new Color(255, 255, 255, 1);
            exit.color = new Color(255, 255, 255, 0.67f);
        } else if (uiElementName == "exit") {
            start.color = new Color(255, 255, 255, 0.67f);
            exit.color = new Color(255, 255, 255, 1);
        } else {
            start.color = new Color(255, 255, 255, 0.67f);
            exit.color = new Color(255, 255, 255, 0.67f);
        }
    }
    }
}
