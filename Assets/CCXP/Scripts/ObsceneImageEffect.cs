using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObsceneImageEffect : MonoBehaviour {

    private Camera PlatformCamera;

	[Range(64.0f, 512.0f)] public float BlockCount = 64;

	[SerializeField]
	private Shader _effect;
	[SerializeField]
	private Texture2D _noise; 
	private Material _effectMaterial;

    void OnEnable()
	{
        PlatformCamera = GetComponent<Camera>();
        
		
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
        //print(PlatformCamera.ToString());

        _effectMaterial = new Material(_effect);
        _effectMaterial.SetTexture("_NoiseTex", _noise);

        float k = PlatformCamera.aspect;
		Vector2 count = new Vector2(BlockCount, BlockCount/k);
		Vector2 size = new Vector2(1.0f/count.x, 1.0f/count.y);
		_effectMaterial.SetVector("BlockCount", count);
		_effectMaterial.SetVector("BlockSize", size);

		if(_effectMaterial != null)
			Graphics.Blit(src, dst, _effectMaterial);
	}
}
