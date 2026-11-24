
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Memuat scene AR secara aditif, menemukan implementasi IARModule
/// di dalamnya, dan menghubungkan event-nya ke aplikasi utama.
/// </summary>
public class ARBridge : MonoBehaviour
{
    [Header("Konfigurasi")]
    [SerializeField] private string arSceneName = SceneNames.AR_STAGE;
    [SerializeField] private GameObject mainUIRoot;
    [SerializeField] private Camera mainCamera;

    private IARModule _module;
    private bool _isLoaded;

    public bool IsLoaded => _isLoaded;

    public void OpenAR(string storyId)
    {
        if (_isLoaded)
        {
            Debug.LogWarning("[ARBridge] Sesi AR sudah aktif, permintaan diabaikan.");
            return;
        }

        StartCoroutine(OpenRoutine(storyId));
    }

    public void CloseAR()
    {
        if (!_isLoaded) return;

        StartCoroutine(CloseRoutine());
    }

    private IEnumerator OpenRoutine(string storyId)
    {
        // 1. Memuat scene AR secara aditif.
        AsyncOperation op = SceneManager.LoadSceneAsync(
            arSceneName,
            LoadSceneMode.Additive
        );

        yield return op;

        Scene arScene = SceneManager.GetSceneByName(arSceneName);

        if (!arScene.IsValid())
        {
            Debug.LogError(
                "[ARBridge] Scene AR tidak valid. " +
                "Pastikan scene terdaftar di Build Settings."
            );
            yield break;
        }

        // 2. Mencari komponen yang memenuhi kontrak IARModule.
        foreach (GameObject root in arScene.GetRootGameObjects())
        {
            _module = root.GetComponentInChildren<IARModule>(true);

            if (_module != null)
                break;
        }

        if (_module == null)
        {
            Debug.LogError(
                "[ARBridge] Tidak ditemukan implementasi IARModule. " +
                "Periksa kembali kontrak dengan tim AR."
            );

            yield return SceneManager.UnloadSceneAsync(arSceneName);
            yield break;
        }

        // 3. Berlangganan event modul AR.
        _module.OnObjectiveCompleted += HandleObjectiveCompleted;
        _module.OnSessionError += HandleSessionError;

        // 4. Menyembunyikan UI dan kamera aplikasi.
        if (mainUIRoot != null)
            mainUIRoot.SetActive(false);

        if (mainCamera != null)
            mainCamera.enabled = false;

        // 5. Menjalankan sesi AR.
        _module.Initialize(storyId);
        _module.StartSession();

        _isLoaded = true;

        Debug.Log("[ARBridge] Modul AR aktif secara aditif.");
    }

    private IEnumerator CloseRoutine()
    {
        // 1. Menghentikan sesi dan melepas event.
        if (_module != null)
        {
            _module.StopSession();

            _module.OnObjectiveCompleted -= HandleObjectiveCompleted;
            _module.OnSessionError -= HandleSessionError;

            _module = null;
        }

        // 2. Membongkar scene AR.
        yield return SceneManager.UnloadSceneAsync(arSceneName);

        // 3. Membersihkan aset yang tidak lagi digunakan.
        yield return Resources.UnloadUnusedAssets();

        // 4. Mengaktifkan kembali UI dan kamera utama.
        if (mainUIRoot != null)
            mainUIRoot.SetActive(true);

        if (mainCamera != null)
            mainCamera.enabled = true;

        _isLoaded = false;

        Debug.Log("[ARBridge] Modul AR dilepas.");
    }

    // Dipanggil ketika objektif AR berhasil diselesaikan.
    private void HandleObjectiveCompleted(string objectiveId, int points)
    {
        Debug.Log(
            $"[ARBridge] Objektif {objectiveId} selesai, +{points} poin."
        );

        GameManager.Instance.RecordObjective(points);

        CloseAR();

        NavigationManager.Instance.Push(SceneNames.RESULT);
    }

    // Dipanggil ketika terjadi kesalahan pada sesi AR.
    private void HandleSessionError(string message)
    {
        Debug.LogWarning($"[ARBridge] Kesalahan sesi AR: {message}");

        CloseAR();
    }
}
