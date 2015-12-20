using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class PostprocessingEffectScript : MonoBehaviour 
{
	[Header("Our Shader")]
	public Shader PostProcessingShader;

	[Header("Values for the Vignette Effect")]
	public Texture VignetteTexture;
	[Range(0, 1)]
	public static float VignetteAmount = 0f;

	[Header("Values for some Screen Settings")]
	[Range(0,2)]
	public float BrightnessAmount = 1f;
	[Range(0,3)]
	public float SaturationAmount = 1f;
	[Range(0,3)]
	public float ContrastAmount = 1f;

	[Header("Values for Blur Effect")]
	[Range(0,0.2f)]
	public float BlurFactor = 0f;
	public CarInformation CarInfo;


	private Material shaderMaterial;
	public Material ShaderMaterial
	{
		get
		{
			if(shaderMaterial==null)
			{
				shaderMaterial = new Material(PostProcessingShader);
				shaderMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return shaderMaterial;
		}
	}

    void Start () 
	{
		if(!SystemInfo.supportsImageEffects)
		{
			Debug.LogError("Image Effects are not supported");
			this.enabled = false;
			return;
		}

		if(PostProcessingShader==null)
		{
			Debug.LogError("Missing Shader, please attach one to this Script");
			this.enabled = false;
			return;
		}

		if(PostProcessingShader!=null && !PostProcessingShader.isSupported)
		{
			Debug.LogError("Shader is not supported");
			this.enabled = false;
			return;
		}
	}

	void OnDisable()
	{
		if(shaderMaterial!=null)
		{
			DestroyImmediate(shaderMaterial);
		}
	}

	void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if(PostProcessingShader!=null)
		{
			ShaderMaterial.SetFloat("_BrightnessAmount", BrightnessAmount);
			ShaderMaterial.SetFloat("_SaturationAmount", SaturationAmount);
			ShaderMaterial.SetFloat("_ContrastAmount", ContrastAmount);

			ShaderMaterial.SetFloat("_BlurFactor", BlurFactor);

			ShaderMaterial.SetTexture("_VignetteTexture", VignetteTexture);
			ShaderMaterial.SetFloat("_VignetteAmount", VignetteAmount);

			Graphics.Blit(sourceTexture, destTexture, ShaderMaterial);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}
	
	void Update () 
	{
		if(Application.isPlaying)
		{
			// BlurFactor = (CarInfo.Speed/CarInfo.MaxSpeed)*0.2f;
		}
	}
}
