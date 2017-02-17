using UnityEngine;
using System;
using Wikitude;
using System.Runtime.InteropServices;

/// <summary>
/// Handles getting the camera frame from the device and forwarding it to the Wikitude SDK.
/// </summary>
public class CustomCameraController : SampleController
{
	public WikitudeCamera WikitudeCam;

	protected WebCamTexture _feed;
	protected bool _imageUpsideDown = false;

	public const int FrameWidth = 640;
	public const int FrameHeight = 480;

	private int _frameDataSize = 0; 
	private int _frameIndex = 0;
	private Color32[] _pixels;

	public void OnInputPluginRegistered() {
		Initialize();
	}

	protected virtual void Initialize() {
		foreach (var device in WebCamTexture.devices) {
			if (!device.isFrontFacing) {
				_feed = new WebCamTexture(device.name, FrameWidth, FrameHeight);
				_feed.Play();
				break;
			}
		}

		if (_feed == null) {
			Debug.LogError("Could not find any cameras on the device.");
		}

		ResetBuffers(FrameWidth, FrameHeight, 4);

		if (_feed.videoRotationAngle == 270) {
			// If the camera angle is 270, it means that it is inverted, such as the camera found on the 5X, 
			// so we flip the invert and mirror flags on the camera to rotate the image upside down
			WikitudeCam.InvertedFrame = false;
			WikitudeCam.EnableMirroring = true;

			_imageUpsideDown = true;
		}
	}

	protected virtual void ResetBuffers(int width, int height, int bytesPerPixel) {
		_frameDataSize = width * height * bytesPerPixel;
		_pixels = new Color32[width * height];

		WikitudeCam.InputFrameWidth = width;
		WikitudeCam.InputFrameHeight = height;
	}

	protected override void Update() {
		base.Update();
		if (_feed == null || !_feed.didUpdateThisFrame) {
			return;
		}

		if (_feed.width != FrameWidth || _feed.height != FrameHeight) {
			Debug.LogError("Camera feed has unexpected size.");
			return;
		}

		int newFrameDataSize = _feed.width * _feed.height * 4;
		if (newFrameDataSize != _frameDataSize) {
			ResetBuffers(_feed.width, _feed.height, 4);
		}

		_feed.GetPixels32(_pixels);
		SendNewCameraFrame();
	}

	private void SendNewCameraFrame() {
		GCHandle handle = default(GCHandle);
		try {
			handle = GCHandle.Alloc(_pixels, GCHandleType.Pinned);
			IntPtr frameData = handle.AddrOfPinnedObject();
			WikitudeCam.NewCameraFrame(++_frameIndex, _frameDataSize, frameData);
		} finally {
			if (handle != default(GCHandle)) {
				handle.Free();
			}
		}
	}

	protected virtual void Cleanup() {
		_frameDataSize = 0;
		if (_feed != null) {
			_feed.Stop();
			_feed = null;
		}
	}

	private void OnApplicationPause(bool paused) {
		if (paused) {
			Cleanup();
		} else {
			Initialize();
		}
	}

	private void OnDestroy() {
		Cleanup();
	}
}
