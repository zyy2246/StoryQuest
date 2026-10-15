using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainMenuController : MonoBehaviour
{
[Header("Tombol")]
[SerializeField] private Button btnStart;
[SerializeField] private Button btnContinue;
[SerializeField] private Button btnAbout;
[SerializeField] private Button btnCloseAbout;
[Header("Teks")]
[SerializeField] private TMP_Text txtPoints;
[Header("Panel")]
[SerializeField] private GameObject aboutPanel;
private void OnEnable()
{
// Berlangganan event saat objek aktif.
btnStart.onClick.AddListener(HandleStart);
btnContinue.onClick.AddListener(HandleContinue);
btnAbout.onClick.AddListener(HandleAbout);
btnCloseAbout.onClick.AddListener(CloseAbout);
}
private void OnDisable()
{
// WAJIB: berhenti berlangganan agar tidak terjadi
// pemanggilan ganda saat objek diaktifkan kembali.
btnStart.onClick.RemoveListener(HandleStart);
btnContinue.onClick.RemoveListener(HandleContinue);
btnAbout.onClick.RemoveListener(HandleAbout);
btnCloseAbout.onClick.RemoveListener(CloseAbout);
}
private void Start()
{
// Pengaman bila aplikasi dijalankan langsung dari scene ini
// saat pengembangan, sehingga GameManager belum pernah dibuat.
if (GameManager.Instance == null)
{
Debug.LogWarning("[Menu] GameManager tidak ditemukan. " +
"Jalankan aplikasi dari scene Splash.");

txtPoints.text = "Poin: -";
btnContinue.interactable = false;
aboutPanel.SetActive(false);
return;
}
RefreshPoints();
// Tombol Lanjutkan hanya aktif bila ada sesi sebelumnya.
bool hasSession = !string.IsNullOrEmpty(GameManager.Instance.SelectedStoryId);
btnContinue.interactable = hasSession;
aboutPanel.SetActive(false);
}
private void RefreshPoints()
{


txtPoints.text = $"Poin: {GameManager.Instance.TotalPoints}";
}


private void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        if (aboutPanel.activeSelf)
        {
            CloseAbout();
        }
    }
}

private void HandleStart()
{
Debug.Log("[Menu] Tombol Mulai ditekan.");
NavigationManager.Instance.GoTo(SceneNames.STORY_LIST);
}
private void HandleContinue()
{
Debug.Log($"[Menu] Melanjutkan cerita {GameManager.Instance.SelectedStoryId}.");
NavigationManager.Instance.GoTo(SceneNames.STORY_LIST);
}
private void HandleAbout()
{
aboutPanel.SetActive(true);
Debug.Log("[Menu] Panel Tentang dibuka.");
}
private void CloseAbout()
{
aboutPanel.SetActive(false);
Debug.Log("[Menu] Panel Tentang ditutup.");
}
}