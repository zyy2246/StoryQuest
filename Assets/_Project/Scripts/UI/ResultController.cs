using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultController : MonoBehaviour, IBackHandler
{
    [Header("Ringkasan")]
    [SerializeField] private TMP_Text txtStoryTitle;
    [SerializeField] private TMP_Text txtObjectives;
    [SerializeField] private TMP_Text txtPoints;

    [Header("Aksi")]
    [SerializeField] private Button btnSubmit;
    [SerializeField] private Button btnMenu;

    [Header("Status")]
    [SerializeField] private GameObject statusPanel;
    [SerializeField] private TMP_Text txtStatus;
    [SerializeField] private GameObject spinner;

    private bool _isSubmitting;
    private bool _hasSubmitted;

    private void OnEnable()
    {
        btnSubmit.onClick.AddListener(HandleSubmit);
        btnMenu.onClick.AddListener(HandleBackToMenu);
    }

    private void OnDisable()
    {
        btnSubmit.onClick.RemoveListener(HandleSubmit);
        btnMenu.onClick.RemoveListener(HandleBackToMenu);
    }

    private void Start()
    {
        SessionData session = GameManager.Instance.CurrentSession;

        if (session == null)
        {
            Debug.LogWarning("[Result] Tidak ada sesi aktif.");
            NavigationManager.Instance.Replace(SceneNames.MAIN_MENU);
            return;
        }

        txtStoryTitle.text = session.storyTitle;
        txtObjectives.text = $"Objektif selesai: {session.objectivesCompleted}";
        txtPoints.text = $"{session.pointsEarned} poin";

        statusPanel.SetActive(false);
        spinner.SetActive(false);
    }

    private void HandleSubmit()
    {
        // Pengaman ganda-kirim di sisi aplikasi
        if (_isSubmitting || _hasSubmitted)
        {
            Debug.Log("[Result] Permintaan kirim diabaikan - sudah berjalan atau selesai.");
            return;
        }

        SessionData session = GameManager.Instance.CurrentSession;

        ScoreSubmission payload = new ScoreSubmission
        {
            // Guid menjamin keunikan tanpa perlu berkoordinasi dengan server
            sessionId = Guid.NewGuid().ToString(),
            playerName = GameManager.Instance.PlayerName,
            storyId = session.storyId,
            points = session.pointsEarned,
            objectivesCompleted = session.objectivesCompleted,
            durationSeconds = session.DurationSeconds
        };

        SetSubmitting(true);

        ApiClient.Instance.SubmitScore(
            payload,
            onSuccess: response =>
            {
                SetSubmitting(false);
                _hasSubmitted = true;
                btnSubmit.interactable = false;

                txtStatus.text = response.rank > 0
                    ? $"Tersimpan. Total {response.totalPoints} poin, peringkat {response.rank}."
                    : "Skor berhasil dikirim.";

                statusPanel.SetActive(true);
                GameManager.Instance.EndSession();
            },
            onError: message =>
            {
                SetSubmitting(false);
                txtStatus.text = $"Gagal mengirim: {message}";
                statusPanel.SetActive(true);
            });
    }

    private void SetSubmitting(bool value)
    {
        _isSubmitting = value;

        // Tombol tetap aktif selama request berlangsung
        // agar klik berikutnya bisa ditolak oleh pengaman.
        btnSubmit.interactable = !_hasSubmitted;

        spinner.SetActive(value);

        // Menu tetap dikunci selama pengiriman.
        btnMenu.interactable = !value;
    }

    private void HandleBackToMenu()
    {
        NavigationManager.Instance.Replace(SceneNames.MAIN_MENU);
    }

    public bool OnBackPressed()
    {
        if (_isSubmitting) return true; // Blokir keluar saat mengirim
        HandleBackToMenu();
        return true;
    }
}
