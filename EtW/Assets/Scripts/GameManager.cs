using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool isreinforced = false;
    
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        
    }

    void Update()
    {
        
    }
}
