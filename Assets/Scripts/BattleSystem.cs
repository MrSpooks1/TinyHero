using UnityEngine;

[System.Serializable]
public class BattleSystem
{
    public HealthSystem TargetHealth { get; set; }
    public float AutoAttackDamage;
    public float AutoAttackCooldown;
}
