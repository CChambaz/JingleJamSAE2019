using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectDontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        DontDestroyOnLoad(this);
    }
}
