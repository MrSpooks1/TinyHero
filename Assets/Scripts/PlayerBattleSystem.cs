using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBattleSystem : MonoBehaviour
{
    public BattleSystem BaseStats;
    public HealthSystem HealthSystem;
    public ManaSystem ManaSystem;
    public EnemyBattleSystem EBS { get; set; }
    private float autoAttackRecoveryTime;
    public float currentTime { get; private set; }
    public ISkill Skill1;
    public ISkill Skill2;
    public float BaseStunChance { get; set; } = 0.3f;
    public float BaseStunDuration { get; set; } = 2f;
    public float BaseManaRegenValue { get; set; } = 5f;
    public float BaseManaRegenCooldown { get; set; } = 1f;
    private float manaRegenRecoveryTime;
    public bool TargetIsAlive => !EBS.HealthSystem.IsDead;
    void Start()
    {
        currentTime = 0;
        Skill1 = new Concentrate();
        Skill1.PBS = this; 
        Skill2 = new Strike();
        Skill2.PBS = this;
        autoAttackRecoveryTime = BaseStats.AutoAttackCooldown;
        manaRegenRecoveryTime = BaseManaRegenCooldown;
}
    void Update()
    {
        if (HealthSystem.IsDead) {  return; }
        currentTime += Time.deltaTime;
        if (currentTime > manaRegenRecoveryTime)
        {
            manaRegenRecoveryTime = currentTime + BaseManaRegenCooldown;
            RegenerateMana(BaseManaRegenValue);
        }
        if (currentTime > Skill1.TimeOfRecovery) { Skill1.IsReady = true; }
        if (currentTime > Skill2.TimeOfRecovery) { Skill2.IsReady = true; }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Skill1.Activate();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Skill2.Activate();
        }
        if (currentTime > autoAttackRecoveryTime)
        {
            autoAttackRecoveryTime = currentTime + BaseStats.AutoAttackCooldown;
            FindTarget();
            Attack(BaseStats.AutoAttackDamage);
            StunOnProc(BaseStunChance, BaseStunDuration);
        }
    }
    public void Attack(float damage) 
    { 
        BaseStats.TargetHealth?.SubtractHealth(damage);
    }
    public void StunOnProc(float procChance, float stunDuration)
    {
        if (EBS != null && Random.value < procChance)
        {
            EBS.StunDuration = stunDuration;
        }
    }
    private void FindTarget()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("SelectedTarget");
        EBS = enemy?.GetComponent<EnemyBattleSystem>();
        BaseStats.TargetHealth = EBS?.HealthSystem;
    }
    private void RegenerateMana(float amount)
    {
        ManaSystem.AddMana(amount);
    }
}
