using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowHit : MonoBehaviour
{
    [SerializeField] private Color _glowColor;          // Color to glow to
    [SerializeField] private List<Renderer> _meshRenderers;         // Mesh renderers that we are going to change
    private List<Material> _materials;                          // Materials from all mesh renderers listed

    private void Start()
    {
        Renderer[] meshRenderers = GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in meshRenderers)
        {
            _meshRenderers.Add(renderer);
        }
    }

    public void StartGlow()
    {
        StartCoroutine(GlowCor());
    }

    private IEnumerator GlowCor()
    {
        _materials = new List<Material>();

        foreach (Renderer renderer in _meshRenderers)
        {
            //yield return new WaitUntil(() => renderer.gameObject.activeSelf);
            _materials.AddRange(renderer.materials);
        }

        float startTime = Time.time;
        float duration = 0.15f; // Time taken for the glow animation

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;

            foreach (var mat in _materials)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.Lerp(Color.black, _glowColor, t));
            }

            yield return null;
        }

        // Ensure the glow is set to its maximum at the end of the animation
        foreach (var mat in _materials)
        {
            mat.SetColor("_EmissionColor", _glowColor);
        }

        // Wait for a short duration with the glow active
        yield return new WaitForSeconds(0.075f);

        // Fade out the glow
        startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;

            foreach (var mat in _materials)
            {
                mat.SetColor("_EmissionColor", Color.Lerp(_glowColor, Color.black, t));
            }

            yield return null;
        }

        // Ensure the glow is completely turned off at the end
        foreach (var mat in _materials)
        {
            mat.SetColor("_EmissionColor", Color.black);
        }
    }
}
