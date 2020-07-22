using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactsManager : MonoBehaviour
{
    public TextMeshProUGUI factText;
    public TextMeshProUGUI continuingText;
    public string[] facts;

    private int time = 10;

    private void OnEnable()
    {
        factText.SetText(facts[Random.Range(0, facts.Length - 1)]);
        time = 10;
        StartCoroutine(UpdateTimeText());
        Invoke("ActivateContinueButton", 10f);
    }

    private void ActivateContinueButton()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator UpdateTimeText()
    {
        for (int i = 10; i > 0; i--)
        {
            continuingText.SetText("Game continuing in " + (i).ToString() + " seconds ...");
            yield return new WaitForSeconds(1f);
        }
    }
}
