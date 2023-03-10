using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaderScript : MonoBehaviour
{
    //public Shader awesomeShader = null;
    public Material m_renderMaterial;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m_renderMaterial);
    }

    public void SetShader(Material mat)
    {
        m_renderMaterial = mat;
    }
}
