using GameplayIngredients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public static Player instance;
    public static Vector3 position => instance.transform.position;
    public static float radius => instance.InteractionRadius;

    [Header("Interaction")]
    public float InteractionRadius = 2.0f;
    public float Speed = 1.0f;

    [Header("Dialogue")]
    public GameObject DialogueRoot;
    public RectTransform DialogueBubble;
    public Text Text;

    Animator m_Animator;
    VirtualCameraManager m_VCM;
    CharacterController m_Char;

    public void OnEnable()
    {
        if (instance != null)
            Debug.LogWarning("Already found a Player in scene");
        instance = this;
    }

    public void OnDisable()
    {
        if (instance == this)
            instance = null;
        else
            Debug.LogWarning("Already found a Player in scene");
    }

    public void Start()
    {
        m_VCM = Manager.Get<VirtualCameraManager>();
        m_Char = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();

    }

    public void LateUpdate()
    {
        if (Manager.Get<DialogueManager>().isPlayingDialogue)
            return;

        Vector2 move = Vector2.zero;
        move.y += Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
        move.y -= Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
        move.x += Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        move.x -= Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
        move.y += Input.GetKey(KeyCode.W) ? 1 : 0;
        move.y -= Input.GetKey(KeyCode.S) ? 1 : 0;
        move.x += Input.GetKey(KeyCode.D) ? 1 : 0;
        move.x -= Input.GetKey(KeyCode.A) ? 1 : 0;
        if (move.sqrMagnitude > 0)
            move.Normalize();

        Vector3 fwd = m_VCM.transform.forward;
        fwd.Scale(new Vector3(1, 0, 1));
        fwd.Normalize();

        Vector3 rgt = m_VCM.transform.right;
        rgt.Scale(new Vector3(1, 0, 1));
        rgt.Normalize();

        m_Char.Move(Speed * (fwd * move.y + rgt * move.x) * Time.deltaTime);

        m_Animator.SetBool("Moving", move.sqrMagnitude > 0);
    }

    float size = 0;
    public void SetBubbleSize(float size)
    {
        DialogueBubble.anchorMin = new Vector2(1 - size, 0);
        DialogueBubble.anchorMax = new Vector2(1, size);
    }

    public bool StepOpenDialogue()
    {
        if (DialogueRoot == null)
        {
            Debug.LogError("No Dialogue root in player");
            return true;
        }

        if(!DialogueRoot.activeSelf)
        {
            // Init
            DialogueRoot.SetActive(true);
            size = 0;
            SetBubbleSize(0);
            return false;
        }
        else
        {
            size = Mathf.Clamp01(size + Time.deltaTime * 3);
            SetBubbleSize(size);
            return size == 1;
        }
    }

    public bool StepCloseDialogue()
    {
        if (DialogueRoot == null)
        {
            Debug.LogError("No Dialogue root in player");
            return true;
        }

        if(size > 0)
        {
            size = Mathf.Clamp01(size - Time.deltaTime * 8);
            SetBubbleSize(size);
            return false;
        }
        else
        {
            SetBubbleSize(0);
            DialogueRoot.SetActive(false);
            return true;
        }
    }

    public void SetDialogueText(string text)
    {
        Text.text = text;
        Text.Rebuild(CanvasUpdate.Layout);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.5f, 0.1f, .3f);
        Gizmos.DrawWireSphere(transform.position, InteractionRadius);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 1, 0.1f);
        Gizmos.DrawSphere(transform.position, InteractionRadius);

        Gizmos.color = new Color(1, 0.5f, 0.1f, 1);
        Gizmos.DrawWireSphere(transform.position, InteractionRadius);
    }


}
