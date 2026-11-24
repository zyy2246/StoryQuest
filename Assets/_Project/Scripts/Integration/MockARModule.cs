using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Pengganti sementara modul AR. Mensimulasikan penyelesaian
/// objektif setelah beberapa detik, sehingga alur aplikasi
/// dapat diuji sebelum modul asli diserahkan.
///
/// JANGAN DIHAPUS setelah modul asli masuk — berkas ini
/// tetap diperlukan untuk uji regresi dan sebagai cadangan demo.
/// </summary>
public class MockARModule : MonoBehaviour, IARModule
{
    [Header("Simulasi")]
    [SerializeField] private float simulatedScanDuration = 3f;
    [SerializeField] private int simulatedPoints = 25;

    [Header("Pengujian Kegagalan")]
    [Tooltip("Centang untuk mensimulasikan marker yang gagal dikenali.")]
    [SerializeField] private bool simulateError;

    public event Action<string, int> OnObjectiveCompleted;
    public event Action<string> OnSessionError;

    private string _storyId;
    private Coroutine _routine;

    public void Initialize(string storyId)
    {
        _storyId = storyId;
        Debug.Log($"[MockAR] Diinisialisasi untuk cerita {storyId}");
    }

    public void StartSession()
    {
        // Cegah dua coroutine berjalan bersamaan jika StartSession dipanggil ulang.
        StopSession();

        Debug.Log("[MockAR] Sesi dimulai, memindai marker...");
        _routine = StartCoroutine(SimulateRoutine());
    }

    public void StopSession()
    {
        if (_routine == null) return;

        StopCoroutine(_routine);
        _routine = null;
        Debug.Log("[MockAR] Sesi dihentikan.");
    }

    private IEnumerator SimulateRoutine()
    {
        yield return new WaitForSeconds(simulatedScanDuration);

        if (simulateError)
        {
            OnSessionError?.Invoke("Marker tidak dikenali.");
        }
        else
        {
            OnObjectiveCompleted?.Invoke($"{_storyId}_obj01", simulatedPoints);
        }

        _routine = null;
    }
}