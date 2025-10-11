using UnityEngine;

public class Strike : ISkill // Simply deals damage
{
    public PlayerBattleSystem PBS { get; set; }
    public float ManaCost { get; set; } = 10f;
    public float Damage = 50;
    public bool IsReady { get; set; } = true;
    public float CooldownTime { get; set; } = 1.3f;
    public float TimeOfRecovery { get; set; }
    public void Activate()
    {
        if (IsReady && PBS.TargetIsAlive) 
        {
            PBS.ManaSystem.SubtractMana(ManaCost);
            IsReady = false;
            TimeOfRecovery = PBS.currentTime + CooldownTime;
            if (PBS != null)
            {
                PBS.Attack(Damage);
                PBS.StunOnProc(PBS.BaseStunChance, PBS.BaseStunDuration);
            }
        }
    }

}
