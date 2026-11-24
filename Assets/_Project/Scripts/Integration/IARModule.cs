using System;

/// <summary>
/// Kontrak antara aplikasi utama dan modul AR.
/// Disepakati bersama tim AR — setiap perubahan pada berkas ini
/// harus diberi nomor versi dan dikomunikasikan ke kedua belah pihak.
///
/// Versi kontrak: 1.0
/// </summary>
public interface IARModule
{
    /// <summary>Menyiapkan sesi AR untuk cerita tertentu.</summary>
    void Initialize(string storyId);

    /// <summary>Memulai pemindaian marker.</summary>
    void StartSession();

    /// <summary>Menghentikan sesi dan melepas kamera.</summary>
    void StopSession();

    /// <summary>
    /// Dipicu ketika pemain menyelesaikan objektif AR.
    /// Parameter: id objektif, jumlah poin yang diperoleh.
    /// </summary>
    event Action<string, int> OnObjectiveCompleted;

    /// <summary>
    /// Dipicu ketika terjadi kegagalan — kamera ditolak, marker rusak,
    /// atau kesalahan lain yang membuat sesi tidak dapat dilanjutkan.
    /// </summary>
    event Action<string> OnSessionError;
}