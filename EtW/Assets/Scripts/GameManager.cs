using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] public bool           isreinforced = false;
    [SerializeField] public GameObject     bspMapGenerator;
    [SerializeField] public GameObject     monsterGenerator;
    [SerializeField] public GameObject     ccamera;
    [SerializeField] public GameObject     player;
     private                 RoomDungeonGen _roomDungeonGen;
     private                 MonsterPcg     _monsterPcg;
    
    
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start()
    {
        _roomDungeonGen = bspMapGenerator.GetComponent<RoomDungeonGen>();
        _roomDungeonGen.GenerateDungeon();
        player.transform.position = new Vector3(_roomDungeonGen.playerStartPos.x, _roomDungeonGen.playerStartPos.y, -10);
        ccamera.transform.position = player.transform.position;

        _monsterPcg = monsterGenerator.GetComponent<MonsterPcg>();
        _monsterPcg.ProceduralGenMonsters();
    }

    void Update()
    {
        
    }
}
