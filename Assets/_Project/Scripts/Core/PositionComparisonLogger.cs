
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PositionComparisonLogger : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(
            $"[Posisi Update] Frame {Time.frameCount}: " +
            $"{transform.position}"
        );
    }

    private void LateUpdate()
    {
        Debug.Log(
            $"[Posisi LateUpdate] Frame {Time.frameCount}: " +
            $"{transform.position}"
        );
    }
}