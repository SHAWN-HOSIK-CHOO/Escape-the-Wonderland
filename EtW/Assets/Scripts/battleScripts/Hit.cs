/*
 * Author : GwangTae Jo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hit : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI critical;

    void Start()
    {
        
    }

    public void SetCriticalText(string text) {
        this.critical.SetText(text);
    }


    void Update()
    {
        
    }
}
