using UnityEngine;

public class ManaSystem : MonoBehaviour 
{
    public float MaxMana;
    public BarSystem ManaBar;
    public float CurrentMana { get; private set; }
    public void Start()
    {
        CurrentMana = MaxMana;
        ManaBar.SetMaxValue(MaxMana);
        ManaBar.SetValue(CurrentMana);
    }
    public void SubtractMana(float value)
    {
        CurrentMana -= value;
        if (CurrentMana < 0) CurrentMana = 0;
        ManaBar.SetValue(CurrentMana);
    }
    public void AddMana(float value)
    {
        CurrentMana += value;
        if (CurrentMana > MaxMana) CurrentMana = MaxMana;
        ManaBar.SetValue(CurrentMana);
    }
}
