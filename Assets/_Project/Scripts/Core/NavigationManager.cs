using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NavigationManager : MonoBehaviour
{
public static NavigationManager Instance { get; private set; }
[Header("Opsi")]
[Tooltip("Durasi minimum layar pemuatan agar tidak berkedip.")]
[SerializeField] private float minimumLoadingTime = 0.5f;
private bool _isLoading;
private void Awake()
{
if (Instance != null && Instance != this) { Destroy(gameObject); return; }
Instance = this;
DontDestroyOnLoad(gameObject);
}
/// <summary>Perpindahan scene sederhana, tanpa pelaporan progres.</summary>
public void GoTo(string sceneName)
{
if (_isLoading) return; // Mencegah dua pemuatan bersamaan.
StartCoroutine(LoadRoutine(sceneName, null));
}
/// <summary>Perpindahan dengan callback progres bernilai 0 sampai 1.</summary>
public void GoTo(string sceneName, System.Action<float> onProgress)
{
if (_isLoading) return;
StartCoroutine(LoadRoutine(sceneName, onProgress));
}
private IEnumerator LoadRoutine(string sceneName, System.Action<float> onProgress)
{
_isLoading = true;
float startTime = Time.time;
AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
op.allowSceneActivation = false;
// Unity menahan progress di 0.9 sampai scene diizinkan aktif,
// sehingga nilai dinormalisasi agar callback tetap 0..1.
while (op.progress < 0.9f)
{
onProgress?.Invoke(op.progress / 0.9f);
yield return null; // tunggu satu frame
}
onProgress?.Invoke(1f);

// Menjaga agar layar pemuatan tidak berkelebat terlalu singkat.
float elapsed = Time.time - startTime;
if (elapsed < minimumLoadingTime)
yield return new WaitForSeconds(minimumLoadingTime - elapsed);
op.allowSceneActivation = true;
_isLoading = false;
Debug.Log($"[Navigation] Masuk scene {sceneName}");
}
}