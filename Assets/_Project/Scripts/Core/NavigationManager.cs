using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance { get; private set; }

    [SerializeField] private float minimumLoadingTime = 0.5f;

    // Stack: yang terakhir masuk adalah yang pertama keluar —
    // persis seperti perilaku tombol kembali yang diharapkan.
    private readonly Stack<string> _history = new Stack<string>();
    private bool _isLoading;

    public string CurrentScene => SceneManager.GetActiveScene().name;
    public bool CanGoBack => _history.Count > 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Tombol kembali fisik Android terbaca sebagai Escape.
        if (Input.GetKeyDown(KeyCode.Escape))
            HandleBackButton();
    }

    /// <summary>Maju ke scene baru dan mencatat scene sekarang ke riwayat.</summary>
    public void Push(string sceneName)
    {
        if (_isLoading) return;

        _history.Push(CurrentScene);
        StartCoroutine(LoadRoutine(sceneName));
    }

    /// <summary>Nama lama dari Push. Sementara dipertahankan agar kode lama tetap jalan.</summary>
    [Obsolete("Gunakan Push.")]
    public void GoTo(string sceneName) => Push(sceneName);

    /// <summary>Berpindah tanpa mencatat riwayat; riwayat lama dibersihkan.</summary>
    public void Replace(string sceneName)
    {
        if (_isLoading) return;

        _history.Clear();
        StartCoroutine(LoadRoutine(sceneName));
    }

    /// <summary>Kembali ke scene sebelumnya. Mengembalikan false bila riwayat kosong.</summary>
    public bool Back()
    {
        if (_isLoading || _history.Count == 0) return false;

        StartCoroutine(LoadRoutine(_history.Pop()));
        return true;
    }

    private void HandleBackButton()
    {
        // Layar aktif diberi kesempatan lebih dulu untuk mencegat.
        foreach (IBackHandler handler in GetBackHandlers())
        {
            if (handler.OnBackPressed()) return;
        }

        if (!Back())
        {
            Debug.Log("[Navigation] Riwayat kosong, keluar aplikasi.");
            Application.Quit();
        }
    }

    private IEnumerable<IBackHandler> GetBackHandlers()
    {
        // FindObjectsByType lebih hemat daripada FindObjectsOfType lama.
        MonoBehaviour[] all = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        foreach (MonoBehaviour mb in all)
        {
            if (mb is IBackHandler handler) yield return handler;
        }
    }

    private IEnumerator LoadRoutine(string sceneName)
    {
        _isLoading = true;
        float start = Time.time;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f) yield return null;

        float elapsed = Time.time - start;
        if (elapsed < minimumLoadingTime)
            yield return new WaitForSeconds(minimumLoadingTime - elapsed);

        op.allowSceneActivation = true;
        _isLoading = false;

        Debug.Log($"[Navigation] {sceneName} aktif. Riwayat: {_history.Count} entri.");
    }
}