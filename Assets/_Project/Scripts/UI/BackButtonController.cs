
using UnityEngine;

public class BackButtonController : MonoBehaviour
{
    public void BackToMainMenu()
    {
        if (NavigationManager.Instance == null)
        {
            Debug.LogError("NavigationManager tidak ditemukan!");
            return;
        }

        NavigationManager.Instance.GoTo("MainMenu");
    }
}