# Laporan Modul 02

## Checkpoint 2.A

Pada pengujian ini, saya mengamati urutan pemanggilan fungsi lifecycle Unity melalui Console. `Awake()` dijalankan saat objek diinisialisasi, sedangkan `Start()` dijalankan sebelum `Update()` pertama. Mengakses objek lain di `Awake()` bisa berisiko karena objek tersebut belum tentu selesai diinisialisasi. Oleh karena itu, `Start()` biasanya lebih cocok untuk mengambil referensi atau menggunakan data dari objek lain.

Log Console:
*[ProbeB] 1. Awake | frame 0
UnityEngine.Debug:Log (object)
LifecycleProbe:Awake () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:17)

[ProbeB] 2. OnEnable | frame 0
UnityEngine.Debug:Log (object)
LifecycleProbe:OnEnable () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:22)

[ProbeA] 1. Awake | frame 0
UnityEngine.Debug:Log (object)
LifecycleProbe:Awake () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:17)

[ProbeA] 2. OnEnable | frame 0
UnityEngine.Debug:Log (object)
LifecycleProbe:OnEnable () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:22)

[ProbeB] 3. Start | frame 1
UnityEngine.Debug:Log (object)
LifecycleProbe:Start () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:27)

[ProbeA] 3. Start | frame 1
UnityEngine.Debug:Log (object)
LifecycleProbe:Start () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:27)

[ProbeB] 5. FixedUpdate #1 | frame 1
UnityEngine.Debug:Log (object)
LifecycleProbe:FixedUpdate () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:47)

[ProbeA] 5. FixedUpdate #1 | frame 1
UnityEngine.Debug:Log (object)
LifecycleProbe:FixedUpdate () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:47)

[ProbeB] 4. Update #1 | frame 1
UnityEngine.Debug:Log (object)
LifecycleProbe:Update () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:37)

[ProbeA] 4. Update #1 | frame 1
UnityEngine.Debug:Log (object)
LifecycleProbe:Update () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:37)

[ProbeB] 5. FixedUpdate #2 | frame 2
UnityEngine.Debug:Log (object)
LifecycleProbe:FixedUpdate () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:47)

[ProbeA] 5. FixedUpdate #2 | frame 2
UnityEngine.Debug:Log (object)
LifecycleProbe:FixedUpdate () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:47)

[ProbeB] 4. Update #2 | frame 2
UnityEngine.Debug:Log (object)
LifecycleProbe:Update () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:37)

[ProbeA] 4. Update #2 | frame 2
UnityEngine.Debug:Log (object)
LifecycleProbe:Update () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:37)

[ProbeB] 5. FixedUpdate #3 | frame 3
UnityEngine.Debug:Log (object)
LifecycleProbe:FixedUpdate () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:47)

[ProbeA] 5. FixedUpdate #3 | frame 3
UnityEngine.Debug:Log (object)
LifecycleProbe:FixedUpdate () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:47)

[ProbeB] 4. Update #3 | frame 3
UnityEngine.Debug:Log (object)
LifecycleProbe:Update () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:37)

[ProbeA] 4. Update #3 | frame 3
UnityEngine.Debug:Log (object)
LifecycleProbe:Update () (at Assets/_Project/Scripts/Core/LifecycleProbe.cs:37)*

## Checkpoint 2.B

Saat mencoba memasang `InteractiveObject` langsung ke GameObject, Unity menampilkan pesan:

`Can't add script behaviour 'InteractiveObject'. The script class can't be abstract!`

Hal ini terjadi karena `InteractiveObject` merupakan abstract class. Class ini berfungsi sebagai induk bagi objek interaktif, sehingga harus diturunkan menjadi class lain sebelum digunakan pada GameObject.

## Checkpoint 2.C

Saya mencoba mengklik `PulsingArtifact` dan `RotatingArtifact`. Kedua objek berhasil menampilkan pesan yang berbeda di Console. `PulsingArtifact` menampilkan pesan bahwa Artefak Denyut bernilai 20 poin, sedangkan `RotatingArtifact` menampilkan pesan bahwa Artefak Putar bernilai 10 poin dan berputar cepat saat aktif.

## Checkpoint 2.D

Pengujian dilakukan langsung pada HP Android menggunakan sentuhan jari. Kedua objek dapat merespons sentuhan, dan pesan interaksi muncul di terminal melalui `adb logcat`.

## Tugas 2.1

Saya membuat class `ColorShiftArtifact` yang merupakan turunan dari `InteractiveObject`. Objek ini mengubah warna secara bertahap setelah diinteraksikan dengan menggunakan `Renderer.material.color` dan `Color.Lerp()`. Saya juga mengubah `GetDescription()` agar memiliki deskripsi sendiri.


using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PositionComparisonLogger : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(
            $"[Posisi Update] Frame {Time.frameCount}: " +
            $"{transform.position}"
        );
    }

    private void LateUpdate()
    {
        Debug.Log(
            $"[Posisi LateUpdate] Frame {Time.frameCount}: " +
            $"{transform.position}"
        );
    }
}

## Tugas 2.2

Saya menambahkan atribut `[Range(0f, 500f)]` pada `normalSpeed` dan `activatedSpeed` di `RotatingArtifact`. Setelah itu, kedua nilai ditampilkan dalam bentuk slider di Inspector. Slider memudahkan anggota tim yang bukan programmer untuk mengatur kecepatan objek tanpa harus mengubah kode.

[Header("Rotasi")]
[SerializeField] private Vector3 rotationAxis = Vector3.up;

[Range(0f, 500f)]
[SerializeField] private float normalSpeed = 30f;

[Range(0f, 500f)]
[SerializeField] private float activatedSpeed = 240f;

## Tugas 2.3

Pada frame 391, posisi X yang tercatat di `Update()` adalah 2.65, sedangkan di `LateUpdate()` adalah 2.66. Perbedaan ini terjadi karena objek bergerak setelah pencatatan di `Update()` dan sebelum pencatatan di `LateUpdate()`. Salah satu penggunaan `LateUpdate()` adalah mengatur kamera agar mengikuti pemain setelah pemain bergerak.


using UnityEngine;

public class MoveInUpdate : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}


using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PositionComparisonLogger : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(
            $"[Posisi Update] Frame {Time.frameCount}: " +
            $"{transform.position}"
        );
    }

    private void LateUpdate()
    {
        Debug.Log(
            $"[Posisi LateUpdate] Frame {Time.frameCount}: " +
            $"{transform.position}"
        );
    }
}

## Tugas 2.4

Secara default, `FixedUpdate()` berjalan sekitar 50 kali per detik karena interval waktunya 0,02 detik. Sementara itu, `Update()` dipanggil setiap frame sehingga jumlahnya bergantung pada frame rate game. `FixedUpdate()` biasanya digunakan untuk proses fisika, sedangkan `Update()` digunakan untuk logika yang perlu diperbarui setiap frame.
