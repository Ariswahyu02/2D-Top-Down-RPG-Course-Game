using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float flashTime = .2f;
    private SpriteRenderer spriteRenderer;
    private Material defaultMat;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public float GetFlashTime()
    {
        return flashTime;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.material = defaultMat;
    }
}
