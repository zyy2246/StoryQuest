/// <summary>
/// Diimplementasikan oleh layar yang ingin mencegat tombol kembali.
/// Kembalikan true bila tombol sudah ditangani dan tidak perlu
/// diteruskan ke NavigationManager.
/// </summary>
public interface IBackHandler
{
bool OnBackPressed();
}