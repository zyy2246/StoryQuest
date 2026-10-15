# Laporan Modul 03

## Tugas 3.1 - Scene Keempat

Pada tugas ini, saya menambahkan scene Result dan mendaftarkannya pada Build Settings. Scene Result menjadi tujuan setelah StoryList. Perpindahan scene dilakukan menggunakan pemicu keyboard agar alur navigasi dapat diuji

## Tugas 3.2 - Melaporkan Progres

Pada scene kecil, Unity selesai memuat data scene dalam hitungan milidetik, jadi op.progress sudah mencapai 0,9 hanya dalam satu atau dua frame. Loop while hanya sempat berjalan sekali atau dua kali, sehingga callback cuma menerima nilai awal (sekitar 0) lalu langsung 1f setelah loop selesai. Nilai antara tidak sempat terlihat karena tidak ada frame yang cukup untuk melaporkannya. Pada scene besar, pemuatan memakan banyak frame sehingga progres naik bertahap sampai 0,9, lalu dinormalisasi menjadi 1

## Tugas 3.3 - Penyimpanan Permanen

Saya menggunakan PlayerPrefs untuk menyimpan total poin pemain agar nilainya tetap tersedia ketika aplikasi ditutup dan dibuka kembali. Nilai poin dimuat menggunakan PlayerPrefs.GetInt(), diperbarui menggunakan PlayerPrefs.SetInt(), lalu disimpan menggunakan PlayerPrefs.Save()

PlayerPrefs memiliki dua kelemahan untuk data kompetitif seperti papan peringkat. Pertama, data disimpan secara lokal pada perangkat sehingga pengguna dapat mengubah nilainya dan memanipulasi skor. Kedua, data lokal tidak otomatis tersinkronisasi atau terverifikasi oleh server, sehingga skor dari perangkat berbeda tidak dapat dipercaya sebagai papan peringkat global
