using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Raylib_CSharp;
using Raylib_CSharp.Logging;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Transformations;

namespace Engine.Systems.ASM;

public class AnimationStateMachine :  SystemClass, IUpdateSys, IDrawUpdateSys, ILateUpdateSys
{
    
    private Dictionary<Entity, Dictionary<string, AnimationState>> _entityStates = new Dictionary<Entity, Dictionary<string, AnimationState>>();
    private Dictionary<Entity, AnimationState> _currentStates = new Dictionary<Entity, AnimationState>();

    public void AddState(Entity entity, AnimationState state)
    {
        if (!_entityStates.ContainsKey(entity))
        {
			_entityStates.Add(entity, new());
            state.Init(entity);
            _entityStates[entity].Add(state.GetAnimationName(), state);
            _currentStates[entity] = state;
            _currentStates[entity].Enter(this);
        }
        else
        {
            if(_entityStates[entity].ContainsValue(state))
            {
                return;
            }
            _entityStates[entity].Add(state.GetAnimationName(), state);
            state.Init(entity);
        }
    }

    public void LoadFolder()
    {

    }

    public void ChangeState(Entity entity, string newState)
    {
        try 
        {
            _entityStates[entity].TryGetValue(newState, out AnimationState state);
            _currentStates[entity].Exit();
            _currentStates[entity] = state;
            _currentStates[entity].Enter(this);
        }
        catch
        {
            Logger.TraceLog(TraceLogLevel.Warning, "could not find "+newState+" Animation State");
        }
    }
    public void Update()
    {
        foreach(AnimationState animState in _currentStates.Values)
        {
			Play(animState.animationData);
            animState.Update(this);
        }
    } 

    public void DrawUpdate()
    {
        foreach(AnimationState animState in _currentStates.Values)
        {

            animState.DrawUpdate(this);
        }
    }

    public void LateUpdate()
    {
        foreach(AnimationState animState in _currentStates.Values)
        {
            animState.LateUpdate(this);
        }
    }

	public void FlipSprite(AnimationData animationData, bool bHorizontalFlip, bool bVerticalFlip)
	{
		animationData.bFlipH = bHorizontalFlip;
		animationData.bFlipV = !animationData.bFlipV;

		if (bHorizontalFlip && bVerticalFlip)
		{
			animationData.FrameRec.Width *= -1;
			animationData.FrameRec.Height *= -1;
		}
		else if (bHorizontalFlip)
		{
			animationData.FrameRec.Width *= -1;
		}
		else if (bVerticalFlip)
		{
			animationData.FrameRec.Height *= -1;
		}
	}

	public void PlaySequence(AnimationData animationData)
	{
		GoToFrame(animationData, animationData.FramesToPlay[animationData.CurrentFrame]);
		animationData.CurrentFrame++;
		if(animationData.CurrentFrame >= animationData.FramesToPlay.Count())
		{
			animationData.CurrentFrame = 0;
		}
	}

	public void SetLooping(AnimationData animationData, bool bLooping)
	{
		animationData.bCanLoop = bLooping;
	}

	public void SetContinuous(AnimationData animationData, bool bIsContinuous)
	{
		animationData.bContinuous = bIsContinuous;
	}

	public void ResetFrameRec(AnimationData animationData)
	{
		animationData.FrameRec.Width = animationData.bFlipH ? -animationData.Sprite.Width / animationData.Columns : animationData.Sprite.Width / animationData.Columns;
		animationData.FrameRec.Height = animationData.bFlipV ? -animationData.Sprite.Height / animationData.Rows : animationData.Sprite.Height / animationData.Rows;
		animationData.FrameWidth = animationData.FrameRec.Width;
		animationData.FrameHeight = animationData.FrameRec.Height;
		animationData.FrameRec.X = animationData.bReverse && animationData.bContinuous ? animationData.Sprite.Width - animationData.FrameWidth : 0;
		animationData.FrameRec.Y = animationData.bReverse && animationData.bContinuous ? animationData.Sprite.Height - animationData.FrameHeight : 0;

		animationData.CurrentFrame = animationData.bReverse ? animationData.Columns - 1 : 0;
		animationData.CurrentRow = animationData.bReverse ? animationData.Rows - 1 : 0;
		animationData.CurrentColumn = animationData.bReverse ? animationData.Columns - 1 : 0;
	}

