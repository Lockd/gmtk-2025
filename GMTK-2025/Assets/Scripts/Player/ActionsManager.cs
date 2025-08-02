using UnityEngine;
using System.Collections.Generic;

public class ActionsManager : MonoBehaviour
{
    public ActionSheet actionSheet;
    public int currentLevel = 1;
    private Dictionary<string, float> cooldownTimers = new Dictionary<string, float>();
    private float lastActionTime = -Mathf.Infinity;
    public float globalActionDelay = 0.5f;

    private void Start()
    {
        currentLevel = GetComponent<UnitInstance>().currentLevel;
    }

    public void PerformAction(GameObject target)
    {
        if (Time.time < lastActionTime + globalActionDelay) return;

        string actionToUse = ChooseAction();
        if (!string.IsNullOrEmpty(actionToUse))
        {
            int damage = 0;
            float cooldown = 1f;
            int actionIndex = 0;
            if (actionToUse == "Skill1")
            {
                actionIndex = 1;
                damage = actionSheet.firstSkillDamage;
                cooldown = actionSheet.firstSkillCooldown;
            }
            else if (actionToUse == "Skill2")
            {
                actionIndex = 2;
                damage = actionSheet.secondSkillDamage;
                cooldown = actionSheet.secondSkillCooldown;
            }
            else if (actionToUse == "Attack")
            {
                actionIndex = 0;
                damage = actionSheet.attackDamage;
                cooldown = actionSheet.attackCooldown;
            }

            var enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                //enemy.TakeDamage(damage);
            }

            cooldownTimers[actionToUse] = Time.time + cooldown;
            lastActionTime = Time.time;

            Debug.Log($"{actionSheet.actionNames[actionIndex]} performed with {damage} damage!");
        }
    }

    string ChooseAction()
    {
        if (actionSheet == null) return null;
        foreach (string actionName in actionSheet.actionPriority)
        {
            if (!IsActionAvailableAtLevel(actionName)) continue;
            if (!IsActionOffCooldown(actionName)) continue;
            return actionName;
        }
        return null;
    }

    bool IsActionAvailableAtLevel(string actionName)
    {
        int index = System.Array.IndexOf(actionSheet.actionsByLevel, actionName);
        return index != -1 && (index + 1) <= currentLevel;
    }

    bool IsActionOffCooldown(string actionName)
    {
        if (!cooldownTimers.ContainsKey(actionName)) return true;
        return Time.time >= cooldownTimers[actionName];
    }
}
