namespace DonutEngine.Backbone;
using System.Numerics;
using Raylib_cs;
using Box2DX.Common;
using static Raylib_cs.Raylib;
using DonutEngine.Backbone.Systems;

public partial class CameraHandler : SystemsClass
{
    internal static Vector2 UpdateCameraPlayerBoundsPush_bbox = new Vector2(0.04f, 0.04f);
	public void UpdateCameraPlayerBoundsPush(ref Camera2D camera, Vector2 pos, float delta, int width, int height)
	{
		Vector2 bboxWorldMin = GetScreenToWorld2D(new((1 - UpdateCameraPlayerBoundsPush_bbox.X) * 0.5f * width, (1 - UpdateCameraPlayerBoundsPush_bbox.Y) * 0.5f * height), camera);
		Vector2 bboxWorldMax = GetScreenToWorld2D(new((1 + UpdateCameraPlayerBoundsPush_bbox.X) * 0.5f * width, (1 + UpdateCameraPlayerBoundsPush_bbox.Y) * 0.5f * height), camera);
		camera.offset = new((1 - UpdateCameraPlayerBoundsPush_bbox.X) * 0.5f * width, (1 - UpdateCameraPlayerBoundsPush_bbox.Y) * 0.5f * height);

		if (pos.X < bboxWorldMin.X)
		{
			camera.target.X = pos.X;
		}
		if (pos.Y < bboxWorldMin.Y)
		{
			camera.target.Y = pos.Y;
		}
		if (pos.X > bboxWorldMax.X)
		{
			camera.target.X = bboxWorldMin.X + (pos.X - bboxWorldMax.X);
		}
		if (pos.Y > bboxWorldMax.Y)
		{
			camera.target.Y = bboxWorldMin.Y + (pos.Y - bboxWorldMax.Y);
		}
	}
	private float fminf(float a, float b)
	{
		return MathF.Min(a, b);
	}
	private float fmaxf(float a, float b)
	{
		return MathF.Max(a, b);
	}
	internal float UpdateCameraCenterSmoothFollow_minSpeed = 30F;
	internal float UpdateCameraCenterSmoothFollow_minEffectLength = 10F;
	internal float UpdateCameraCenterSmoothFollow_fractionSpeed = 0.8f;

	public void UpdateCameraCenterSmoothFollow(ref Camera2D camera, Vector2 pos, float delta, int width, int height)
	{
		camera.offset = new(width / 2.0f, height / 2.0f);
		Vector2 diff = Vector2.Subtract(new(pos.X, pos.Y), camera.target);
		float length = diff.Length();

		if (length > UpdateCameraCenterSmoothFollow_minEffectLength)
		{
			float speed = fmaxf(UpdateCameraCenterSmoothFollow_fractionSpeed * length, UpdateCameraCenterSmoothFollow_minSpeed);
			camera.target = Vector2.Add(camera.target, diff * (speed * delta / length));
		}
	}

	static void UpdateCameraCenter(ref Camera2D camera, Vector2 pos, float delta, int width, int height)
	{
		camera.offset = new Vector2(width / 2, height / 2);
		camera.target = pos;
	}
}