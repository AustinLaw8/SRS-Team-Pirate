using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
   [SerializeField] private GameObject questPrefab;
   [SerializeField] private Transform questsContent;
   [SerializeField] private GameObject questHolder;

   public List<Quest> currentQuests;

   private void Awake()
   {
      foreach (var quest in currentQuests)
      {
         quest.initialize();
         quest.completionEvent.AddListener(onQuestCompleted);
         
         GameObject questObj = Instantiate(questPrefab, questsContent);
         questObj.transform.Find("Icon").GetComponent<Image>().sprite = quest.information.icon;
         
         questObj.GetComponent<Button>().onClick.AddListener(delegate
         {
            questHolder.GetComponent<QuestWindow>().initialize(quest);
            questHolder.SetActive(true);
         });
      }
   }

   public void kill(string mobName){
      EventManager.Instance.QueueEvent(new KillEvent(mobName));
   }

   private void onQuestCompleted(Quest quest)
   {
      questsContent.GetChild(currentQuests.IndexOf(quest)).Find("Checkmark").gameObject.SetActive(true);
   }
}
