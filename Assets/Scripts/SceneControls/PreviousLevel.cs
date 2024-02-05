using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousLevel : MonoBehaviour
{
    public static string Previous { get; private set; }
    private void OnDestroy()
    {
        Previous = gameObject.scene.name;
    }
}
