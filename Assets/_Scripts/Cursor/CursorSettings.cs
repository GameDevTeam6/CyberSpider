using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSettings : MonoBehaviour
{
    [SerializeField] Texture2D newCursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(newCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
