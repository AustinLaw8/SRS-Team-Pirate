using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    //fields for all the elements
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private Transform taskContent;
    // [SerializeField] private Text xp;
    // [SerializeField] private Text gold;

    public void initialize(Quest quest)
    {
        titleText.text = quest.information.name;
        descriptionText.text = quest.information.description;
        //KillingTask.onUpdateKillCount += updateCount;
        foreach (var task in quest.tasks)
        {
            GameObject taskObj = Instantiate(taskPrefab, taskContent);
            taskObj.transform.Find("Text").GetComponent<Text>().text = task.getDescription();

            GameObject countObj = taskObj.transform.Find("Count").gameObject;

            if (task.completed) {
                countObj.SetActive(false);
                taskObj.transform.Find("Done").gameObject.SetActive(true);
            }
            else {
                countObj.GetComponent<Text>().text = task.curAmount + "/" + task.reqAmount;
            }
        }
        // xp.text = quest.reward.xp.ToString();
        // gold.text = quest.reward.gold.ToString();
    }

/*
    public void updateCount() {

        foreach (var task in quest.tasks) {
            if (!task.completed) {
               countObj.GetComponent<Text>().text = task.curAmount + "/" + task.reqAmount; 
            }
        }
    }
*/

    public void closeWindow()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < taskContent.childCount; i++)
        {
            Destroy(taskContent.GetChild(i).gameObject);
        }
       //KillingTask.onUpdateKillCount -= upDateCount; 
    }
}