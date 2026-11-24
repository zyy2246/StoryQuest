using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StoryListController : MonoBehaviour, IBackHandler
{
[Header("Referensi UI")]
[SerializeField] private Transform listContainer;
[SerializeField] private GameObject itemPrefab;
[SerializeField] private GameObject scrollRoot;
[SerializeField] private GameObject loadingPanel;
[SerializeField] private GameObject errorPanel;
[SerializeField] private TMP_Text txtError;
[SerializeField] private Button btnRetry;
[Header("Integrasi")]
[SerializeField] private ARBridge arBridge;
private void OnEnable() => btnRetry.onClick.AddListener(FetchStories);
private void OnDisable() => btnRetry.onClick.RemoveListener(FetchStories);
private void Start() => FetchStories();
private void FetchStories()
{
ShowState(loading: true, error: false, list: false);
ApiClient.Instance.GetStories(
onSuccess: stories =>
{
BuildList(stories);
ShowState(loading: false, error: false, list: true);
},
onError: message =>


{
txtError.text = message;
ShowState(loading: false, error: true, list: false);
});
}
private void ShowState(bool loading, bool error, bool list)
{
loadingPanel.SetActive(loading);
errorPanel.SetActive(error);
scrollRoot.SetActive(list);
}
private void BuildList(StoryDto[] stories)
{
foreach (Transform child in listContainer)
Destroy(child.gameObject);
foreach (StoryDto story in stories)
{
GameObject item = Instantiate(itemPrefab, listContainer);
item.transform.Find("Txt_Title").GetComponent<TMP_Text>().text = story.title;
item.transform.Find("Txt_Synopsis").GetComponent<TMP_Text>().text = story.synopsis;

// Salinan lokal — jebakan closure, lihat Modul 6.
StoryDto captured = story;
item.GetComponent<Button>().onClick.AddListener(() => SelectStory(captured));
}
Debug.Log($"[StoryList] {stories.Length} cerita ditampilkan.");
}
private void SelectStory(StoryDto story)
{
Debug.Log($"[StoryList] Cerita dipilih: {story.title}");
GameManager.Instance.BeginSession(story.id, story.title);
arBridge.OpenAR(story.id);
}
public bool OnBackPressed() => false;
}