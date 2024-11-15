using System.Collections;
using UnityEngine;

public class InvincibilityComponent : MonoBehaviour
{
    #region Editor Settings
    [Tooltip("Material to switch to during the flash")]
    [SerializeField] private Material blinkMaterial;  // Material untuk efek berkedip

    [Tooltip("Duration of the flash.")]
    [SerializeField] private int blinkingCount = 7;   // Jumlah kedipan
    [SerializeField] private float blinkInterval = 0.1f;  // Interval kedipan
    #endregion

    #region Private Fields
    // SpriteRenderer yang akan berkedip
    private SpriteRenderer spriteRenderer;

    // Material asli yang digunakan oleh SpriteRenderer
    private Material originalMaterial;

    // Menyimpan Coroutine yang sedang berjalan
    private Coroutine flashCoroutine;

    // Status invincibility
    public bool isInvincible = false;
    #endregion

    void Start()
    {
        // Ambil komponen SpriteRenderer yang ada pada GameObject ini
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Simpan material asli yang digunakan oleh spriteRenderer
        originalMaterial = spriteRenderer.material;
    }

    // Coroutine untuk efek berkedip
    private IEnumerator FlashRoutine()
    {
        isInvincible = true;  // Set Entitas menjadi invincible selama efek berkedip

        for (int i = 0; i < blinkingCount; i++)
        {
            // Ganti material menjadi material berkedip
            spriteRenderer.material = blinkMaterial;

            // Tunggu sesuai interval kedipan
            yield return new WaitForSeconds(blinkInterval);

            // Kembalikan material ke asli
            spriteRenderer.material = originalMaterial;

            // Tunggu interval kedipan berikutnya
            yield return new WaitForSeconds(blinkInterval);
        }

        // Setelah kedipan selesai, kembalikan ke kondisi normal
        isInvincible = false; // Set Entitas kembali ke kondisi rentan
        flashCoroutine = null;  // Clear reference to the coroutine
    }

    // Fungsi untuk memulai efek berkedip
    public void Flash()
    {
        // Jika efek kedipan sudah berjalan, hentikan terlebih dahulu
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        // Mulai coroutine dengan efek berkedip
        flashCoroutine = StartCoroutine(FlashRoutine());
    }
}
