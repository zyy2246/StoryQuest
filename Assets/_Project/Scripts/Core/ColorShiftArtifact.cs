
using UnityEngine;

public class ColorShiftArtifact : InteractiveObject
{
    [Header("Perubahan Warna")]
    [SerializeField] private Color targetColor = Color.cyan;
    [SerializeField] private float colorShiftSpeed = 1f;

    private Renderer objectRenderer;
    private Color originalColor;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    private void Update()
    {
        if (!isActivated || objectRenderer == null) return;

        float t = Mathf.PingPong(Time.time * colorShiftSpeed, 1f);
        objectRenderer.material.color = Color.Lerp(
            originalColor,
            targetColor,
            t
        );
    }

    public override void OnInteract()
    {
        if (objectRenderer == null)
        {
            Debug.LogWarning("[ColorShift] Renderer tidak ditemukan.");
            return;
        }

        ReportInteraction();
    }

    public override string GetDescription()
    {
        return base.GetDescription()
            + " Warna artefak berubah secara bertahap saat aktif.";
    }
}