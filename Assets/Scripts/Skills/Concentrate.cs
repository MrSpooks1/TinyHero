using UnityEngine;

public class Concentrate :  ISkill // Buff that increases damage but slows attack speed
{
    public PlayerBattleSystem PBS { get; set; }
    public float ManaCost { get; set; } = 30f;
    public bool IsReady { get; set; } = true;
    public float CooldownTime { get; set; } = 20f;
    public float TimeOfRecovery { get; set; }
    public float BuffDuration { get; set; } = 10f;
    private static float DAMAGEMULTIPLIER = 2; // 2 = 100% bonus damage
    private static float AUTOATTACKCOOLDOWNMULTIPLIER = 1.4f; // 1.4 times more cooldown means 40% slow
    public void Activate()
    {
        if (IsReady)
        {
            PBS.ManaSystem.SubtractMana(ManaCost);
            BuffManager.Instance.ApplyBuff(
                target: PBS,
                buffId: "Concentrate",
                duration: 10f,
                onStart: () =>
                {
                    IsReady = false;
                    TimeOfRecovery = PBS.currentTime + CooldownTime;
                    if (PBS != null)
                    {
                        PBS.BaseStats.AutoAttackDamage *= DAMAGEMULTIPLIER;
                        PBS.BaseStats.AutoAttackCooldown *= AUTOATTACKCOOLDOWNMULTIPLIER;
                    }
                },
                onEnd: () =>
                {
                    PBS.BaseStats.AutoAttackDamage /= DAMAGEMULTIPLIER;
                    PBS.BaseStats.AutoAttackCooldown /= AUTOATTACKCOOLDOWNMULTIPLIER;
                }
            );
        }
    }
}
