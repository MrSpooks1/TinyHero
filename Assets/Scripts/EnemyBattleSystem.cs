using UnityEngine;

public class EnemyBattleSystem : MonoBehaviour
{
    public BattleSystem BaseStats;
    public PlayerBattleSystem PBS { get; set; }
    public HealthSystem HealthSystem;
    public bool IsStunned => StunDuration > 0;
    public float StunDuration;
    private float autoAttackRecoveryTime;
    public float currentTime { get; private set; }
    public float BaseHealChance { get; set; } = 0.25f;
    public float BaseHealPercent { get; set; } = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = 0;
        autoAttackRecoveryTime = BaseStats.AutoAttackCooldown;
    }
    // Update is called once per frame
    void Update()
    {
        if (HealthSystem.IsDead) 
        {
            return; 
        }
        if (!IsStunned) { currentTime += Time.deltaTime; }
        else { StunDuration -= Time.deltaTime; }
        if (currentTime > autoAttackRecoveryTime)
        {
            autoAttackRecoveryTime = currentTime + BaseStats.AutoAttackCooldown;
            FindTarget();
            if (PBS.HealthSystem.IsDead) { return; } // if player is dead
            Attack(BaseStats.AutoAttackDamage);
            HealOnProc(BaseHealChance, BaseHealPercent * HealthSystem.MaxHealth);
        }
    }
    public void Attack(float damage) { BaseStats.TargetHealth?.SubtractHealth(damage); }
    private void FindTarget()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        PBS = playerObj?.GetComponent<PlayerBattleSystem>();
        BaseStats.TargetHealth = PBS?.HealthSystem;
    }
    public void HealOnProc(float procChance, float healAmount)
    {
        if (Random.value < procChance) { HealthSystem.AddHealth(healAmount); }
    }
}
