using UnityEngine;

public interface ISkill
{
    public PlayerBattleSystem PBS { get; set; }
    public bool IsReady { get; set; }
    public float CooldownTime { get; set; }
    public float TimeOfRecovery { get; set; }
    public float ManaCost { get; set; }
    public void Activate();
}