namespace DonutEngine.Backbone;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

public static class GameCamera2D
{
	internal static Vector2 UpdateCameraPlayerBoundsPush_bbox = new Vector2(0.03f, 0.02f);
	public static void UpdateCameraPlayerBoundsPush(ref Camera2D camera, Player player, float delta, int width, int height)
	{
		Vector2 bboxWorldMin = GetScreenToWorld2D(new((1 - UpdateCameraPlayerBoundsPush_bbox.X) * 0.5f * width, (1 - UpdateCameraPlayerBoundsPush_bbox.Y) * 0.5f * height), camera);
		Vector2 bboxWorldMax = GetScreenToWorld2D(new((1 + UpdateCameraPlayerBoundsPush_bbox.X) * 0.5f * width, (1 + UpdateCameraPlayerBoundsPush_bbox.Y) * 0.5f * height), camera);
		camera.offset = new((1 - UpdateCameraPlayerBoundsPush_bbox.X) * 0.5f * width, (1 - UpdateCameraPlayerBoundsPush_bbox.Y) * 0.5f * height);

		if (player.physics2D.rigidbody2D.GetPosition().X < bboxWorldMin.X)
		{
			camera.target.X = player.physics2D.rigidbody2D.GetPosition().X;
		}
		if (player.physics2D.rigidbody2D.GetPosition().Y < bboxWorldMin.Y)
		{
			camera.target.Y = player.physics2D.rigidbody2D.GetPosition().Y;
		}
		if (player.physics2D.rigidbody2D.GetPosition().X > bboxWorldMax.X)
		{
			camera.target.X = bboxWorldMin.X + (player.physics2D.rigidbody2D.GetPosition().X - bboxWorldMax.X);
		}
		if (player.physics2D.rigidbody2D.GetPosition().Y > bboxWorldMax.Y)
		{
			camera.target.Y = bboxWorldMin.Y + (player.physics2D.rigidbody2D.GetPosition().Y - bboxWorldMax.Y);
		}
	}

	public static void UpdateCameraCenter(ref Camera2D camera, Player player, float delta, int width, int height)
	{
		camera.offset = new(width / 2.0f, height / 2.0f);
		camera.target = player.physics2D.rigidbody2D.GetPosition();
	}

	public static void UpdateCameraCenterInsideMap(ref Camera2D camera, Player player, float delta, int width, int height)
	{
		camera.target = player.physics2D.rigidbody2D.GetPosition();
		camera.offset = new(width / 2.0f, height / 2.0f);
		float minX = 1000F;
		float minY = 1000F;
		float maxX = -1000F;
		float maxY = -1000F;

		/*for (int i = 0; i < envItemsLength; i++)
		{
			EnvItem ei = envItems[i];
			minX = fminf(ei.rect.x, minX);
			maxX = fmaxf(ei.rect.x + ei.rect.width, maxX);
			minY = fminf(ei.rect.y, minY);
			maxY = fmaxf(ei.rect.y + ei.rect.height, maxY);

		}*/

		Vector2 max = GetWorldToScreen2D(new(maxX, maxY), camera);
		Vector2 min = GetWorldToScreen2D(new(minX, minY), camera);

		if (max.X < width)
		{
			camera.offset.X = width - (max.X - width / 2);
		}
		if (max.Y < height)
		{
			camera.offset.Y = height - (max.Y - height / 2);
		}
		if (min.X > 0)
		{
			camera.offset.X = width / 2 - min.X;
		}
		if (min.Y > 0)
		{
			camera.offset.Y = height / 2 - min.Y;
		}
	}
	private static float fminf(float a, float b)
	{
		return MathF.Min(a, b);
	}
	private static float fmaxf(float a, float b)
	{
		return MathF.Max(a, b);
	}
	internal static float UpdateCameraCenterSmoothFollow_minSpeed = 30F;
	internal static float UpdateCameraCenterSmoothFollow_minEffectLength = 10F;
	internal static float UpdateCameraCenterSmoothFollow_fractionSpeed = 0.8f;

	public static void UpdateCameraCenterSmoothFollow(ref Camera2D camera, Player player, float delta, int width, int height)
	{
		camera.offset = new(width / 2.0f, height / 2.0f);
		Vector2 diff = Vector2.Subtract(player.physics2D.rigidbody2D.GetPosition(), camera.target);
		float length = diff.Length();

		if (length > UpdateCameraCenterSmoothFollow_minEffectLength)
		{
			float speed = fmaxf(UpdateCameraCenterSmoothFollow_fractionSpeed * length, UpdateCameraCenterSmoothFollow_minSpeed);
			camera.target = Vector2.Add(camera.target, diff * (speed * delta / length));
		}
	}
		// Store pointers to the multiple update camera functions
		//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
		//cameraUpdatersDelegate cameraUpdaters[] = { UpdateCameraCenter, UpdateCameraCenterInsideMap, UpdateCameraCenterSmoothFollow, UpdateCameraEvenOutOnLanding, UpdateCameraPlayerBoundsPush };
		/*cameraUpdatersDelegate[] cameraUpdaters = { UpdateCameraCenter, UpdateCameraCenterInsideMap, UpdateCameraCenterSmoothFollow, UpdateCameraEvenOutOnLanding, UpdateCameraPlayerBoundsPush };

		int cameraOption = 0;
		//C++ TO C# CONVERTER WARNING: This 'sizeof' ratio was replaced with a direct reference to the array length:
		//ORIGINAL LINE: int cameraUpdatersLength = sizeof(cameraUpdaters)/sizeof(cameraUpdaters[0]);
		int cameraUpdatersLength = cameraUpdaters.Length;*/
}