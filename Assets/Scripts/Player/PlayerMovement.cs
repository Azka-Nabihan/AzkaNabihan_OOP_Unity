using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Kecepatan maksimum pada sumbu x dan y
    [SerializeField] Vector2 maxSpeed;
    // Waktu yang diperlukan untuk mencapai kecepatan penuh
    [SerializeField] Vector2 timeToFullSpeed;
    // Waktu yang diperlukan untuk berhenti sepenuhnya
    [SerializeField] Vector2 timeToStop;
    // Batas kecepatan minimum agar Player dianggap berhenti
    [SerializeField] Vector2 stopClamp;
    
    // Arah gerakan Player berdasarkan input
    Vector2 moveDirection;
    // Kecepatan gerakan yang diberikan saat Player bergerak
    Vector2 moveVelocity;
    // Gaya gesek saat Player bergerak
    Vector2 moveFriction;
    // Gaya gesek saat Player berhenti
    Vector2 stopFriction;
    // Komponen Rigidbody2D dari Player untuk mengatur physics
    Rigidbody2D rb;

    // Menyimpan batas kamera
    float xMin, xMax, yMin, yMax;

    // Fungsi ini dipanggil sekali saat object diinisialisasi
    void Start()
    {
        // Mengambil komponen Rigidbody2D pada Player
        rb = GetComponent<Rigidbody2D>();

        // Menghitung kecepatan dan gesekan berdasarkan waktu yang diberikan
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;      // Mengatur percepatan saat ada input
        moveFriction = -2 * maxSpeed / timeToFullSpeed;     // Mengatur gesekan saat Player bergerak
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop); // Mengatur gesekan saat Player berhenti

         // Menghitung batas berdasarkan ukuran kamera
        Camera cam = Camera.main;
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));

        // Menyimpan batas layar
        xMin = -screenBounds.x;
        xMax = screenBounds.x;
        yMin = -screenBounds.y;
        yMax = screenBounds.y;
    }

    // Fungsi ini dipanggil setiap frame
    void Update()
    {
        Move(); // Memanggil fungsi Move() untuk memproses input dan gerakan
        MoveBound();  // Batasi gerakan Player
    }

    // Fungsi untuk membatasi posisi Player
    public void MoveBound()
    {
        // Dapatkan posisi saat ini
        Vector3 currentPosition = transform.position;

        // Membatasi posisi pada sumbu x dan y
        currentPosition.x = Mathf.Clamp(currentPosition.x, xMin, xMax);
        currentPosition.y = Mathf.Clamp(currentPosition.y, yMin, yMax);

        // Terapkan kembali posisi yang sudah dibatasi
        transform.position = currentPosition;
    }

    // Fungsi untuk menggerakkan Player berdasarkan input
    public void Move()
    {
        // Mendapatkan input arah gerakan dalam vektor (x dan y)
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // Jika ada input gerakan
        if (moveDirection != Vector2.zero)
        {
            // Tambahkan kecepatan berdasarkan arah gerakan dan waktu
            rb.velocity += moveVelocity * moveDirection * Time.deltaTime;

            // Batasi kecepatan agar tidak melebihi maxSpeed
            rb.velocity = new Vector2(
                Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x),
                Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y)
            );
        }
        // Jika tidak ada input, tambahkan gaya gesekan untuk berhenti
        else
        {
            rb.velocity += GetFriction() * Time.deltaTime;

            // Periksa apakah kecepatan di bawah batas stopClamp, jika ya, set kecepatan ke nol
            if (Mathf.Abs(rb.velocity.x) < stopClamp.x) rb.velocity = new Vector2(0, rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) < stopClamp.y) rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    // Fungsi untuk menghitung gaya gesek saat Player bergerak atau berhenti
    public Vector2 GetFriction()
    {
        return new Vector2(
            // Mengatur gesekan pada sumbu x berdasarkan arah kecepatan
            rb.velocity.x != 0 ? (rb.velocity.x > 0 ? stopFriction.x : -stopFriction.x) : 0,
            // Mengatur gesekan pada sumbu y berdasarkan arah kecepatan
            rb.velocity.y != 0 ? (rb.velocity.y > 0 ? stopFriction.y : -stopFriction.y) : 0
        );
    }


    // Mengembalikan true jika Player bergerak, false jika Player diam
    public bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }
}
