using UnityEngine;

[CreateAssetMenu(fileName = "ApiConfig", menuName = "StoryQuest/API Config")]
public class ApiConfig : ScriptableObject
{
    [Header("Server API Kelompok 10")]
    public string baseUrl = "http://kelompok10.202.10.47.91.sslip.io/api";

    [Header("Endpoint")]
    public string storiesPath = "/stories";
    public string submitScorePath = "/scores";

    [Header("Perilaku")]
    [Tooltip("Batas waktu dalam detik. 0 berarti tanpa batas - jangan dipakai.")]
    public int timeoutSeconds = 10;

    public string StoriesUrl => baseUrl.TrimEnd('/') + storiesPath;
    public string SubmitScoreUrl => baseUrl.TrimEnd('/') + submitScorePath;
}
