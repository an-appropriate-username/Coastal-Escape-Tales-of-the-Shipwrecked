using UnityEngine;
using Unity;

[System.Serializable]
public class Quest
{

    public string title;
    
    [TextArea(3, 10)]
    public string description;
    
    [TextArea(3, 10)]
    public string isReachedDescription;
    public int acornReward;
    public int rewardAmount;

    public Item item;
    public Item itemDelete;

    public bool isActive;
    public bool isComplete;

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        isComplete = true;
    }

}
