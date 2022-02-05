using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct info{
        public string name;
        public string description;
        public Sprite icon;
    }

    public info information;

    [System.Serializable]
    public struct stat{
        public int xp;
        public int gold;
    }

    public stat reward = new stat {gold = 0, xp = 0};
    public bool completed {get; protected set;}
    public QuestCompletionEvent completionEvent;

    public List<Task> tasks;

    public void initialize(){
        completed = false;
        completionEvent = new QuestCompletionEvent();

        foreach (var task in tasks){
            task.initialize();
            task.completionEvent.AddListener(delegate { checkTasks(); });
        }
    }

    public void checkTasks(){
        completed = tasks.All( task => task.completed );

        if(completed){
            giveReward();
            completionEvent.Invoke(this);
            completionEvent.RemoveAllListeners();
        }
    }
    
    public void giveReward(){
        // put xp and gold into player's inventory
    }
}

public class QuestCompletionEvent : UnityEvent<Quest> {}


#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    SerializedProperty questInfo;
    SerializedProperty questReward;

    List<string> questTaskTypes;
    SerializedProperty questTaskList;


    //Path of Quest Folder
    [MenuItem("Assets/Quest", priority = 0)]
    static public void CreateQuest()
    {
        var newQuest = CreateInstance<Quest>();
        
        ProjectWindowUtil.CreateAsset(newQuest, "quest.asset");
    }
    
    void OnEnable()
    {
        //Initialize Property of Quest Info Struct
        questInfo = serializedObject.FindProperty(nameof(Quest.information));
        //Initialize Property of Quest Reward Stat Struct
        questReward = serializedObject.FindProperty(nameof(Quest.reward));
        //Initilize Property of List of Quest Tasks
        questTaskList = serializedObject.FindProperty(nameof(Quest.tasks));
        
        //Initilize list of Quest Tasks Types (ex. KillingTask .... etc.)
        var lookup = typeof(Task);
        questTaskTypes = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
            .Select(type => type.Name)
            .ToList();
    }

    public override void OnInspectorGUI()
    {
        var child = questInfo.Copy();
        var depth = child.depth;
        child.NextVisible(true);
        
        EditorGUILayout.LabelField("Quest info", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }
        
        child = questReward.Copy();
        depth = child.depth;
        child.NextVisible(true);
        
        EditorGUILayout.LabelField("Quest reward", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }
        
        int choice = EditorGUILayout.Popup("Add new Quest Goal", -1, questTaskTypes.ToArray());

        if (choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(questTaskTypes[choice]);
            
            AssetDatabase.AddObjectToAsset(newInstance, target);
            
            questTaskList.InsertArrayElementAtIndex(questTaskList.arraySize);
            questTaskList.GetArrayElementAtIndex(questTaskList.arraySize - 1).objectReferenceValue = newInstance;
        }


        Editor ed = null;
        int toDelete = -1;
        for (int i = 0; i < questTaskList.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = questTaskList.GetArrayElementAtIndex(i);           
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);
            
            ed.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (toDelete != -1)
        {
            var item = questTaskList.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);
            
            //need to do it twice, first time just nullify the entry, second actually remove it.
            questTaskList.DeleteArrayElementAtIndex(toDelete);
            questTaskList.DeleteArrayElementAtIndex(toDelete);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif