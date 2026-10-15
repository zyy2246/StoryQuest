using UnityEngine;
/// <summary>
/// Objek tunggal yang bertahan lintas scene dan menyimpan state
/// aplikasi: poin, cerita terpilih, dan nama pemain.
/// </summary>
public class GameManager : MonoBehaviour
{
// Properti statis: dapat diakses dari mana saja tanpa
// perlu mencari objeknya lebih dulu.
public static GameManager Instance { get; private set; }
[Header("State Sesi")]
[SerializeField] private int totalPoints;
[SerializeField] private string selectedStoryId = string.Empty;
[SerializeField] private string playerName = "Pemain";
public int TotalPoints => totalPoints;
public string SelectedStoryId => selectedStoryId;
public string PlayerName => playerName;
private void Awake()
{
// Pola singleton: hanya satu instance yang boleh hidup.
// Tanpa pengaman ini, kembali ke Splash akan membuat
// instance kedua dan poin pemain ter-reset diam-diam.
if (Instance != null && Instance != this)
{
Debug.LogWarning("[GameManager] Duplikat terdeteksi, objek ini dihancurkan.");
Destroy(gameObject);
return;
}
Instance = this;
DontDestroyOnLoad(gameObject);
Debug.Log("[GameManager] Instance dibuat dan dipertahankan lintas scene.");
}
public void SelectStory(string storyId)
{
selectedStoryId = storyId;
Debug.Log($"[GameManager] Cerita terpilih: {storyId}");
}
public void AddPoints(int amount)
{
if (amount <= 0) return;
totalPoints += amount;
Debug.Log($"[GameManager] +{amount} poin. Total: {totalPoints}");
}
public void ResetSession()
{
totalPoints = 0;
selectedStoryId = string.Empty;
Debug.Log("[GameManager] Sesi direset.");
}
}