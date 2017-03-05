using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Text MatchesLeft;

    void Update()
    {
        MatchesLeft.text = "Matches Left: " + GameObject.Find("Main").GetComponent<Main>().matchCount.ToString();           // Update number of remaining Matches at realtime
    }
}
