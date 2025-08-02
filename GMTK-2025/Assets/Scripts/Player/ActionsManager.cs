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

            if (actionToUse == "PiercingShot")
            {
                damage = actionSheet.piercingShotDamage;
                cooldown = actionSheet.piercingShotCooldown;
            }
            else if (actionToUse == "RainOfArrows")
            {
                damage = actionSheet.rainOfArrowsDamage;
                cooldown = actionSheet.rainOfArrowsCooldown;
            }
            else if (actionToUse == "Attack")
            {
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

            Debug.Log($"{actionToUse} performed with {damage} damage!");
        }
    }

    string ChooseAction()
    {
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
