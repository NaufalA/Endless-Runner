using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTemplateController : MonoBehaviour
{
    private const float DebugLineHeight = 10.0f;

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + Vector3.up * DebugLineHeight / 2, transform.position + Vector3.down * DebugLineHeight / 2, Color.green);
    }
}
