using UnityEngine;

[CreateAssetMenu(fileName = "New Action Sheet", menuName = "ActionSheet")]
public class ActionSheet : ScriptableObject
{
    public string[] actionPriority;
    public string[] actionsByLevel;
    public string[] actionNames;


    [Header("Attack Attributes")]
    public int attackDamage;
    public int attackCooldown;


    [Header("Skill 1 Attributes")]
    public int firstSkillDamage;
    public int firstSkillCooldown;


    [Header("Skill 2 Attributes")]
    public int secondSkillDamage;
    public int secondSkillCooldown;
}
