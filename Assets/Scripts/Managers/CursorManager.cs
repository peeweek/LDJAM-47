using GameplayIngredients;
using NaughtyAttributes;
using System;
using UnityEngine;


public enum CursorType
{
    Default,
    Arrow,
    Hand,
    Dialogue,
    Forbidden
}

[ManagerDefaultPrefab("CursorManager")]
public class CursorManager : Manager
{
    [Serializable]
    public struct CursorDef
    {
        public Texture2D Texture;
        public Vector2 HotSpot;
    }

    public CursorDef Arrow;
    public CursorDef Hand;
    public CursorDef Dialogue;
    public CursorDef Forbidden;

    public CursorType defaultCursor;

    public CursorType cursorType { get; private set; }

    public void SetCursor(CursorType type)
    {
        cursorType = type;
    }

    private void LateUpdate()
    {
        switch (cursorType)
        {
            default:
            case CursorType.Default:
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                break;
            case CursorType.Arrow:
                Cursor.SetCursor(Arrow.Texture, Arrow.HotSpot, CursorMode.Auto);
                break;
            case CursorType.Hand:
                Cursor.SetCursor(Hand.Texture, Hand.HotSpot, CursorMode.Auto);
                break;
            case CursorType.Dialogue:
                Cursor.SetCursor(Dialogue.Texture, Dialogue.HotSpot, CursorMode.Auto);
                break;
            case CursorType.Forbidden:
                Cursor.SetCursor(Forbidden.Texture, Forbidden.HotSpot, CursorMode.Auto);
                break;
        }

        cursorType = defaultCursor;
    }
}
