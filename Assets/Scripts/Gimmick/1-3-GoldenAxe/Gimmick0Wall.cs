using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick0Wall : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> clickBoxObjects = new();

    public void TrialClear(int listValue)
    {
        for (int i = 0; i < clickBoxObjects.Count; i++)
        {
            clickBoxObjects[i].SetActive(false);
        }
        clickBoxObjects[listValue].SetActive(true);
        Destroy(gameObject);
    }
}
