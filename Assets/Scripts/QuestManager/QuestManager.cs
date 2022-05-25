using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questsContent;
    [SerializeField] private GameObject questWindow;
    [SerializeField] private Player player;

    private Quest currentQuest;

    private void Start()
    {
        currentQuest = player.getCurrentQuest();
        currentQuest.initialize();
        currentQuest.completionEvent.AddListener(onQuestCompleted);

        GameObject questObj = Instantiate(questPrefab, questsContent);
        questObj.transform.Find("Icon").GetComponent<Image>().sprite = currentQuest.information.icon;

        questObj.GetComponent<Button>().onClick.AddListener(delegate
        {
            if(player.isControllable())
            {
                questWindow.SetActive(!questWindow.activeSelf);
                if (questWindow.activeSelf)
                {
                    questWindow.GetComponent<QuestWindow>().initialize(currentQuest);
                }
            }
        });
    }

    public void kill(string mobName)
    {
        EventManager.Instance.QueueEvent(new KillEvent(mobName));
    }

    private void onQuestCompleted(Quest quest)
    {
        // currentQuest.Find("Checkmark").gameObject.SetActive(true);
    }
}
