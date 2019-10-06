using UnityEngine;

public class UseCustomCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorIdle;

    [SerializeField]
    private Texture2D cursorClick;

    private readonly Vector2 hotSpot = new Vector2(4, 5);

    private void Awake()
    {
        Cursor.SetCursor(cursorIdle, hotSpot, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorClick, hotSpot, CursorMode.Auto);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorIdle, hotSpot, CursorMode.Auto);
        }
    }
}
