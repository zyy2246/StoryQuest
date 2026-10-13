
using UnityEngine;

/// <summary>
/// Menembakkan ray dari titik sentuh ke dalam scene dan meneruskan
/// interaksi ke InteractiveObject yang terkena.
/// Mendukung sentuhan di perangkat dan klik mouse di Editor.
/// </summary>
public class TouchDispatcher : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private bool showDebugRay = true;

    private void Awake()
    {
        // Menggunakan kamera utama jika slot Inspector kosong.
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    private void Update()
    {
        Vector2 screenPos;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Bereaksi hanya saat jari mulai menyentuh layar.
            if (touch.phase != TouchPhase.Began) return;

            screenPos = touch.position;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // Untuk pengujian menggunakan mouse di Editor.
            screenPos = Input.mousePosition;
        }
        else
        {
            return;
        }

        TryInteract(screenPos);
    }

    private void TryInteract(Vector2 screenPos)
    {
        if (targetCamera == null)
        {
            Debug.LogError("[Touch] Kamera tidak ditemukan!");
            return;
        }

        Ray ray = targetCamera.ScreenPointToRay(screenPos);

        if (showDebugRay)
        {
            Debug.DrawRay(
                ray.origin,
                ray.direction * maxDistance,
                Color.yellow,
                1f
            );
        }

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.TryGetComponent(
                out InteractiveObject interactive))
            {
                interactive.OnInteract();
            }
            else
            {
                Debug.Log(
                    $"[Touch] Mengenai {hit.collider.name}, bukan objek interaktif."
                );
            }
        }
        else
        {
            Debug.Log("[Touch] Tidak mengenai apa pun.");
        }
    }
}