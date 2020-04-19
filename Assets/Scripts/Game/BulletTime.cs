using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BulletTime : MonoBehaviour
{

    private bool active = false;
    public bool Active
    {
        get { return active; }
        set
        {
            Time.timeScale = value ? 0.05f : 1;
            /* if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(FxRamp(value)); */
            active = value;
        }
    }

    PostProcessVolume volume;
    Vignette vignette;

    Coroutine coroutine;
    float intensity = 0;

    void Start()
    {
        vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.enabled.Override(true);
        vignette.intensity.Override(1f);

        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
        vignette.intensity.value = 0.2f;
    }

    IEnumerator FxRamp(bool on)
    {
        while(on ? intensity < 1 : intensity > 0)
        {
            vignette.intensity.value = 0.2f + intensity * 0.3f;
            yield return null;
            intensity += on ? 0.05f : -0.05f;
        }

        coroutine = null;
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(volume, true, true);
    }
}
