using UnityEngine;
/// <summary>
/// Pemicu navigasi sementara untuk pengujian sebelum UI dibangun.
/// HAPUS script ini setelah Modul 4 selesai.
/// </summary>
public class TemporaryNavProbe : MonoBehaviour
{
private void Update()
{
if (Input.GetKeyDown(KeyCode.Alpha1))
NavigationManager.Instance.GoTo(SceneNames.MAIN_MENU);
if (Input.GetKeyDown(KeyCode.Alpha2))
NavigationManager.Instance.GoTo(SceneNames.STORY_LIST);
if (Input.GetKeyDown(KeyCode.P))
GameManager.Instance.AddPoints(10);
if (Input.GetKeyDown(KeyCode.R))
GameManager.Instance.ResetSession();
if (Input.GetKeyDown(KeyCode.Alpha3))
    NavigationManager.Instance.GoTo(SceneNames.STORY_LIST,
    progress => Debug.Log($"[Progress] {progress:P0}"));
}
}