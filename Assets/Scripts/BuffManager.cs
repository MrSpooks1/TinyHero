using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    private static BuffManager instance;
    public static BuffManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject managerObject = new GameObject("BuffManager");
                instance = managerObject.AddComponent<BuffManager>();
            }
            return instance;
        }
    }
    private Dictionary<MonoBehaviour, List<ActiveBuff>> activeBuffs = new Dictionary<MonoBehaviour, List<ActiveBuff>>();
    private void Awake()
    {
        // Deleting duplicates
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) 
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
      foreach (List<ActiveBuff> targetBuffs in activeBuffs.Values)
        {
            for (int i = targetBuffs.Count - 1; i >= 0; i--)
            {
                ActiveBuff buff = targetBuffs[i];
                buff.ManualUpdate(Time.deltaTime);
                if (buff.IsExpired)
                {
                    buff.OnEnd?.Invoke();
                    targetBuffs.RemoveAt(i);
                }
            }
        }   
    }
    public void ApplyBuff(MonoBehaviour target, string buffId, float duration, System.Action onStart = null, System.Action onEnd = null, System.Action onUpdate = null)
    {
        if (!activeBuffs.ContainsKey(target))
        {
            activeBuffs[target] = new List<ActiveBuff>();
        }
        ActiveBuff newBuff = new ActiveBuff(buffId, duration, onStart, onEnd, onUpdate);
        activeBuffs[target].Add(newBuff);
        onStart?.Invoke();
    }
}
