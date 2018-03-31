using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectMaskCamera : MonoBehaviour {

	[HideInInspector]
    [SerializeField]
    private Camera _camera;

	[SerializeField]
	private Shader _shader;

	private string _globalTextureName = "_GlobalObjectMask";	

	void OnEnable()
	{
		GenerateRT();

		_camera.SetReplacementShader(_shader, "ObjectMask");
	}

	void GenerateRT()
	{
        _camera = GetComponent<Camera>();

        if (_camera.targetTexture != null)
        {
            RenderTexture temp = _camera.targetTexture;

            _camera.targetTexture = null;
            DestroyImmediate(temp);
        }

        _camera.targetTexture = new RenderTexture(_camera.pixelWidth, _camera.pixelHeight, 16);
        _camera.targetTexture.filterMode = FilterMode.Bilinear;

        Shader.SetGlobalTexture(_globalTextureName, _camera.targetTexture);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
        Graphics.Blit(src, _camera.targetTexture);
	}
}
