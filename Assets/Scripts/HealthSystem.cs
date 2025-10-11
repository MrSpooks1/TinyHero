using UnityEngine;

public class HealthSystem: MonoBehaviour
{
    public float MaxHealth;
    public BarSystem HealthBar;
    public float CurrentHealth { get; private set; }
    public bool IsDead;
    public static float DESTROYDELAY = 0.5f;
    public void Start() 
    {
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxValue(MaxHealth);
        HealthBar.SetValue(CurrentHealth);
        IsDead = false;
    }
    public void SubtractHealth(float value)
    {
        if (!IsDead) 
        {
            CurrentHealth -= value;
            if (CurrentHealth <= 0) { Die(); }
            HealthBar.SetValue(CurrentHealth);
        }
    }
    public void AddHealth(float value)
    {
        if (!IsDead)
        {
            CurrentHealth += value;
            if (CurrentHealth > MaxHealth) { CurrentHealth = MaxHealth; }
            HealthBar.SetValue(CurrentHealth);
        }
    }
    private void Die()
    {
        CurrentHealth = 0;
        IsDead = true;
        if (gameObject.tag != "Player") { Destroy(gameObject, DESTROYDELAY); }
    }
}
