/*
 * Author : Hosik Choo
 */
using UnityEngine;

public enum ePlayerLocation
{
    Base, Dungeon0, Dungeon1, Dungeon2, Dungeon3, Boss1, Boss2, Boss3, Boss4, Number
}

//TODO: XML로 플레이어의 최근 맵 정보 저장/로드 필요
public class MapManager : MonoBehaviour
{
    //TODO: 밑에 dungeonMapHolder를 이용한 코드로 바꿔야 함. 아래는 dungeonMapHolder의 자식들임
    [SerializeField] public GameObject     bspMapGenerator;
    [SerializeField] public GameObject     monsterGenerator;
    [SerializeField] public GameObject     player;
    [SerializeField] public RoomDungeonGen roomDungeonGen;
    [SerializeField] public MonsterPcg     monsterPcg;

    [SerializeField] public GameObject dungeonMapHolder;
    //TODO: 여기까지__________

    [SerializeField] public GameObject baseMapHolder;
    [SerializeField] public GameObject bossMapHolder;

    public ePlayerLocation playerLocation
    {
        get;
        set;
    }
    
    public int CurrentDungeonFloorCount
    {
        get;
        set;
    }

    public bool IsCurrentFloorCleared
    {
        get;
        set;
    }
    
    public void Init()
    {
        roomDungeonGen           = bspMapGenerator.GetComponent<RoomDungeonGen>();
        monsterPcg               = monsterGenerator.GetComponent<MonsterPcg>();
        player                   = GameManager.SPlayer;
        CurrentDungeonFloorCount = 0;
        IsCurrentFloorCleared    = false;
    }

    public void GenerateMapAndPlaceCharacter(ePlayerLocation location)
    {
        playerLocation = location;
        monsterPcg.DestroyExistingEnemy();
        
        switch (location)
        {
            case ePlayerLocation.Base:
                GenerateBase();
                break;
            case ePlayerLocation.Dungeon0:
            case ePlayerLocation.Dungeon1:
            case ePlayerLocation.Dungeon2:
            case ePlayerLocation.Dungeon3:
                GenerateDungeon(location);
                break;
            case ePlayerLocation.Boss1:
            case ePlayerLocation.Boss2:
            case ePlayerLocation.Boss3:
            case ePlayerLocation.Boss4:
                GenerateBossField(location);
                break;
            default:
                Debug.Log("Unspecified Location");
                break;
        }
    }

    private void GenerateBase()
    {
        RenderSettings.skybox                                           = GameManager.Instance.skyboxArray[0];
        GameManager.SPlayer.GetComponent<Player>().sspotLight.range     = 22.0f;
        GameManager.SPlayer.GetComponent<Player>().sspotLight.intensity = 30.0f;
        dungeonMapHolder.SetActive(false);
        bossMapHolder.SetActive(false);
        //TODO: 맵 구조에 맞춰서 스폰 위치 설정 필요
        baseMapHolder.SetActive(true);
        GameManager.SPlayer.transform.position = Vector3.zero;
        PlayerManager.instance.playerStart = true;
        PlayerManager.instance.start = true;
        PlayerManager.instance.skillInit = true;
        Debug.Log("Current floor count called from base : " + CurrentDungeonFloorCount);
    }

    private void GenerateDungeon(ePlayerLocation dungeonNumber)
    {
        GameManager.SPlayer.GetComponent<Player>().sspotLight.range     = 40.0f;
        GameManager.SPlayer.GetComponent<Player>().sspotLight.intensity = 30.0f;
        DungeonQuestManager.Instance.GenerateQuest();
        
        baseMapHolder.SetActive(false);
        bossMapHolder.SetActive(false);
        dungeonMapHolder.SetActive(true);
        
        CurrentDungeonFloorCount++;
        Debug.Log("Current Floor : " + CurrentDungeonFloorCount);
        IsCurrentFloorCleared = false;
        roomDungeonGen.GenerateDungeon(dungeonNumber);
        
        player.transform.position = new Vector3(roomDungeonGen.playerStartPos.x, roomDungeonGen.playerStartPos.y, -10);
        
        monsterPcg = monsterGenerator.GetComponent<MonsterPcg>();
        monsterPcg.ProceduralGenMonsters();
        
        DungeonQuestManager.Instance.OpenQuestUI();
    }

    private void GenerateBossField(ePlayerLocation location)
    {
        GameManager.SPlayer.GetComponent<Player>().sspotLight.range     = 40.0f;
        GameManager.SPlayer.GetComponent<Player>().sspotLight.intensity = 30.0f;
        dungeonMapHolder.SetActive(false);
        baseMapHolder.SetActive(false);

        switch (location)
        {
            case ePlayerLocation.Boss1:
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss1").gameObject.SetActive(true);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss2").gameObject.SetActive(false);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss3").gameObject.SetActive(false);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss4").gameObject.SetActive(false);
                break;
            case ePlayerLocation.Boss2:
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss1").gameObject.SetActive(false);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss2").gameObject.SetActive(true);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss3").gameObject.SetActive(false);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss4").gameObject.SetActive(false);
                break;
            case ePlayerLocation.Boss3:
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss1").gameObject.SetActive(false);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss2").gameObject.SetActive(false);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss3").gameObject.SetActive(true);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss4").gameObject.SetActive(false);
                break;
            case ePlayerLocation.Boss4:
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss1").gameObject.SetActive(false);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss2").gameObject.SetActive(false);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss3").gameObject.SetActive(false);
                bossMapHolder.GetComponentInChildren<Grid>().transform.Find("Boss4").gameObject.SetActive(true);
                break;
            default:
                Debug.Log("Wrong Dungeon , Called from MapManager.GenerateBossfield");
                break;
        }

        GameManager.SPlayer.transform.position = Vector3.zero;
        
        bossMapHolder.SetActive(true);
    }
    
}
