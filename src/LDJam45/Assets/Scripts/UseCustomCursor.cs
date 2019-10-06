using UnityEngine;

public class UseCustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;

    private void Awake()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
