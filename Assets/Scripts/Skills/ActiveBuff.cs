using UnityEngine;

public class ActiveBuff
{
    public string Id {  get; private set; }
    public float Duration { get; private set; }
    public System.Action OnStart { get; private set; }
    public System.Action OnEnd { get; private set; }
    public System.Action OnUpdate { get; private set; }
    public bool IsExpired => Duration <= 0;
    public ActiveBuff(string id, float duration, System.Action onStart = null, System.Action onEnd = null, System.Action onUpdate = null)
    {
        Id = id;
        Duration = duration;
        OnStart = onStart;
        OnEnd = onEnd;
        OnUpdate = onUpdate;
    }
    public void ManualUpdate(float deltaTime)
    {
        OnUpdate?.Invoke();
        Duration -= deltaTime;
    }
}
