
using Raylib_CSharp.Textures;
using Raylib_CSharp;
using Raylib_CSharp.Transformations;
using System.Diagnostics.CodeAnalysis;

namespace Engine.Systems.ASM;
public class Animator
{
	[AllowNull] AnimationData animationData = null;


	public Animator() 
	{	

	}

	public void AssignAnimationData(AnimationData animationData)
	{
		this.animationData = animationData;
		Restart();
	}

	public void FlipSprite(bool bHorizontalFlip, bool bVerticalFlip)
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

	public void SetLooping(bool bLooping)
	{
		animationData.bCanLoop = bLooping;
	}

	public void SetContinuous(bool bIsContinuous)
	{
		animationData.bContinuous = bIsContinuous;
	}

	public void ResetFrameRec()
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

	public void GoToRow(int RowNumber)
	{
		if (RowNumber >= animationData.Rows)
		{
			animationData.FrameRec.Y = (animationData.Rows - 1) * animationData.FrameHeight;
			animationData.CurrentRow = animationData.Rows - 1;
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames();
		}
		else if (animationData.Rows >= 1)
		{
			animationData.FrameRec.Y = RowNumber == 0 ? 0 : RowNumber * animationData.FrameHeight;
			animationData.CurrentRow = RowNumber;
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - (RowNumber * animationData.Columns + animationData.Columns);
		}
	}

	public void GoToColumn(int ColumnNumber)
	{
		if (ColumnNumber >= animationData.Columns)
		{
			animationData.FrameRec.X = (animationData.Columns - 1) * animationData.FrameWidth;
			animationData.CurrentColumn = animationData.Columns - 1;
			animationData.CurrentFrame = animationData.Columns - 1;
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - animationData.CurrentRow * animationData.Columns;
		}
		else if (animationData.Columns >= 1)
		{
			animationData.FrameRec.X = ColumnNumber == 0 ? 0 : ColumnNumber * animationData.FrameWidth;
			animationData.CurrentColumn = ColumnNumber;
			animationData.CurrentFrame = ColumnNumber;
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - animationData.CurrentRow * animationData.Columns - ColumnNumber;
		}
	}

	public void GoToFirstRow()
	{
		GoToRow(0);

		// Update time remaining
		animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - animationData.CurrentColumn;
	}

	public void GoToFirstColumn()
	{
		if (!animationData.bIsAnimationFinished)
		{
			GoToColumn(0);

			// Update time remaining
			if (animationData.bContinuous)
				animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - animationData.Columns * animationData.CurrentRow;
			else
				animationData.TimeRemainingFramesCounter = animationData.Columns;
		}
		else
		{
			GoToColumn(0);
			animationData.TimeRemainingFramesCounter = animationData.bContinuous ? GetTotalTimeInFrames() - GetTotalTimeInFrames() / animationData.Rows * animationData.CurrentRow : 0;
		}
	}

	public void GoToLastRow()
	{
		GoToRow(animationData.Rows - 1);

		// Update time remaining
		if (animationData.bContinuous)
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - animationData.CurrentColumn - animationData.Columns * (animationData.Rows - 1); 
		else
			animationData.TimeRemainingFramesCounter = animationData.Columns - animationData.CurrentColumn;
	}

