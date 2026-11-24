using UnityEngine;
public class SimpleSpinner : MonoBehaviour
{
[SerializeField] private float degreesPerSecond = 240f;
private void Update()
{
// unscaledDeltaTime agar tetap berputar walau Time.timeScale = 0.
transform.Rotate(0f, 0f, -degreesPerSecond * Time.unscaledDeltaTime);
}
}