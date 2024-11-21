using System.Collections;
using UnityEngine;

public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private Material blinkMaterial;  // Material untuk efek berkedip
    [SerializeField] private int blinkingCount = 7;   // Jumlah kedipan
    [SerializeField] private float blinkInterval = 0.1f;  // Interval kedipan

    
    private SpriteRenderer spriteRenderer;  // Material asli yang digunakan oleh SpriteRenderer
    private Material originalMaterial;

    // Status invincibility
    public bool isInvincible = false;

    void Awake()
    {
        // Ambil komponen SpriteRenderer yang ada pada GameObject ini
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Simpan material asli yang digunakan oleh spriteRenderer
        originalMaterial = spriteRenderer.material;
    }

    public void TriggerInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvicibilityCoroutine());
        }
    }

    // Coroutine untuk efek berkedip
    private IEnumerator InvicibilityCoroutine()
    {
        isInvincible = true;  // Set Entitas menjadi invincible selama efek berkedip

        for (int i = 0; i < blinkingCount; i++)
        {
            // Ganti material menjadi material berkedip
            spriteRenderer.material = blinkMaterial;

            // Tunggu sesuai interval kedipan
            yield return new WaitForSeconds(blinkInterval / 2);

            // Kembalikan material ke asli
            spriteRenderer.material = originalMaterial;

            // Tunggu interval kedipan berikutnya
            yield return new WaitForSeconds(blinkInterval / 2);
        }

        // Setelah kedipan selesai, kembalikan ke kondisi normal
        spriteRenderer.material = originalMaterial;
        isInvincible = false; // Set Entitas kembali ke kondisi rentan
    }

}
