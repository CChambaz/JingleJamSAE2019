using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class MoldSelector : MonoBehaviour {
    [SerializeField] private GameObject[] moldSprites;
    private MoldManager moldManager;

    // Start is called before the first frame update
    void Start()
    {
        moldManager = FindObjectOfType<MoldManager>();
        foreach (GameObject moldSprite in moldSprites) {
            moldSprite.SetActive(false);
        }
        moldSprites[moldManager.SelectedMold].SetActive(true);
    }

    public void MoveSelector(int arrowIndex) {
        moldSprites[moldManager.SelectedMold].SetActive(false);
        moldManager.SelectedMold += arrowIndex;
        moldSprites[(int)moldManager.SelectedMold].SetActive(true);
    }
}
