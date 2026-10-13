# Laporan Modul 01

### 1.1 Personalisasi Build Label

Build label diubah menjadi `StoryQuest v0.1 — FrezzyAlvaWijaksana`. Setelah di build ulang, label baru sudah muncul di HP.

### 1.2 Informasi RAM dan GPU

Informasi perangkat ditambahkan ke aplikasi menggunakan `SystemInfo`.

- RAM: 5718 MB
- GPU: Mali-G52

### 1.3 Target Frame Rate

Target FPS diatur ke 30 menggunakan `Application.targetFrameRate = 30;`. Rotasi kubus tetap menggunakan `Time.deltaTime` supaya kecepatannya tidak bergantung langsung pada jumlah frame per detik.

Pada 30 FPS, gerakan terlihat sedikit kurang halus, tetapi rotasi masih berjalan.

### 1.4 Ukuran Repository

Hasil `git count-objects -vH`:

- Jumlah objek: 55
- Ukuran objek lepas: 25.57 KiB
- Ukuran pack: 0 bytes

Ukuran folder proyek Unity di Windows Properties adalah 1,61 GB.

Ukuran keduanya berbeda karena folder proyek mencakup file Unity dan aset lainnya, sedangkan perintah Git hanya menampilkan ukuran objek Git lepas. Jadi, angka 25.57 KiB bukan ukuran keseluruhan repository.