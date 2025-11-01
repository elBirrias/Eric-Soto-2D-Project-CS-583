using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour
{
    public static GemCounter instance;

    public TMP_Text gemText;
    public int currentGems = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gemText.text = "Gems Collected: " + currentGems.ToString();
    }

    public void IncreaseGems(int v)
    {
        currentGems += v;
        gemText.text = "Gems Collected: " + currentGems.ToString();
    }
}
