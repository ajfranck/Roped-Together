using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public Material mat;
    public float thickness = 1.03f;
    [ColorUsage(true, true)]
    public Color colorOutline;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        GameObject outlineObject = Instantiate(this.gameObject,transform.position,transform.rotation,transform);
        outlineObject.transform.localScale = new Vector3(1, 1, 1);
        Renderer rend = outlineObject.GetComponent<Renderer>();
        rend.material = mat;
        rend.material.SetFloat("_Thickness", thickness);
        rend.material.SetColor("_OutlineColor", colorOutline);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        rend.enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        outlineObject.GetComponent<OutlineEffect>().enabled = false;
        this.rend = rend;
    }
    private void OnMouseExit()
    {
        rend.enabled = false;
    }
    private void OnMouseEnter()
    {
        rend.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
