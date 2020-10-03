using GameplayIngredients;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Interactive : MonoBehaviour
{
    public string Name = "Interactive";

    public TextMesh text;

    [ReorderableList]
    public GameObject[] HighlightableObjects;

    [ReorderableList]
    public Callable[] OnInteract;

    SphereCollider m_SphereCollider => GetComponent<SphereCollider>();


    bool hover;
    bool clickable;

    private void OnEnable()
    {
        m_SphereCollider.isTrigger = true;
        text.text = Name;
    }

    private void OnMouseOver()
    {
        hover = !Manager.Get<DialogueManager>().isPlayingDialogue;
    }

    private void OnMouseExit()
    {
        hover = false;
    }

    private void OnMouseDown()
    {
        if (clickable && !Manager.Get<DialogueManager>().isPlayingDialogue)
            Callable.Call(OnInteract);
    }

    public void Update()
    {
        var s = m_SphereCollider;
        var p = Player.instance;
        var d = Vector3.Distance(transform.position, p.transform.position);
        var t = s.radius + p.InteractionRadius;

        int layer = LayerMask.NameToLayer("Default");
        clickable = false;

        if (d < t && hover)
        {
            layer = LayerMask.NameToLayer("Highlight");
            clickable = true;
        }

        ShowText(clickable);

        if (HighlightableObjects != null)
        {
            foreach (var obj in HighlightableObjects)
                obj.layer = layer;  
        }

        var cm = Manager.Get<CursorManager>();

        if (clickable)
            cm.SetCursor(CursorType.Hand);

    }

    void ShowText(bool show)
    {
        if (text == null)
            return;

        var color = text.color;
        float dt = Time.deltaTime;

        color.a = Mathf.Clamp(color.a + (show ? dt * 4 : dt * -2), 0, 0.5f);

        text.color = color;
        text.gameObject.SetActive(color.a > 0);
    }

    private void OnDrawGizmos()
    {
        var s = m_SphereCollider;
        Gizmos.color = new Color(1, 0.5f, 0.1f, .3f);
        Gizmos.DrawWireSphere(transform.position + s.center, s.radius);
    }
    private void OnDrawGizmosSelected()
    {
        var s = m_SphereCollider;
        Gizmos.color = new Color(1, 1, 1, 0.1f);
        Gizmos.DrawSphere(transform.position + s.center, s.radius);

        Gizmos.color = new Color(1, 0.5f, 0.1f, 1);
        Gizmos.DrawWireSphere(transform.position + s.center, s.radius);
    }

}
