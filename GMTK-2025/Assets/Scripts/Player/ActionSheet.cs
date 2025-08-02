using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action Sheet", menuName = "ActionSheet")]
public class ActionSheet : ScriptableObject
{
    public string[] actionPriority;

    public string[] actionsByLevel;



    [Header("Attack Attributes")]
    public int attackDamage;
    public int attackCooldown;


    [Header("Archer Skill 1 Attributes")]
    public int piercingShotDamage;
    public int piercingShotCooldown;

    [Header("Archer Skill 2 Attributes")]
    public int rainOfArrowsDamage;
    public int rainOfArrowsCooldown;


    [Header("Warrior Skill 1 Attributes")]
    public int slashDamage;
    public int slashCooldown;

    [Header("Warrior Skill 2 Attributes")]
    public int roarDamage;
    public int roarCooldown;


    [Header("Mage Skill 1 Attributes")]
    public int fireNovaDamage;
    public int fireNovaCooldown;

    [Header("Mage Skill 2 Attributes")]
    public int meteorDamage;
    public int meteorCooldown;


    [Header("Priest Skill 1 Attributes")]
    public int massHealAmount;
    public int massHealCooldown;  

    [Header("Priest Skill 2 Attributes")]
    public int haloDamageHeal;
    public int haloCooldown;


    [Header("Peasant Skill 1 Attributes")]
    public int pierceDamage;
    public int pierceCooldown;

    [Header("Peasant Skill 2 Attributes")]
    public int cleaveDamage;
    public int cleaveCooldown;


}
