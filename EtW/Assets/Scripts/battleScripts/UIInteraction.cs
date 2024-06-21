using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInteraction : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI notiText;

    [SerializeField]
    private Skill skillManager;

    public Button attack1;
    public Button attack2;
    public Button attack3;
    public Button defense1;
    public Button defense2;
    public Button defense3;
    public Button util1;
    public Button util2;
    public Button util3;
    public Button ATKPlusBtn;
    public Button DEFPlusBtn;
    public Button AGIPlusBtn;
    public Sprite[] skillImages = new Sprite[9];
    public Sprite defaultImage;
    public Image[] skillUI = new Image[3];
    public Image[] skillUIAlpha = new Image[3];
    public Player player;
    public TextMeshProUGUI skillDescription;
    
    public void Play() {
        Time.timeScale = 1f;
    }

    public void ToTitle() {
        SceneManager.LoadScene("introScene");
    }

    public void ATKPlus() {
        if (PlayerManager.instance.LVP >= 1) {
            PlayerManager.instance.playerStatATK++;
            PlayerManager.instance.appliedATK += 0.05f;
            PlayerManager.instance.LVP--;
        } else {
            notiText.SetText("Not enough LVP");
        }
    }

    public void DEFPlus() {
        if (PlayerManager.instance.LVP >= 1) {
            PlayerManager.instance.playerStatDEF++;
            PlayerManager.instance.appliedDEF += 0.02f;
            PlayerManager.instance.LVP--;
            player.playerDEF += 0.05f;
        } else {
            notiText.SetText("Not enough LVP");
        }
    }

    public void AGIPlus() {
        if (PlayerManager.instance.LVP >= 1) {
            PlayerManager.instance.playerStatAGI++;
            PlayerManager.instance.appliedAGI += 0.1f;
            PlayerManager.instance.LVP--;
            player.playerAGI += 0.1f;
        } else {
            notiText.SetText("Not enough LVP");
        }
    }

    public void HPPlus() {
        if (PlayerManager.instance.LVP >= 1) {
            PlayerManager.instance.playerStatHP++;
            PlayerManager.instance.appliedHP += 2f;
            PlayerManager.instance.LVP--;
        } else {
            notiText.SetText("Not enough LVP");
        }
    }

    public void MPPlus() {
        if (PlayerManager.instance.LVP >= 1) {
            PlayerManager.instance.playerStatMP++;
            PlayerManager.instance.appliedMP += 10f;
            PlayerManager.instance.LVP--;
        } else {
            notiText.SetText("Not enough LVP");
        }
    }

    public void Attack1() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[0] = "attack1";
            PlayerManager.instance.SP--;
            skillUIAlpha[0].gameObject.SetActive(true);
            skillUI[0].sprite = skillImages[0];
            skillUI[0].color = new Color(255, 255, 255, 1);
            skillUIAlpha[0].sprite = skillImages[0];
            skillUIAlpha[0].color = new Color(255, 255, 255, 1);
            skillManager.firstSkillActivated = true;
            attack1.interactable = false;
            defense1.interactable = false;
            util1.interactable = false;
            attack2.interactable = true;
            defense2.interactable = true;
            util2.interactable = true;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Attack2() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[1] = "attack2";
            PlayerManager.instance.SP--;
            skillUIAlpha[1].gameObject.SetActive(true);
            skillUI[1].sprite = skillImages[1];
            skillUI[1].color = new Color(255, 255, 255, 1);
            skillUIAlpha[1].sprite = skillImages[1];
            skillUIAlpha[1].color = new Color(255, 255, 255, 1);
            skillManager.secondSkillActivated = true;
            attack2.interactable = false;
            defense2.interactable = false;
            util2.interactable = false;
            attack3.interactable = true;
            defense3.interactable = true;
            util3.interactable = true;
            
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Attack3() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[2] = "attack3";
            PlayerManager.instance.SP--;
            skillUIAlpha[2].gameObject.SetActive(true);
            skillUI[2].sprite = skillImages[2];
            skillUI[2].color = new Color(255, 255, 255, 1);
            skillUIAlpha[2].sprite = skillImages[2];
            skillUIAlpha[2].color = new Color(255, 255, 255, 1);
            skillManager.thirdSkillActivated = true;
            attack3.interactable = false;
            defense3.interactable = false;
            util3.interactable = false;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Defense1() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[0] = "defense1";
            PlayerManager.instance.SP--;
            skillUIAlpha[0].gameObject.SetActive(true);
            skillUI[0].sprite = skillImages[3];
            skillUI[0].color = new Color(255, 255, 255, 1);
            skillUIAlpha[0].sprite = skillImages[3];
            skillUIAlpha[0].color = new Color(255, 255, 255, 1);
            skillManager.firstSkillActivated = true;
            attack1.interactable = false;
            defense1.interactable = false;
            util1.interactable = false;
            attack2.interactable = true;
            defense2.interactable = true;
            util2.interactable = true;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Defense2() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[1] = "defense2";
            PlayerManager.instance.SP--;
            skillUIAlpha[1].gameObject.SetActive(true);
            skillUI[1].sprite = skillImages[4];
            skillUI[1].color = new Color(255, 255, 255, 1);
            skillUIAlpha[1].sprite = skillImages[4];
            skillUIAlpha[1].color = new Color(255, 255, 255, 1);
            skillManager.secondSkillActivated = true;
            attack2.interactable = false;
            defense2.interactable = false;
            util2.interactable = false;
            attack3.interactable = true;
            defense3.interactable = true;
            util3.interactable = true;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Defense3() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[2] = "defense3";
            PlayerManager.instance.SP--;
            skillUIAlpha[2].gameObject.SetActive(true);
            skillUI[2].sprite = skillImages[5];
            skillUI[2].color = new Color(255, 255, 255, 1);
            skillUIAlpha[2].sprite = skillImages[5];
            skillUIAlpha[2].color = new Color(255, 255, 255, 1);
            skillManager.thirdSkillActivated = true;
            attack3.interactable = false;
            defense3.interactable = false;
            util3.interactable = false;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Util1() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[0] = "util1";
            PlayerManager.instance.SP--;
            skillUIAlpha[0].gameObject.SetActive(true);
            skillUI[0].sprite = skillImages[6];
            skillUI[0].color = new Color(255, 255, 255, 1);
            skillUIAlpha[0].sprite = skillImages[6];
            skillUIAlpha[0].color = new Color(255, 255, 255, 1);
            skillManager.firstSkillActivated = true;
            attack1.interactable = false;
            defense1.interactable = false;
            util1.interactable = false;
            attack2.interactable = true;
            defense2.interactable = true;
            util2.interactable = true;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Util2() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[1] = "util2";
            PlayerManager.instance.SP--;
            skillUIAlpha[1].gameObject.SetActive(true);
            skillUI[1].sprite = skillImages[7];
            skillUI[1].color = new Color(255, 255, 255, 1);
            skillUIAlpha[1].sprite = skillImages[7];
            skillUIAlpha[1].color = new Color(255, 255, 255, 1);
            skillManager.secondSkillActivated = true;
            attack2.interactable = false;
            defense2.interactable = false;
            util2.interactable = false;
            attack3.interactable = true;
            defense3.interactable = true;
            util3.interactable = true;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Util3() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[2] = "util3";
            PlayerManager.instance.SP--;
            skillUIAlpha[2].gameObject.SetActive(true);
            skillUI[2].sprite = skillImages[8];
            skillUI[2].color = new Color(255, 255, 255, 1);
            skillUIAlpha[2].sprite = skillImages[8];
            skillUIAlpha[2].color = new Color(255, 255, 255, 1);
            skillManager.thirdSkillActivated = true;
            attack3.interactable = false;
            defense3.interactable = false;
            util3.interactable = false;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    private void Update() {
        if (IsPointerOverUIElement(out string uiElementName))
        {
            if (uiElementName == "Askill1") {
                skillDescription.SetText("사용시 스타일리쉬 포인트를 2만큼 증가시킵니다.\n(MP소모 : 30)");
            } else if (uiElementName == "Askill2") {
                skillDescription.SetText("획득시 집중 성공시 얻는 스타일리쉬 포인트의 수치가 증가합니다.");
            } else if (uiElementName == "Askill3") {
                skillDescription.SetText("초집중 상태가 종료될때 맵에있는 모든 늑대와 사슴을 공격합니다.\n(MP소모 : 250)");
            } else if (uiElementName == "Dskill1") {
                skillDescription.SetText("사용시 시전자의 방어력을 증가시킵니다.\n(MP소모 : 30)");
            } else if (uiElementName == "Dskill2") {
                skillDescription.SetText("획득시 방어력이 영구적으로 올라갑니다.");
            } else if (uiElementName == "Dskill3") {
                skillDescription.SetText("사용시 일정시간 무적이 됩니다.\n(MP소모 : 200)");
            } else if (uiElementName == "Uskill1") {
                skillDescription.SetText("사용시 일정시간 이동속도가 증가합니다.\n(MP소모 : 30)");
            } else if (uiElementName == "Uskill2") {
                skillDescription.SetText("사용시 시전자의 체력을 회복합니다.\n(MP소모 : 60)");
            } else if (uiElementName == "Uskill3") {
                skillDescription.SetText("사용시 모든 적의 이동속도를 대폭 감속시킵니다.\n(MP소모 : 150)");
            } else {
                skillDescription.SetText("");
            }
        }

        if (PlayerManager.instance.skillInit & PlayerManager.instance.notDead) {
            attack1.interactable = true;
            attack2.interactable = false;
            attack3.interactable = false;
            defense1.interactable = true;
            defense2.interactable = false;
            defense3.interactable = false;
            util1.interactable = true;
            util2.interactable = false;
            util3.interactable = false;
            skillUI[0].sprite = null;
            skillUI[1].sprite = null;
            skillUI[2].sprite = null;
            skillUIAlpha[0].sprite = null;
            skillUIAlpha[1].sprite = null;
            skillUIAlpha[2].sprite = null;
            foreach (Image skillUI in skillUI) {
                skillUI.color = new Color(255, 255, 255, 0);
            }
            foreach (Image skillUI in skillUIAlpha) {
                skillUI.color = new Color(255, 255, 255, 0);
            }
            PlayerManager.instance.skillInit = false;
        }

        if (PlayerManager.instance.appliedATK >= PlayerManager.instance.ATKupperLimit) {
            ATKPlusBtn.interactable = false;
        } else {
            ATKPlusBtn.interactable = true;
        }

        if (PlayerManager.instance.appliedDEF >= PlayerManager.instance.DEFupperLimit) {
            DEFPlusBtn.interactable = false;
        } else {
            DEFPlusBtn.interactable = true;
        }

        if (PlayerManager.instance.appliedAGI >= PlayerManager.instance.AGIupperLimit) {
            AGIPlusBtn.interactable = false;
        } else {
            AGIPlusBtn.interactable = true;
        }
    }


    private bool IsPointerOverUIElement(out string uiElementName)
    {
        uiElementName = null;

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject != null)
            {
                uiElementName = result.gameObject.name;
                return true;
            }
        }

        return false;
    }
}