	public void GoToLastColumn()
	{
		if (!animationData.bIsAnimationFinished)
		{
			GoToColumn(animationData.Columns - 1);

			// Update time remaining
			if (animationData.bContinuous)
			{
				if (!animationData.bReverse)
				{
					if (animationData.Columns * animationData.CurrentRow != 0)
						animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - animationData.Columns * animationData.CurrentRow + animationData.Columns;
					else
						animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - animationData.Columns;
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
			GoToColumn(animationData.Columns - 1);
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - animationData.CurrentRow * animationData.Columns - animationData.Columns;
		}
	}

	public void GoToFrame(int FrameNumber)
	{
		// Does frame exist in sprite-sheet
		if (FrameNumber < animationData.Columns * animationData.Rows)
		{
			GoToRow(FrameNumber / animationData.Columns);
			GoToColumn(FrameNumber % animationData.Columns);
		}
		else
			Console.WriteLine("ERROR from GoToFrame(): Frame %u does not exist! %s sprite has frames from %u to %u.\n", FrameNumber, animationData.Name, 0, animationData.Rows * animationData.Columns - 1);
	}

	public void GoToFirstFrame()
	{
		GoToColumn(0);
	}

	public void GoToLastFrame()
	{
		GoToColumn(animationData.Columns - 1);
	}

	public void GoToFirstFrameOfSpriteSheet()
	{
		GoToRow(0);
		GoToColumn(0);
	}

	public void GoToLastFrameOfSpriteSheet()
	{
		GoToRow(animationData.Rows - 1);
		GoToColumn(animationData.Columns - 1);
	}

	public void NextFrame()
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
					NextRow();
					GoToFirstColumn();
				}
				// Otherwise, Go back to the start
				else
				{
					GoToFirstColumn();
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
					if (!IsAtLastFrame())
					{
						NextRow();
						GoToFirstColumn();
					}
					else
						animationData.bIsAnimationFinished = true;

				}
				// Otherwise, Stay at the end
				else
				{
					animationData.bIsAnimationFinished = true;
					GoToLastColumn();
				}
			}
		}
	}

	public void PreviousFrame()
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
					PreviousRow();
					GoToLastColumn();
				}
				// Otherwise, Go back to the last column
				else
				{
					GoToLastColumn();
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
					if (!IsAtFirstFrame())
					{
						PreviousRow();
						GoToLastColumn();
					}
					else
						animationData.bIsAnimationFinished = true;
				}
				// Otherwise, Stay at the start
				else
				{
					animationData.bIsAnimationFinished = true;
					GoToFirstColumn();
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

	public void NextRow()
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

			ResetTimer();
		}
		else
			animationData.CurrentRow++;

		// Update the time remaining
		animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - GetTotalTimeInFrames() / animationData.Rows * animationData.CurrentRow;
	}

	public void PreviousRow()
	{
		animationData.FrameRec.Y -= animationData.FrameHeight;

		if (animationData.FrameRec.Y < 0)
		{
			animationData.FrameRec.Y = animationData.Sprite.Height - animationData.FrameHeight;
			animationData.CurrentRow = animationData.Rows - 1;
			ResetTimer();
		}
		else
			animationData.CurrentRow--;

		// Update the time remaining
		if (!animationData.bReverse)
			animationData.TimeRemainingFramesCounter = GetTotalTimeInFrames() - GetTotalTimeInFrames() / animationData.Rows * animationData.CurrentRow;
	}

	public void NextColumn()
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

	public void PreviousColumn()
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

	public void Forward()
	{
		if (animationData.bReverse)
			animationData.bReverse = false;
	}

	public void Reverse(bool bToggle)
	{
		if (bToggle)
		{
			animationData.bReverse = !animationData.bReverse;
			animationData.TimeRemainingFramesCounter += GetTotalTimeInFrames() - animationData.TimeRemainingFramesCounter * 2;
			animationData.bIsAnimationFinished = false;
		}
		else
		{
			animationData.bReverse = true;
			animationData.TimeRemainingFramesCounter += GetTotalTimeInFrames() - animationData.TimeRemainingFramesCounter * 2;
			animationData.bIsAnimationFinished = false;
		}
	}

	public void Restart()
	{
		ResetFrameRec();
		ResetTimer();
		animationData.bHasStartedPlaying = true;
	}

	public int GetTotalFrames()
	{
		return animationData.Rows * animationData.Columns;
	}

	public int GetTotalRows()
	{
		return animationData.Rows;
	}

	public int GetTotalColumns()
	{
		return animationData.Columns;
	}

	public int GetCurrentFrame()
	{
		return animationData.CurrentRow * animationData.Columns + animationData.CurrentColumn;
	}

	public int GetCurrentRow()
	{
		return animationData.CurrentRow;
	}

	public int GetCurrentColumn()
	{
		return animationData.CurrentColumn;
	}

	public int GetTotalTimeInFrames()
	{
		return animationData.bContinuous ? animationData.Columns * animationData.Rows : animationData.Columns;
	}

	public float GetTotalTimeInSeconds()
	{
		return animationData.bContinuous ? animationData.Columns * animationData.Rows / animationData.Framerate : animationData.Columns / animationData.Framerate;
	}

	public float GetTimeRemainingInFrames()
	{
		return animationData.TimeRemainingFramesCounter;
	}

	public float GetTimeRemainingInSeconds()
	{
		return animationData.TimeRemainingFramesCounter; // Framerate;
	}

	public string GetName()
	{
		return animationData.Name;
	}

	private void CountdownInFrames()
	{
		if (animationData.TimeRemainingFramesCounter != 0.0f)
			animationData.TimeRemainingFramesCounter -= Time.GetFrameTime() < 0.01f ? animationData.Framerate * Time.GetFrameTime() : 0.0f;

		if (animationData.TimeRemainingFramesCounter <= 0.0f)
			animationData.TimeRemainingFramesCounter = 0.0f;
	}

	public void Play()
	{
		if (!animationData.bPaused)
		{
			animationData.PlaybackPosition++;

			// Update the time remaining
			if (!animationData.bIsAnimationFinished)
				CountdownInFrames();

			// Has 'X' amount of frames passed?
			if (animationData.PlaybackPosition > Time.GetFPS() / animationData.Framerate)
			{
				// Reset playback position
				animationData.PlaybackPosition = 0;

				// Go to previous frame when reversing
				if (animationData.bReverse)
					PreviousFrame();
				// Go to next frame if not reversing
				else
					NextFrame();
			}

			// Only go to next frame if animation has not finished playing
			if (!animationData.bIsAnimationFinished)
				animationData.FrameRec.X = animationData.CurrentFrame * animationData.FrameWidth;

			animationData.bHasStartedPlaying = false;
		}
	}

	private void LerpAnim(float Speed, bool bConstant)
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

	public void Start()
	{
		UnPause();

		if (!animationData.bHasStartedPlaying)
		{
			animationData.bHasStartedPlaying = true;
		}
	}

	public void Stop()
	{
		animationData.PlaybackPosition = 0;
		animationData.CurrentColumn = 0;
		animationData.CurrentFrame = 0;
		animationData.CurrentRow = 0;
		animationData.bHasStartedPlaying = true;
		animationData.bIsAnimationFinished = true;

		ResetFrameRec();
		ResetTimer();
		Pause(animationData.bPaused);
	}

	public void UnPause()
	{
		animationData.bPaused = false;
		animationData.bHasStartedPlaying = true;
	}

	public void Pause(bool bToggle)
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

	public void SetFramerate(int NewFramerate)
	{
		animationData.Framerate = NewFramerate;
	}

	public bool IsAtFrame(int FrameNumber)
	{
		// Does frame exist in sprite-sheet
		if (FrameNumber < animationData.Columns * animationData.Rows)
		{
			int RowFrameNumberIsOn = FrameNumber / animationData.Columns;
			int ColumnFrameNumberIsOn = FrameNumber % animationData.Columns;

			return IsAtRow(RowFrameNumberIsOn) && IsAtColumn(ColumnFrameNumberIsOn);
		}

		Console.WriteLine("ERROR from IsAtFrame(): Frame %u does not exist! %s sprite has frames from %u to %u.\n", FrameNumber, animationData.Name, 0, animationData.Rows * animationData.Columns - 1);
		return false;
	}

	public bool IsAtRow(int RowNumber)
	{
		if (RowNumber < animationData.Rows)
			return RowNumber == animationData.CurrentRow;

		Console.WriteLine("ERROR from IsAtRow(): Row does not exist!\n");
		return false;
	}

	public bool IsAtColumn(int ColumnNumber)
	{
		if (ColumnNumber < animationData.Columns)
		{
			return ColumnNumber == animationData.CurrentColumn;
		}

		Console.WriteLine("ERROR from IsAtColumn(): Column does not exist!\n");
		return false;
	}

	public bool IsAtFirstFrameOfSpriteSheet()
	{
		return IsAtFirstRow() && IsAtFirstColumn();
	}

	public bool IsAtLastFrameOfSpriteSheet()
	{
		return IsAtLastRow() && IsAtLastColumn();
	}

	public bool IsAtFirstRow()
	{
		return animationData.CurrentRow == 0;
	}

	public bool IsAtFirstColumn()
	{
		return animationData.CurrentColumn == 0;
	}

	public bool IsAtFirstFrame()
	{
		return animationData.bContinuous ? IsAtFirstRow() && IsAtFirstColumn() : IsAtFirstColumn();
	}

	public bool IsAtLastFrame()
	{
		return animationData.bContinuous ? IsAtLastRow() && IsAtLastColumn() : IsAtLastColumn();
	}

	private void ResetTimer()
	{
		animationData.TimeRemainingFramesCounter = (float)(GetTotalTimeInFrames());
	}

	public Rectangle GetFrameRec()
	{
		return animationData.FrameRec;
	}

	public Texture2D GetSprite()
	{
		return animationData.Sprite;
	}

	public bool IsAtLastRow()
	{
		return animationData.CurrentRow == animationData.Rows - 1;
	}

	public bool IsAtLastColumn()
	{
		return animationData.CurrentColumn == animationData.Columns - 1;
	}

	public bool IsStartedPlaying()
	{
		if (IsAtFirstFrame())
		{
			ResetTimer();
			return true;
		}

		return animationData.bHasStartedPlaying;
	}

	public bool IsFinishedPlaying()
	{
		if (IsAtLastFrame())
		{
			ResetTimer();
			return true;
		}

		if (!animationData.bCanLoop)
			return animationData.bIsAnimationFinished;

		return animationData.bIsAnimationFinished;
	}

	public bool IsPlaying()
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
}
