using UnityEngine;
/// <summary>
/// Menyesuaikan RectTransform agar berada di dalam safe area perangkat,
/// menghindari poni, punch-hole, dan gesture bar.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
private RectTransform _rect;
private Rect _lastSafeArea;
private ScreenOrientation _lastOrientation;
private void Awake()
{
_rect = GetComponent<RectTransform>();
Apply();
}
private void Update()
{
// Rotasi layar dan tampilnya bilah sistem mengubah safe area,
// jadi nilainya perlu dipantau, bukan dibaca sekali saja.
if (Screen.safeArea != _lastSafeArea || Screen.orientation != _lastOrientation)
Apply();
}
private void Apply()
{
_lastSafeArea = Screen.safeArea;
_lastOrientation = Screen.orientation;
Vector2 min = _lastSafeArea.position;
Vector2 max = _lastSafeArea.position + _lastSafeArea.size;
// Anchor bernilai 0..1, sedangkan safeArea dalam piksel,
// sehingga perlu dibagi ukuran layar.
min.x /= Screen.width;
min.y /= Screen.height;
max.x /= Screen.width;
max.y /= Screen.height;
_rect.anchorMin = min;
_rect.anchorMax = max;
_rect.offsetMin = Vector2.zero;
_rect.offsetMax = Vector2.zero;

Debug.Log($"[SafeArea] Diterapkan: {_lastSafeArea} pada layar " +
          $"{Screen.width}x{Screen.height}");
}
}