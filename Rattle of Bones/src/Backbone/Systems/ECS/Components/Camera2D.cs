namespace DonutEngine.Backbone;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;
using System.Numerics;

public class GameCamera2D : Component
{
	[JsonProperty("IsActive")]
	public bool IsActive { get; set; }
	internal static Vector2 UpdateCameraPlayerBoundsPush_bbox = new Vector2(0.03f, 0.02f);
	public void UpdateCameraPlayerBoundsPush(ref Camera2D camera, PositionComponent pos, float delta, int width, int height)
	{
		Vector2 bboxWorldMin = GetScreenToWorld2D(new((1 - UpdateCameraPlayerBoundsPush_bbox.X) * 0.5f * width, (1 - UpdateCameraPlayerBoundsPush_bbox.Y) * 0.5f * height), camera);
		Vector2 bboxWorldMax = GetScreenToWorld2D(new((1 + UpdateCameraPlayerBoundsPush_bbox.X) * 0.5f * width, (1 + UpdateCameraPlayerBoundsPush_bbox.Y) * 0.5f * height), camera);
		camera.offset = new((1 - UpdateCameraPlayerBoundsPush_bbox.X) * 0.5f * width, (1 - UpdateCameraPlayerBoundsPush_bbox.Y) * 0.5f * height);

		if (pos.GetPosition().X < bboxWorldMin.X)
		{
			camera.target.X = pos.GetPosition().X;
		}
		if (pos.GetPosition().Y < bboxWorldMin.Y)
		{
			camera.target.Y = pos.GetPosition().Y;
		}
		if (pos.GetPosition().X > bboxWorldMax.X)
		{
			camera.target.X = bboxWorldMin.X + (pos.GetPosition().X - bboxWorldMax.X);
		}
		if (pos.GetPosition().Y > bboxWorldMax.Y)
		{
			camera.target.Y = bboxWorldMin.Y + (pos.GetPosition().Y - bboxWorldMax.Y);
		}
	}

	public void UpdateCameraCenter(ref Camera2D camera, PositionComponent pos, float delta, int width, int height)
	{
		camera.offset = new(width / 2.0f, height / 2.0f);
		camera.target = pos.GetPosition();
	}

	public void UpdateCameraCenterInsideMap(ref Camera2D camera, PositionComponent pos, float delta, int width, int height)
	{
		camera.target = pos.GetPosition();
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

	public void UpdateCameraCenterSmoothFollow(ref Camera2D camera, PositionComponent pos, float delta, int width, int height)
	{
		camera.offset = new(width / 2.0f, height / 2.0f);
		Vector2 diff = Vector2.Subtract(pos.GetPosition(), camera.target);
		float length = diff.Length();

		if (length > UpdateCameraCenterSmoothFollow_minEffectLength)
		{
			float speed = fmaxf(UpdateCameraCenterSmoothFollow_fractionSpeed * length, UpdateCameraCenterSmoothFollow_minSpeed);
			camera.target = Vector2.Add(camera.target, diff * (speed * delta / length));
		}
	}
	PositionComponent? position;
    public override void OnAddedToEntity(Entity entity)
    {
        position = entity.GetComponent<PositionComponent>();
		ECS.ecsLateUpdate += LateUpdate;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        //throw new NotImplementedException();
    }

	public void LateUpdate(float deltaTime)
	{
		if(IsActive)
		{
			UpdateCameraCenterSmoothFollow(ref Program.donutCamera, position, deltaTime, 2, 1);
		}
	}
}