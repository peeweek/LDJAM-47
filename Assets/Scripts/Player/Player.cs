using GameplayIngredients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float Speed = 1.0f;

    VirtualCameraManager m_VCM;
    CharacterController m_Char;

    public void Start()
    {
        m_VCM = Manager.Get<VirtualCameraManager>();
        m_Char = GetComponent<CharacterController>();
    }

    public void LateUpdate()
    {
        Vector2 move = Vector2.zero;
        move.y += Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
        move.y -= Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
        move.x += Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        move.x -= Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
        if (move.sqrMagnitude > 0)
            move.Normalize();

        Vector3 fwd = m_VCM.transform.forward;
        fwd.Scale(new Vector3(1, 0, 1));
        fwd.Normalize();

        Vector3 rgt = m_VCM.transform.right;
        rgt.Scale(new Vector3(1, 0, 1));
        rgt.Normalize();

        m_Char.Move(Speed * (fwd * move.y + rgt * move.x) * Time.deltaTime);
    }
}
