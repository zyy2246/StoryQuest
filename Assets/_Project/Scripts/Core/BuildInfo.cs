using UnityEngine;
/// <summary>
/// Menampilkan informasi build di layar dan di log perangkat.
/// Berguna untuk memastikan APK yang terpasang benar-benar versi terbaru.
/// </summary>
public class BuildInfo : MonoBehaviour
{
[SerializeField] private string buildLabel = "StoryQuest v0.1-FrezzyAlvaWijaksana";
[SerializeField] private float rotationSpeed = 45f;
[SerializeField] private Transform targetCube;
private string _info;

private void Start()
{
      Application.targetFrameRate = 30;
    _info = $"{buildLabel}\n" +
            $"Device : {SystemInfo.deviceModel}\n" +
            $"OS : {SystemInfo.operatingSystem}\n" +
            $"Layar : {Screen.width}x{Screen.height}\n" +
            $"RAM : {SystemInfo.systemMemorySize} MB\n" +
            $"GPU : {SystemInfo.graphicsDeviceName}";

    Debug.Log($"[BuildInfo] {_info.Replace("\n", " | ")}");
}

private void Update()
{
if (targetCube == null) return;
// Time.deltaTime membuat kecepatan rotasi sama di semua perangkat,
// berapa pun frame rate-nya. Tanpa ini, ponsel 120 fps akan
// memutar kubus dua kali lebih cepat daripada ponsel 60 fps.
targetCube.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
}
private void OnGUI()

{
GUI.skin.label.fontSize = 28;
GUI.Label(new Rect(20, 20, Screen.width - 40, 220), _info);
}
}