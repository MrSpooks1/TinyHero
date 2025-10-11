using UnityEngine;

public class SelectableTarget : MonoBehaviour
{
    // Update is called once per frame
    private Camera mainCamera;
    private static int LEFTMOUSEBUTTON = 0;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(LEFTMOUSEBUTTON))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == this.gameObject && this.gameObject.tag != "SelectedTarget")
            {
                GameObject previousTarget = GameObject.FindGameObjectWithTag("SelectedTarget");
                if (previousTarget != null)
                {
                    previousTarget.tag = "Enemy";
                }
                this.gameObject.tag = "SelectedTarget";
            }
        }   
    }
}
