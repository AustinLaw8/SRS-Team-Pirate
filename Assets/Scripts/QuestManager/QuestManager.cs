using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private static GameObject questCanvasInstance;

    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questsContent;
    [SerializeField] private GameObject questWindow;
    
    //[SerializeField] private Player player;
    private GameObject questObj;
    private Quest currentQuest;

    private void Awake()
    {

        if (questCanvasInstance != null && questCanvasInstance != this)
        {
            Destroy(this.transform.parent.gameObject);
        } else {
            questCanvasInstance = this.transform.parent.gameObject;
        }

        questObj = Instantiate(questPrefab, questsContent);

        questObj.GetComponent<Button>().onClick.AddListener(delegate
        {
            if(Player.MyPlayer.isControllable())
            {
                questWindow.SetActive(!questWindow.activeSelf);
                if (questWindow.activeSelf)
                {
                    questWindow.GetComponent<QuestWindow>().initialize(currentQuest);
                }
                else
                {
                    questWindow.GetComponent<QuestWindow>().closeWindow();
                }
            }
        });
    }

    public void initQuest() {
        currentQuest = Player.MyPlayer.getCurrentQuest();
        currentQuest.initialize();
        // currentQuest.completionEvent.AddListener(onQuestCompleted);
        questObj.transform.Find("Icon").GetComponent<Image>().sprite = currentQuest.information.icon;
        if (Player.MyPlayer.stage == -1) Player.MyPlayer.playDialogue("Intro_Cutscene", "Captain");
    }

    public void kill(string mobName)
    {
        EventManager.Instance.QueueEvent(new KillEvent(mobName));
    }


    public void talk(string npcName)
    {
        EventManager.Instance.QueueEvent(new TalkToNpcEvent(npcName));
    }

    private void onQuestCompleted(Quest quest)
    {
        // currentQuest.Find("Checkmark").gameObject.SetActive(true);
    }
}
