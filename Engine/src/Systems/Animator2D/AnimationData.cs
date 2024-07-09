using Raylib_CSharp.Textures;
using Raylib_CSharp.Transformations;
using Newtonsoft.Json;
using Engine.Assets;
using System.Diagnostics.CodeAnalysis;
namespace Engine.Systems.ASM;

public class AnimationData
{
    [JsonProperty(propertyName:"Frame Width")] public float FrameWidth;
	[JsonProperty(propertyName:"Frame Height")] public float FrameHeight;
	[JsonIgnore] internal float TimeRemainingFramesCounter;
	[JsonIgnore] internal int PlaybackPosition;
	[JsonIgnore] internal int DelayFramesCounter;
	[JsonProperty(propertyName:"Sprite Sheet Rows")] public int Rows;
	[JsonProperty(propertyName:"Sprite Sheet Columns")] public int Columns;
	[JsonProperty(propertyName:"Frames per Second")] public int Framerate;
	[JsonIgnore] internal int CurrentRow;
	[JsonIgnore] internal int CurrentColumn;
	[JsonIgnore] internal int CurrentFrame;
	[JsonIgnore] internal bool bFlipH;
	[JsonIgnore] internal bool bFlipV;
	[JsonProperty(propertyName:"Looping")] public bool bCanLoop;
	[JsonProperty(propertyName:"Play in Reverse")] public bool bReverse;
	[JsonProperty(propertyName:"Continuous")] public bool bContinuous;
	[JsonIgnore] internal bool bPaused;
	[JsonIgnore] internal bool bIsAnimationFinished;
	[JsonIgnore] internal bool bHasStartedPlaying;
	[JsonIgnore] internal Rectangle FrameRec;
    [JsonProperty(propertyName:"Texture Filename")] public string textureName;
    [JsonIgnore] Texture2D _sprite;
	[JsonIgnore] internal Texture2D Sprite
    {
        get
        {
            if(!_sprite.IsReady())
            {
		        _sprite = Textures.GetTexture(textureName);
            }
            return _sprite;
        }
    }
	[JsonProperty(propertyName:"Animation Name")] public string Name;

    public AnimationData(string AnimatonName, int NumOfFramesPerRow, int NumOfRows, int Speed, string textureName, bool bPlayInReverse = false, bool bContinuous = false, bool bLooping = true)
	{
		Name = AnimatonName;
		Framerate = Speed == 0 ? 1 : Speed;
		Columns = NumOfFramesPerRow;
		Rows = NumOfRows == 0 ? 1 : NumOfRows;
		bReverse = bPlayInReverse;
		bCanLoop = bLooping;
		this.bContinuous = bContinuous;
		PlaybackPosition = 0;
		DelayFramesCounter = 0;
		CurrentFrame = 0;
		CurrentRow = 0;
		CurrentColumn = 0;
		bFlipH = false;
		bFlipV = false;
		this.textureName = textureName;
	}

	#if DEBUG
	public void GenerateJson()
	{

	}
	#endif

}