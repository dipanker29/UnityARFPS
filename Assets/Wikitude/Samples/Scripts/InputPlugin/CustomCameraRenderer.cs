using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Handles custom camera rendering of a frame.
/// </summary>
public class CustomCameraRenderer : MonoBehaviour 
{
	public Material EffectMaterial;

	private Texture _currentFrame;
	public Texture CurrentFrame {
		set {
			_currentFrame = value;
			enabled = true;
			SetCommandBuffer();
		}
	}

	private struct ImageRotation {
		public bool FlipHorizontally;
		public bool FlipVertically;
		public bool Rotate;

		public ImageRotation(bool flipHorizontally, bool flipVertically, bool rotate) {
			FlipHorizontally = flipHorizontally;
			FlipVertically = flipVertically;
			Rotate = rotate;
		}
	}

	[HideInInspector]
	public bool FlipImage = false;

	private CommandBuffer _drawFrameBuffer;
	private int _currentScreenWidth = 0;

	private void SetCommandBuffer() {

		var camera = GetComponent<Camera>();
		CameraEvent eventForBlit;

		if (camera.actualRenderingPath == RenderingPath.Forward) {
			eventForBlit = CameraEvent.BeforeForwardOpaque;
		} else {
			eventForBlit = CameraEvent.BeforeGBuffer;
		}

		if (_drawFrameBuffer != null) {
			camera.RemoveCommandBuffer(eventForBlit, _drawFrameBuffer);
		}

		if (_currentFrame != null) {
			EffectMaterial.SetInt("_ResolutionX", CustomCameraController.FrameWidth);
			EffectMaterial.SetInt("_ResolutionY", CustomCameraController.FrameHeight);

			_drawFrameBuffer = new CommandBuffer();
			_drawFrameBuffer.Blit(_currentFrame, BuiltinRenderTextureType.CameraTarget, EffectMaterial);
			camera.AddCommandBuffer(eventForBlit, _drawFrameBuffer);
		}
	}

	void Update() {
		if (_currentFrame.width == CustomCameraController.FrameWidth && _currentFrame.height == CustomCameraController.FrameHeight) {
			if (Screen.width != _currentScreenWidth && Screen.orientation != ScreenOrientation.Unknown) {
				_currentScreenWidth = Screen.width;
				UpdateOrientation(Screen.orientation);
			}
		}
	}

	public void UpdateOrientation(ScreenOrientation screenOrientation) {
		var rotation = new ImageRotation(false, false, false);
		
		switch (screenOrientation) {
			case ScreenOrientation.LandscapeLeft:
				rotation = new ImageRotation(false, false, false);
				break;
			case ScreenOrientation.LandscapeRight:
				rotation = new ImageRotation(true, true, false);
				break;
			case ScreenOrientation.Portrait:
				rotation = new ImageRotation(false, true, true);
				break;
			case ScreenOrientation.PortraitUpsideDown:
				rotation = new ImageRotation(true, false, true);
				break;
		}

		if (FlipImage) {
			rotation.FlipVertically = !rotation.FlipVertically;
			rotation.FlipHorizontally = !rotation.FlipHorizontally;
		}

		SetImageRotation(rotation);

		float frameAspectRatio = (float)_currentFrame.width / (float)_currentFrame.height;
		float screenAspectRatio = (float)Screen.width / (float)Screen.height;

		float ratio = 1.0f;

		switch (screenOrientation) {
		case ScreenOrientation.LandscapeLeft:
		case ScreenOrientation.LandscapeRight:
			ratio = frameAspectRatio / screenAspectRatio;
			break;
		case ScreenOrientation.Portrait:
		case ScreenOrientation.PortraitUpsideDown:
			ratio = frameAspectRatio * screenAspectRatio;
			break;
		}

		EffectMaterial.SetFloat("_Scale", ratio);
		EffectMaterial.SetFloat("_Pan", (1.0f - ratio) / 2.0f);
	}

	void SetImageRotation(ImageRotation rotation) {
		EffectMaterial.SetFloat("_FlipU", rotation.FlipHorizontally ? 1 : 0);
		EffectMaterial.SetFloat("_FlipV", rotation.FlipVertically ? 1 : 0);
		EffectMaterial.SetFloat("_Rotate", rotation.Rotate ? 1 : 0);
	}
}
