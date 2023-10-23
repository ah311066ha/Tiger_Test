using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class disable_stop : MonoBehaviour
{
    public Button endTurnButton;
 
    public void StartTimer() //Call this from OnClick
    {
        StartCoroutine(TimeoutEndTurnButton());
    }
    public IEnumerator TimeoutEndTurnButton()
    {
        endTurnButton.interactable = false;
        yield return new WaitForSeconds(1f);
        endTurnButton.interactable = true;
    }
}
