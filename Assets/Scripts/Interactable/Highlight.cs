using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    //we assign all the renderers here through the inspector
    [SerializeField]
    private List<Renderer> renderers;
    [SerializeField]
    private Color color = Color.white;
    [SerializeField] private string highlightName;

    //helper list to cache all the materials ofd this object
    private List<Material> materials;

    public bool isAbleToHighlight = true;

    //Gets all the materials from each renderer
    private void Awake()
    {
        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            //A single child-object might have mutliple materials on it
            //that is why we need to all materials with "s"
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool val, InteractIndicator indicator)
    {
        if (val && isAbleToHighlight)
        {
            foreach (var material in materials)
            {
                //We need to enable the EMISSION
                material.EnableKeyword("_EMISSION");
                //before we can set the color
                material.SetColor("_EmissionColor", color);
            }
            indicator?.gameObject.SetActive(true);
            indicator?.SetName(highlightName);
        }
        else
        {
            foreach (var material in materials)
            {
                //we can just disable the EMISSION
                //if we don't use emission color anywhere else
                material.DisableKeyword("_EMISSION");
            }
            indicator?.gameObject.SetActive(false);
            indicator?.SetName("");
        }
    }
}
