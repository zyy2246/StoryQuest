using System.Collections;
using UnityEngine;
public class SplashController : MonoBehaviour
{
[SerializeField] private float displayDuration = 2f;
// Start() boleh bertipe IEnumerator — Unity akan menjalankannya
// sebagai coroutine, sehingga yield dapat dipakai langsung.
private IEnumerator Start()
{
Debug.Log("[Splash] Menampilkan logo...");
yield return new WaitForSeconds(displayDuration);
NavigationManager.Instance.GoTo(SceneNames.MAIN_MENU);
}
}