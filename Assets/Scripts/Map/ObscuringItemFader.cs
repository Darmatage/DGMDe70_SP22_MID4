using System.Collections;
using UnityEngine;

namespace Game.Map
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObscuringItemFader : MonoBehaviour
    {
        // Obscuring Item Fading - ObscuringItemFader
        private float fadeInSeconds = 0.25f;
        private float fadeOutSeconds = 0.35f;
        private float targetAlpha = 0.45f;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void FadeOut()
        {
            StartCoroutine(FadeOutRoutine());
        }

        public void FadeIn()
        {
            StartCoroutine(FadeInRoutine());
        }

        private IEnumerator FadeInRoutine()
        {
            float currentAlpha = spriteRenderer.color.a;
            float distance = 1f - currentAlpha;

            while (1f - currentAlpha > 0.01f)
            {
                currentAlpha = currentAlpha + distance / fadeInSeconds * Time.deltaTime;
                spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
                yield return null;
            }
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }

        private IEnumerator FadeOutRoutine()
        {
            float currentAlpha = spriteRenderer.color.a;
            float distance = currentAlpha - targetAlpha;

            while(currentAlpha - targetAlpha > 0.01f)
            {
                currentAlpha = currentAlpha - distance / fadeOutSeconds * Time.deltaTime;
                spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
                yield return null;
            }

            spriteRenderer.color = new Color(1f, 1f, 1f, targetAlpha);
        }
    }
}