	public void GoToRow(AnimationData animationData, int RowNumber)
	{
		if (RowNumber >= animationData.Rows)
		{
			animationData.FrameRec.Y = (animationData.Rows - 1) * animationData.FrameHeight;
			animationData.CurrentRow = animationData.Rows - 1;
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData);
		}
		else if (animationData.Rows >= 1)
		{
			animationData.FrameRec.Y = RowNumber == 0 ? 0 : RowNumber * animationData.FrameHeight;
			animationData.CurrentRow = RowNumber;
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - (RowNumber * animationData.Columns + animationData.Columns);
		}
	}

	public void GoToColumn(AnimationData animationData, int ColumnNumber)
	{
		if (ColumnNumber >= animationData.Columns)
		{
			animationData.FrameRec.X = (animationData.Columns - 1) * animationData.FrameWidth;
			animationData.CurrentColumn = animationData.Columns - 1;
			animationData.CurrentFrame = animationData.Columns - 1;
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - animationData.CurrentRow * animationData.Columns;
		}
		else if (animationData.Columns >= 1)
		{
			animationData.FrameRec.X = ColumnNumber == 0 ? 0 : ColumnNumber * animationData.FrameWidth;
			animationData.CurrentColumn = ColumnNumber;
			animationData.CurrentFrame = ColumnNumber;
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - animationData.CurrentRow * animationData.Columns - ColumnNumber;
		}
	}

	public void GoToFirstRow(AnimationData animationData)
	{
		GoToRow(animationData, 0);

		// Update time remaining
		animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - animationData.CurrentColumn;
	}

	public void GoToFirstColumn(AnimationData animationData)
	{
		if (!animationData.bIsAnimationFinished)
		{
			GoToColumn(animationData, 0);

			// Update time remaining
			if (animationData.bContinuous)
				animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - animationData.Columns * animationData.CurrentRow;
			else
				animationData.TimeRemainingFramesCounter = animationData.Columns;
		}
		else
		{
			GoToColumn(animationData, 0);
			animationData.TimeRemainingFramesCounter = animationData.bContinuous ? GetTotalTimeInFrames(animationData) - GetTotalTimeInFrames(animationData) / animationData.Rows * animationData.CurrentRow : 0;
		}
	}

	public void GoToLastRow(AnimationData animationData)
	{
		GoToRow(animationData, animationData.Rows - 1);

		// Update time remaining
		if (animationData.bContinuous)
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - animationData.CurrentColumn - animationData.Columns * (animationData.Rows - 1); 
		else
			animationData.TimeRemainingFramesCounter = animationData.Columns - animationData.CurrentColumn;
	}

	public void GoToLastColumn(AnimationData animationData)
	{
		if (!animationData.bIsAnimationFinished)
		{
			GoToColumn(animationData, animationData.Columns - 1);

			// Update time remaining
			if (animationData.bContinuous)
			{
				if (!animationData.bReverse)
				{
					if (animationData.Columns * animationData.CurrentRow != 0)
						animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - animationData.Columns * animationData.CurrentRow + animationData.Columns;
					else
						animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - animationData.Columns;
				}
				else
				{
					animationData.TimeRemainingFramesCounter = animationData.Columns * animationData.CurrentRow + animationData.Columns;
				}

			}
			else
				animationData.TimeRemainingFramesCounter = animationData.bIsAnimationFinished ? 0.0f : animationData.Columns;
		}
		else
		{
			GoToColumn(animationData, animationData.Columns - 1);
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - animationData.CurrentRow * animationData.Columns - animationData.Columns;
		}
	}

	public void GoToFrame(AnimationData animationData, int FrameNumber)
	{
		// Does frame exist in sprite-sheet
		if (FrameNumber < animationData.Columns * animationData.Rows)
		{
			GoToRow(animationData, FrameNumber / animationData.Columns);
			GoToColumn(animationData, FrameNumber % animationData.Columns);
		}
		else
		{
			Console.WriteLine("ERROR from GoToFrame(): Frame %u does not exist! %s sprite has frames from %u to %u.\n", FrameNumber, animationData.Name, 0, animationData.Rows * animationData.Columns - 1);
		}
	}

	public void GoToFirstFrame(AnimationData animationData)
	{
		GoToColumn(animationData, 0);
	}

	public void GoToLastFrame(AnimationData animationData)
	{
		GoToColumn(animationData, animationData.Columns - 1);
	}

	public void GoToFirstFrameOfSpriteSheet(AnimationData animationData)
	{
		GoToRow(animationData, 0);
		GoToColumn(animationData, 0);
	}

	public void GoToLastFrameOfSpriteSheet(AnimationData animationData)
	{
		GoToRow(animationData, animationData.Rows - 1);
		GoToColumn(animationData, animationData.Columns - 1);
	}

	public void NextFrame(AnimationData animationData)
	{
		// Only increment when animation is playing
		if (!animationData.bIsAnimationFinished)
		{
			animationData.CurrentFrame++;
			animationData.CurrentColumn++;
		}

		if (animationData.bCanLoop)
		{
			// Are we over the max columns
			if (animationData.CurrentFrame > animationData.Columns - 1)
			{
				// If we are continuous, Go to the next row in the sprite-sheet
				if (animationData.bContinuous)
				{
					NextRow(animationData);
					GoToFirstColumn(animationData);
				}
				// Otherwise, Go back to the start
				else
				{
					GoToFirstColumn(animationData);
				}
			}
		}
		else
		{
			// Are we over the max columns
			if (animationData.CurrentFrame > animationData.Columns - 1)
			{
				// If we are continuous, Go to the next row in the sprite-sheet
				if (animationData.bContinuous)
				{
					// Clamp values back down
					animationData.CurrentFrame = animationData.Columns - 1;
					animationData.CurrentColumn = animationData.Columns - 1;

					// Go to next row if we are not at the last frame
					if (!IsAtLastFrame(animationData))
					{
						NextRow(animationData);
						GoToFirstColumn(animationData);
					}
					else
					{
						animationData.bIsAnimationFinished = true;
					}

				}
				// Otherwise, Stay at the end
				else
				{
					animationData.bIsAnimationFinished = true;
					GoToLastColumn(animationData);
				}
			}
		}
	}

	public void NextFrameInAnimation(AnimationData animationData)
	{
		// Only increment when animation is playing
		if (!animationData.bIsAnimationFinished)
		{
			animationData.CurrentFrame++;
			animationData.CurrentColumn++;
		}

		if (animationData.bCanLoop)
		{
			// Are we over the max columns
			if (animationData.CurrentFrame > animationData.Columns - 1)
			{
				// If we are continuous, Go to the next row in the sprite-sheet
				if (animationData.bContinuous)
				{
					NextRow(animationData);
					GoToFirstColumn(animationData);
				}
				// Otherwise, Go back to the start
				else
				{
					GoToFirstColumn(animationData);
				}
			}
		}
		else
		{
			// Are we over the max columns
			if (animationData.CurrentFrame > animationData.Columns - 1)
			{
				// If we are continuous, Go to the next row in the sprite-sheet
				if (animationData.bContinuous)
				{
					// Clamp values back down
					animationData.CurrentFrame = animationData.Columns - 1;
					animationData.CurrentColumn = animationData.Columns - 1;

					// Go to next row if we are not at the last frame
					if (!IsAtLastFrame(animationData))
					{
						NextRow(animationData);
						GoToFirstColumn(animationData);
					}
					else
						animationData.bIsAnimationFinished = true;

				}
				// Otherwise, Stay at the end
				else
				{
					animationData.bIsAnimationFinished = true;
					GoToLastColumn(animationData);
				}
			}
		}
	}

	public void PreviousFrame(AnimationData animationData)
	{
		// Only decrement when animation is playing
		if (!animationData.bIsAnimationFinished)
		{
			animationData.CurrentFrame--;
			animationData.CurrentColumn--;
		}

		if (animationData.bCanLoop)
		{
			// Are we over the max columns OR equal to zero
			if (animationData.CurrentFrame == 0 || animationData.CurrentFrame > animationData.Columns)
			{
				// If we are continuous, Go to the previous row in the sprite-sheet
				if (animationData.bContinuous)
				{
					PreviousRow(animationData);
					GoToLastColumn(animationData);
				}
				// Otherwise, Go back to the last column
				else
				{
					GoToLastColumn(animationData);
				}
			}
		}
		else
		{
			// Are we over the max columns OR equal to zero
			if (animationData.CurrentFrame == 0 || animationData.CurrentFrame > animationData.Columns)
			{
				// If we are continuous, Go to the previous row in the sprite-sheet
				if (animationData.bContinuous)
				{
					// Clamp values back down
					animationData.CurrentFrame = 0;
					animationData.CurrentColumn = 0;

					// Go to previous row if we are not at the first frame
					if (!IsAtFirstFrame(animationData))
					{
						PreviousRow(animationData);
						GoToLastColumn(animationData);
					}
					else
						animationData.bIsAnimationFinished = true;
				}
				// Otherwise, Stay at the start
				else
				{
					animationData.bIsAnimationFinished = true;
					GoToFirstColumn(animationData);
				}
			}
		}
	}

	private float Lerp(float Start, float End, float Alpha)
	{
		return (1.0f - Alpha) * Start + Alpha * End;
	}

	int IntLerp(int a, int b, float t)
	{
		if (t > 0.9999f)
		{
			return b;
		}

		return a + (int)(((float)b - (float)a) * t);
	}

	public void NextRow(AnimationData animationData)
	{
		animationData.FrameRec.Y += animationData.FrameHeight;

		if (animationData.FrameRec.Y >= animationData.Sprite.Height)
		{
			// Go to start
			if (animationData.bCanLoop)
			{
				animationData.FrameRec.Y = 0;
				animationData.CurrentRow = 0;
			}
			// Stay at end
			else
			{
				animationData.FrameRec.Y = animationData.Sprite.Height;
				animationData.CurrentRow = animationData.Rows - 1;
			}

			ResetTimer(animationData);
		}
		else
			animationData.CurrentRow++;

		// Update the time remaining
		animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - GetTotalTimeInFrames(animationData) / animationData.Rows * animationData.CurrentRow;
	}

	public void PreviousRow(AnimationData animationData)
	{
		animationData.FrameRec.Y -= animationData.FrameHeight;

		if (animationData.FrameRec.Y < 0)
		{
			animationData.FrameRec.Y = animationData.Sprite.Height - animationData.FrameHeight;
			animationData.CurrentRow = animationData.Rows - 1;
			ResetTimer(animationData);
		}
		else
			animationData.CurrentRow--;

		// Update the time remaining
		if (!animationData.bReverse)
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames(animationData) - GetTotalTimeInFrames(animationData) / animationData.Rows * animationData.CurrentRow;
	}

	public void NextColumn(AnimationData animationData)
	{
		animationData.FrameRec.X += animationData.FrameWidth;

		if (animationData.FrameRec.X > animationData.Sprite.Width)
		{
			animationData.FrameRec.X = 0;
			animationData.CurrentColumn = 0;
		}
		else
			animationData.CurrentColumn++;

		// Update the time remaining
		animationData.TimeRemainingFramesCounter -= 1;
	}

	public void PreviousColumn(AnimationData animationData)
	{
		animationData.FrameRec.X -= animationData.FrameWidth;

		if (animationData.FrameRec.X < 0)
		{
			animationData.FrameRec.X = animationData.Sprite.Width - animationData.FrameWidth;
			animationData.CurrentColumn = animationData.Columns - 1;
		}
		else
			animationData.CurrentColumn--;

		// Update the time remaining
		animationData.TimeRemainingFramesCounter += 1;
	}

	public void Forward(AnimationData animationData)
	{
		if (animationData.bReverse)
			animationData.bReverse = false;
	}

	public void Reverse(AnimationData animationData, bool bToggle)
	{
		if (bToggle)
		{
			animationData.bReverse = !animationData.bReverse;
			animationData.TimeRemainingFramesCounter += GetTotalTimeInFrames(animationData) - animationData.TimeRemainingFramesCounter * 2;
			animationData.bIsAnimationFinished = false;
		}
		else
		{
			animationData.bReverse = true;
			animationData.TimeRemainingFramesCounter += GetTotalTimeInFrames(animationData) - animationData.TimeRemainingFramesCounter * 2;
			animationData.bIsAnimationFinished = false;
		}
	}

	public void Restart(AnimationData animationData)
	{
		ResetFrameRec(animationData);
		ResetTimer(animationData);
		animationData.bHasStartedPlaying = true;
	}

	public int GetTotalFrames(AnimationData animationData)
	{
		return animationData.Rows * animationData.Columns;
	}

	public int GetTotalRows(AnimationData animationData)
	{
		return animationData.Rows;
	}

	public int GetTotalColumns(AnimationData animationData)
	{
		return animationData.Columns;
	}

	public int GetCurrentFrame(AnimationData animationData)
	{
		return animationData.CurrentRow * animationData.Columns + animationData.CurrentColumn;
	}

	public int GetCurrentRow(AnimationData animationData)
	{
		return animationData.CurrentRow;
	}

	public int GetCurrentColumn(AnimationData animationData)
	{
		return animationData.CurrentColumn;
	}

	public int GetTotalTimeInFrames(AnimationData animationData)
	{
		return animationData.bContinuous ? animationData.Columns * animationData.Rows : animationData.Columns;
	}

	public float GetTotalTimeInSeconds(AnimationData animationData)
	{
		return animationData.bContinuous ? animationData.Columns * animationData.Rows / animationData.Framerate : animationData.Columns / animationData.Framerate;
	}

	public float GetTimeRemainingInFrames(AnimationData animationData)
	{
		return animationData.TimeRemainingFramesCounter;
	}

	public float GetTimeRemainingInSeconds(AnimationData animationData)
	{
		return animationData.TimeRemainingFramesCounter; // Framerate;
	}

	public string GetName(AnimationData animationData)
	{
		return animationData.Name;
	}

	private void CountdownInFrames(AnimationData animationData)
	{
		if (animationData.TimeRemainingFramesCounter != 0.0f)
			animationData.TimeRemainingFramesCounter -= Time.GetFrameTime() < 0.01f ? animationData.Framerate * Time.GetFrameTime() : 0.0f;

		if (animationData.TimeRemainingFramesCounter <= 0.0f)
			animationData.TimeRemainingFramesCounter = 0.0f;
	}

	public void Play(AnimationData animationData)
	{
		if (!animationData.bPaused)
		{
			animationData.PlaybackPosition++;

			// Update the time remaining
			if (!animationData.bIsAnimationFinished)
				CountdownInFrames(animationData);

			// Has 'X' amount of frames passed?
			if (animationData.PlaybackPosition > Time.GetFPS() / animationData.Framerate)
			{
				// Reset playback position
				animationData.PlaybackPosition = 0;

				// Go to previous frame when reversing
				if (animationData.bReverse)
				{
					PreviousFrame(animationData);
					// Go to next frame if not reversing
				}
				else
				{
					NextFrame(animationData);
				}
			}

			// Only go to next frame if animation has not finished playing
			if (!animationData.bIsAnimationFinished)
			{
				animationData.FrameRec.X = animationData.CurrentFrame * animationData.FrameWidth;
			}

			animationData.bHasStartedPlaying = false;
		}
	}

	private void LerpAnim(AnimationData animationData, float Speed, bool bConstant)
	{
		animationData.PlaybackPosition++;
		if (animationData.PlaybackPosition > Time.GetFPS() / animationData.Framerate)
		{
			animationData.PlaybackPosition = 0;

			if (bConstant)
			{
				animationData.FrameRec.X = (int)(animationData.FrameRec.X + Speed * Time.GetFrameTime());
			}
			else
			{
				animationData.FrameRec.X = Lerp(animationData.FrameRec.X, animationData.Sprite.Width, Speed * Time.GetFrameTime());
			}
		}
	}

	public void Start(AnimationData animationData)
	{
		UnPause(animationData);

		if (!animationData.bHasStartedPlaying)
		{
			animationData.bHasStartedPlaying = true;
		}
	}

	public void Stop(AnimationData animationData)
	{
		animationData.PlaybackPosition = 0;
		animationData.CurrentColumn = 0;
		animationData.CurrentFrame = 0;
		animationData.CurrentRow = 0;
		animationData.bHasStartedPlaying = true;
		animationData.bIsAnimationFinished = true;

		ResetFrameRec(animationData);
		ResetTimer(animationData);
		Pause(animationData, animationData.bPaused);
	}

	public void UnPause(AnimationData animationData)
	{
		animationData.bPaused = false;
		animationData.bHasStartedPlaying = true;
	}

	public void Pause(AnimationData animationData, bool bToggle)
	{
		if (bToggle)
		{
			animationData.bPaused = !animationData.bPaused;
			animationData.bHasStartedPlaying = !animationData.bPaused;
		}
		else
		{
			animationData.bPaused = true;
			animationData.bHasStartedPlaying = false;
		}
	}

	public void SetFramerate(AnimationData animationData, int NewFramerate)
	{
		animationData.Framerate = NewFramerate;
	}

	public bool IsAtFrame(AnimationData animationData, int FrameNumber)
	{
		// Does frame exist in sprite-sheet
		if (FrameNumber < animationData.Columns * animationData.Rows)
		{
			int RowFrameNumberIsOn = FrameNumber / animationData.Columns;
			int ColumnFrameNumberIsOn = FrameNumber % animationData.Columns;

			return IsAtRow(animationData, RowFrameNumberIsOn) && IsAtColumn(animationData, ColumnFrameNumberIsOn);
		}

		Console.WriteLine("ERROR from IsAtFrame(): Frame %u does not exist! %s sprite has frames from %u to %u.\n", FrameNumber, animationData.Name, 0, animationData.Rows * animationData.Columns - 1);
		return false;
	}

	public bool IsAtRow(AnimationData animationData, int RowNumber)
	{
		if (RowNumber < animationData.Rows)
			return RowNumber == animationData.CurrentRow;

		Console.WriteLine("ERROR from IsAtRow(): Row does not exist!\n");
		return false;
	}

	public bool IsAtColumn(AnimationData animationData, int ColumnNumber)
	{
		if (ColumnNumber < animationData.Columns)
		{
			return ColumnNumber == animationData.CurrentColumn;
		}

		Console.WriteLine("ERROR from IsAtColumn(): Column does not exist!\n");
		return false;
	}

	public bool IsAtFirstFrameOfSpriteSheet(AnimationData animationData)
	{
		return IsAtFirstRow(animationData) && IsAtFirstColumn(animationData);
	}

	public bool IsAtLastFrameOfSpriteSheet(AnimationData animationData)
	{
		return IsAtLastRow(animationData) && IsAtLastColumn(animationData);
	}

	public bool IsAtFirstRow(AnimationData animationData)
	{
		return animationData.CurrentRow == 0;
	}

	public bool IsAtFirstColumn(AnimationData animationData)
	{
		return animationData.CurrentColumn == 0;
	}

	public bool IsAtFirstFrame(AnimationData animationData)
	{
		return animationData.bContinuous ? IsAtFirstRow(animationData) && IsAtFirstColumn(animationData) : IsAtFirstColumn(animationData);
	}

	public bool IsAtLastFrame(AnimationData animationData)
	{
		return animationData.bContinuous ? IsAtLastRow(animationData) && IsAtLastColumn(animationData) : IsAtLastColumn(animationData);
	}

	private void ResetTimer(AnimationData animationData)
	{
		animationData.TimeRemainingFramesCounter = (float)(GetTotalTimeInFrames(animationData));
	}

	public Rectangle GetFrameRec(AnimationData animationData)
	{
		return animationData.FrameRec;
	}

	public Texture2D GetSprite(AnimationData animationData)
	{
		return animationData.Sprite;
	}

	public bool IsAtLastRow(AnimationData animationData)
	{
		return animationData.CurrentRow == animationData.Rows - 1;
	}

	public bool IsAtLastColumn(AnimationData animationData)
	{
		return animationData.CurrentColumn == animationData.Columns - 1;
	}

	public bool IsStartedPlaying(AnimationData animationData)
	{
		if (IsAtFirstFrame(animationData))
		{
			ResetTimer(animationData);
			return true;
		}

		return animationData.bHasStartedPlaying;
	}

	public bool IsFinishedPlaying(AnimationData animationData)
	{
		if (IsAtLastFrame(animationData))
		{
			ResetTimer(animationData);
			return true;
		}

		if (!animationData.bCanLoop)
			return animationData.bIsAnimationFinished;

		return animationData.bIsAnimationFinished;
	}

	public bool IsPlaying(AnimationData animationData)
	{
		if (animationData.bCanLoop)
		{
			return !animationData.bPaused;
		}

		if (!animationData.bCanLoop && animationData.bContinuous)
		{
			return !animationData.bIsAnimationFinished;
		}

		return !animationData.bIsAnimationFinished;
	}

    public override void Initialize()
    {
        
    }

    public override void Shutdown()
    {
        
    }
}