using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    GameObject target;
    private Vector3 defaultScale;
    private void Start()
    {
        defaultScale = transform.localScale;
    }
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("SelectedTarget");
        if (target != null)
        {
            this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1.5f);
            this.transform.localScale = defaultScale;
        }
        else this.transform.localScale = Vector3.zero;
    }
}
