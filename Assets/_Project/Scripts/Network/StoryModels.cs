using System;

/// <summary>Satu entri cerita Folklore Indonesia sebagaimana dikirim server.</summary>
[Serializable]
public class StoryDto
{
    public string id;
    public string title;
    public string synopsis;
    public string origin;
    public string markerCode;
    public int maxPoints;
}

/// <summary>Pembungkus respons daftar cerita dari GET /api/stories.</summary>
[Serializable]
public class StoryListResponse
{
    public string status;
    public StoryDto[] data;
    public string message;
}

/// <summary>Pembungkus untuk JSON yang akarnya berupa array murni.</summary>
[Serializable]
public class ArrayWrapper<T>
{
    public T[] items;

    public static T[] Parse(string rawJson)
    {
        string wrapped = "{\"items\":" + rawJson + "}";
        return UnityEngine.JsonUtility.FromJson<ArrayWrapper<T>>(wrapped).items;
    }
}

/// <summary>Muatan (payload) yang dikirim ke server saat pemain menyelesaikan sesi.</summary>
[Serializable]
public class ScoreSubmission
{
    public string sessionId;
    public string playerName;
    public string storyId;
    public int points;
    public int objectivesCompleted;
    public int durationSeconds;
}

/// <summary>Balasan server atas pengiriman skor pada POST /api/scores.</summary>
[Serializable]
public class ScoreResponse
{
    public string status;
    public string message;
    public int totalPoints;
    public int rank;
}
