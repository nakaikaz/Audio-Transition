using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioTransition : MonoBehaviour
{
    [SerializeField] AudioSource audioSource = default;

    [Header("フェードの速さ")]
    [SerializeField] float step = 0.2f;

    bool isFadeOut = false;

    void Awake()
    {
        // シーン遷移時にオブジェクトを破棄しない
        DontDestroyOnLoad(gameObject);

        audioSource.volume = 0f;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInHandler());
    }

    public void FadeOut(bool destroyWhenFadeout = false)
    {
        isFadeOut = true;

        StartCoroutine(FadeOutHandler(destroyWhenFadeout));
    }

    /// <summary>
    /// オーデイオソースをフェードインさせるコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeInHandler()
    {
        float t = 0f;

        while(audioSource.volume < 1f && isFadeOut == false)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, t);

            t += Time.deltaTime * step;

            yield return null;
        }
    }

    /// <summary>
    /// オーディオソースをフェードアウトさせるコルーチン
    /// </summary>
    /// <param name="destroyWhenFadeout">trueであれば、フェードアウト完了時に自身を破棄する</param>
    /// <returns></returns>
    IEnumerator FadeOutHandler(bool destroyWhenFadeout)
    {
        float t = 0f;

        float a = audioSource.volume;

        while(audioSource.volume > 0f)
        {
            audioSource.volume = Mathf.Lerp(a, 0f, t);

            t += Time.deltaTime * step;

            yield return null;
        }

        if(destroyWhenFadeout)
        {
            Destroy(gameObject);
        }
    }
}
