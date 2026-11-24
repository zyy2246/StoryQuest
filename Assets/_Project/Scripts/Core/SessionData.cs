using System;
/// <summary>
/// Menyimpan seluruh informasi satu sesi bermain: cerita apa
/// yang dipilih, berapa objektif selesai, berapa poin diperoleh,
/// dan kapan sesi dimulai.
/// </summary>
[Serializable]
public class SessionData
{
public string storyId;
public string storyTitle;
public int pointsEarned;
public int objectivesCompleted;
public DateTime startedAt;


public SessionData(string id, string title)
{
storyId = id;
storyTitle = title;
pointsEarned = 0;
objectivesCompleted = 0;
startedAt = DateTime.Now;
}
public int DurationSeconds => (int)(DateTime.Now - startedAt).TotalSeconds;
}