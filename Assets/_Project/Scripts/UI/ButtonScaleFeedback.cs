using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Mengecilkan tombol sedikit saat ditekan, lalu mengembalikannya.
/// Memakai interface event system, bukan onClick, sehingga
/// bereaksi pada saat jari menekan — bukan saat dilepas.
/// </summary>
public class ButtonScaleFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
[SerializeField] private float pressedScale = 0.95f;
[SerializeField] private float speed = 12f;
private Vector3 _baseScale;
private Vector3 _targetScale;
private void Awake()
{
_baseScale = transform.localScale;
_targetScale = _baseScale;
}
private void Update()
{
// unscaledDeltaTime agar animasi tetap jalan
// walau Time.timeScale disetel 0 (misalnya saat jeda).
transform.localScale = Vector3.Lerp(
transform.localScale, _targetScale, Time.unscaledDeltaTime * speed);
}
public void OnPointerDown(PointerEventData eventData) => _targetScale = _baseScale *pressedScale;
public void OnPointerUp(PointerEventData eventData) => _targetScale = _baseScale;
}