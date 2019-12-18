using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private Canvas menu;
    private bool inPause = false;

    public void BtnPlay()
    {
        menu.gameObject.GetComponentsInChildren<RectTransform>()[1].rect.Set(0, 200, 0, 0);
        menu.GetComponentsInChildren<Button>()[0].GetComponent<CanvasGroup>().interactable = false;
        menu.GetComponentsInChildren<Button>()[0].GetComponent<CanvasGroup>().alpha = 0;
        menu.GetComponentsInChildren<Button>()[0].GetComponent<CanvasGroup>().blocksRaycasts = false;

        menu.GetComponentsInChildren<Button>()[1].GetComponent<CanvasGroup>().interactable = true;
        menu.GetComponentsInChildren<Button>()[1].GetComponent<CanvasGroup>().alpha = 1;
        menu.GetComponentsInChildren<Button>()[1].GetComponent<CanvasGroup>().blocksRaycasts = true;
        SceneManager.LoadScene(1);
    }

    public void BtnReturnMainMenu()
    {
        menu.gameObject.GetComponentsInChildren<RectTransform>()[1].rect.Set(0, 0, 0, 0);
        menu.GetComponentsInChildren<Button>()[1].GetComponent<CanvasGroup>().interactable = false;
        menu.GetComponentsInChildren<Button>()[1].GetComponent<CanvasGroup>().alpha = 0;
        menu.GetComponentsInChildren<Button>()[1].GetComponent<CanvasGroup>().blocksRaycasts = false;

        menu.GetComponentsInChildren<Button>()[0].GetComponent<CanvasGroup>().interactable = true;
        menu.GetComponentsInChildren<Button>()[0].GetComponent<CanvasGroup>().alpha = 1;
        menu.GetComponentsInChildren<Button>()[0].GetComponent<CanvasGroup>().blocksRaycasts = true;

        BtnPause();
        Destroy(menu.gameObject);
        Destroy(GameObject.FindObjectOfType<AudioConfig>().gameObject);
        SceneManager.LoadScene(0);
    }

    public void BtnPause()
    {
        inPause = !inPause;
        if (inPause)
        {
            menu.GetComponent<CanvasGroup>().interactable = true;
            menu.GetComponent<CanvasGroup>().alpha = 1;
            menu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            menu.GetComponent<CanvasGroup>().interactable = false;
            menu.GetComponent<CanvasGroup>().alpha = 0;
            menu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    private void Update()
    {

    }
}
