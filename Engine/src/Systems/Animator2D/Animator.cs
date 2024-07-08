
using Raylib_CSharp.Textures;
using Raylib_CSharp;
using Raylib_CSharp.Transformations;
using Engine.Systems.FSM;

namespace Engine.Systems;
public class Animator
{
	AnimationData animationData;


	public Animator(AnimationData animationData)
	{
		this.animationData = animationData;
	}

	public void AssignAnimationData(AnimationData animationData)
	{
		this.animationData = animationData;

		Restart();
	}

	/*public void ChangeSprite(Texture2D NewSprite, int NumOfFramesPerRow, int NumOfRows, int Speed, float DelayInSeconds, bool bPlayInReverse, bool bContinuous, bool bLooping)
	{
		animationData.DelayFramesCounter++;

		if (Time.GetFPS() >= 0)
		{
			if (animationData.DelayFramesCounter > DelayInSeconds * Time.GetFPS())
			{
				animationData.Rows = NumOfRows == 0 ? 1 : NumOfRows;
				animationData.Columns = NumOfFramesPerRow;
				animationData.Framerate = Speed;
				animationData.bCanLoop = bLooping;
				animationData.bContinuous = bContinuous;
				animationData.bReverse = bPlayInReverse;
				animationData.PlaybackPosition = 0;
				animationData.DelayFramesCounter = 0;
				animationData.bIsAnimationFinished = false;
				animationData.bHasStartedPlaying = !animationData.bPaused;

				//animationData.AssignSprite(NewSprite);
			}
		}
	}*/

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
		GoToRow(Rows - 1);

		// Update time remaining
		if (bContinuous)
			TimeRemainingFramesCounter = GetTotalTimeInFrames() - CurrentColumn - Columns * (Rows - 1); 
		else
			TimeRemainingFramesCounter = Columns - CurrentColumn;
	}

	public void GoToLastColumn()
	{
		if (!bIsAnimationFinished)
		{
			GoToColumn(Columns - 1);

			// Update time remaining
			if (bContinuous)
			{
				if (!bReverse)
				{
					if (Columns * CurrentRow != 0)
						TimeRemainingFramesCounter = GetTotalTimeInFrames() - Columns * CurrentRow + Columns;
					else
						TimeRemainingFramesCounter = GetTotalTimeInFrames() - Columns;
				}
				else
				{
					TimeRemainingFramesCounter = Columns * CurrentRow + Columns;
				}

			}
			else
				TimeRemainingFramesCounter = bIsAnimationFinished ? 0.0f : Columns;
		}
		else
		{
			GoToColumn(Columns - 1);
			TimeRemainingFramesCounter = GetTotalTimeInFrames() - CurrentRow * Columns - Columns;
		}
	}

	public void GoToFrame(int FrameNumber)
	{
		// Does frame exist in sprite-sheet
		if (FrameNumber < Columns * Rows)
		{
			GoToRow(FrameNumber / Columns);
			GoToColumn(FrameNumber % Columns);
		}
		else
			Console.WriteLine("ERROR from GoToFrame(): Frame %u does not exist! %s sprite has frames from %u to %u.\n", FrameNumber, Name, 0, Rows * Columns - 1);
	}

	public void GoToFirstFrame()
	{
		GoToColumn(0);
	}

	public void GoToLastFrame()
	{
		GoToColumn(Columns - 1);
	}

	public void GoToFirstFrameOfSpriteSheet()
	{
		GoToRow(0);
		GoToColumn(0);
	}

	public void GoToLastFrameOfSpriteSheet()
	{
		GoToRow(Rows - 1);
		GoToColumn(Columns - 1);
	}

	public void NextFrame()
	{
		// Only increment when animation is playing
		if (!bIsAnimationFinished)
		{
			CurrentFrame++;
			CurrentColumn++;
		}

		if (bCanLoop)
		{
			// Are we over the max columns
			if (CurrentFrame > Columns - 1)
			{
				// If we are continuous, Go to the next row in the sprite-sheet
				if (bContinuous)
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
			if (CurrentFrame > Columns - 1)
			{
				// If we are continuous, Go to the next row in the sprite-sheet
				if (bContinuous)
				{
					// Clamp values back down
					CurrentFrame = Columns - 1;
					CurrentColumn = Columns - 1;

					// Go to next row if we are not at the last frame
					if (!IsAtLastFrame())
					{
						NextRow();
						GoToFirstColumn();
					}
					else
						bIsAnimationFinished = true;

				}
				// Otherwise, Stay at the end
				else
				{
					bIsAnimationFinished = true;
					GoToLastColumn();
				}
			}
		}
	}

	public void PreviousFrame()
	{
		// Only decrement when animation is playing
		if (!bIsAnimationFinished)
		{
			CurrentFrame--;
			CurrentColumn--;
		}

		if (bCanLoop)
		{
			// Are we over the max columns OR equal to zero
			if (CurrentFrame == 0 || CurrentFrame > Columns)
			{
				// If we are continuous, Go to the previous row in the sprite-sheet
				if (bContinuous)
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
			if (CurrentFrame == 0 || CurrentFrame > Columns)
			{
				// If we are continuous, Go to the previous row in the sprite-sheet
				if (bContinuous)
				{
					// Clamp values back down
					CurrentFrame = 0;
					CurrentColumn = 0;

					// Go to previous row if we are not at the first frame
					if (!IsAtFirstFrame())
					{
						PreviousRow();
						GoToLastColumn();
					}
					else
						bIsAnimationFinished = true;
				}
				// Otherwise, Stay at the start
				else
				{
					bIsAnimationFinished = true;
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
		FrameRec.Y += FrameHeight;

		if (FrameRec.Y >= Sprite.Height)
		{
			// Go to start
			if (bCanLoop)
			{
				FrameRec.Y = 0;
				CurrentRow = 0;
			}
			// Stay at end
			else
			{
				FrameRec.Y = Sprite.Height;
				CurrentRow = Rows - 1;
			}

			ResetTimer();
		}
		else
			CurrentRow++;

		// Update the time remaining
		TimeRemainingFramesCounter = GetTotalTimeInFrames() - GetTotalTimeInFrames() / Rows * CurrentRow;
	}

	public void PreviousRow()
	{
		FrameRec.Y -= FrameHeight;

		if (FrameRec.Y < 0)
		{
			FrameRec.Y = Sprite.Height - FrameHeight;
			CurrentRow = Rows - 1;
			ResetTimer();
		}
		else
			CurrentRow--;

		// Update the time remaining
		if (!bReverse)
			TimeRemainingFramesCounter = GetTotalTimeInFrames() - GetTotalTimeInFrames() / Rows * CurrentRow;
	}

	public void NextColumn()
	{
		FrameRec.X += FrameWidth;

		if (FrameRec.X > Sprite.Width)
		{
			FrameRec.X = 0;
			CurrentColumn = 0;
		}
		else
			CurrentColumn++;

		// Update the time remaining
		TimeRemainingFramesCounter -= 1;
	}

	public void PreviousColumn()
	{
		FrameRec.X -= FrameWidth;

		if (FrameRec.X < 0)
		{
			FrameRec.X = Sprite.Width - FrameWidth;
			CurrentColumn = Columns - 1;
		}
		else
			CurrentColumn--;

		// Update the time remaining
		TimeRemainingFramesCounter += 1;
	}

	public void Forward()
	{
		if (bReverse)
			bReverse = false;
	}

	public void Reverse(bool bToggle)
	{
		if (bToggle)
		{
			bReverse = !bReverse;
			TimeRemainingFramesCounter += GetTotalTimeInFrames() - TimeRemainingFramesCounter * 2;
			bIsAnimationFinished = false;
		}
		else
		{
			bReverse = true;
			TimeRemainingFramesCounter += GetTotalTimeInFrames() - TimeRemainingFramesCounter * 2;
			bIsAnimationFinished = false;
		}
	}

	public void Restart()
	{
		ResetFrameRec();
		ResetTimer();
		bHasStartedPlaying = true;
	}

	public int GetTotalFrames()
	{
		return Rows * Columns;
	}

	public int GetTotalRows()
	{
		return Rows;
	}

	public int GetTotalColumns()
	{
		return Columns;
	}

	public int GetCurrentFrame()
	{
		return CurrentRow * Columns + CurrentColumn;
	}

	public int GetCurrentRow()
	{
		return CurrentRow;
	}

	public int GetCurrentColumn()
	{
		return CurrentColumn;
	}

	public int GetTotalTimeInFrames()
	{
		return bContinuous ? Columns * Rows : Columns;
	}

	public float GetTotalTimeInSeconds()
	{
		return bContinuous ? Columns * Rows / Framerate : Columns / Framerate;
	}

	public float GetTimeRemainingInFrames()
	{
		return TimeRemainingFramesCounter;
	}

	public float GetTimeRemainingInSeconds()
	{
		return TimeRemainingFramesCounter; // Framerate;
	}

	public string GetName()
	{
		return Name;
	}

	private void CountdownInFrames()
	{
		if (TimeRemainingFramesCounter != 0.0f)
			TimeRemainingFramesCounter -= Time.GetFrameTime() < 0.01f ? Framerate * Time.GetFrameTime() : 0.0f;

		if (TimeRemainingFramesCounter <= 0.0f)
			TimeRemainingFramesCounter = 0.0f;
	}

	public void Play()
	{
		if (!bPaused)
		{
			PlaybackPosition++;

			// Update the time remaining
			if (!bIsAnimationFinished)
				CountdownInFrames();

			// Has 'X' amount of frames passed?
			if (PlaybackPosition > Time.GetFPS() / Framerate)
			{
				// Reset playback position
				PlaybackPosition = 0;

				// Go to previous frame when reversing
				if (bReverse)
					PreviousFrame();
				// Go to next frame if not reversing
				else
					NextFrame();
			}

			// Only go to next frame if animation has not finished playing
			if (!bIsAnimationFinished)
				FrameRec.X = CurrentFrame * FrameWidth;

			bHasStartedPlaying = false;
		}
	}

	private void LerpAnim(float Speed, bool bConstant)
	{
		PlaybackPosition++;
		if (PlaybackPosition > Time.GetFPS() / Framerate)
		{
			PlaybackPosition = 0;

			if (bConstant)
			{
				FrameRec.X = (int)(FrameRec.X + Speed * Time.GetFrameTime());
			}
			else
			{
				FrameRec.X = Lerp(FrameRec.X, Sprite.Width, Speed * Time.GetFrameTime());
			}
		}
	}

	public void Start()
	{
		UnPause();

		if (!bHasStartedPlaying)
		{
			bHasStartedPlaying = true;
		}
	}

	public void Stop()
	{
		PlaybackPosition = 0;
		CurrentColumn = 0;
		CurrentFrame = 0;
		CurrentRow = 0;
		bHasStartedPlaying = true;
		bIsAnimationFinished = true;

		ResetFrameRec();
		ResetTimer();
		Pause(bPaused);
	}

	public void UnPause()
	{
		bPaused = false;
		bHasStartedPlaying = true;
	}

	public void Pause(bool bToggle)
	{
		if (bToggle)
		{
			bPaused = !bPaused;
			bHasStartedPlaying = !bPaused;
		}
		else
		{
			bPaused = true;
			bHasStartedPlaying = false;
		}
	}

	public void SetFramerate(int NewFramerate)
	{
		Framerate = NewFramerate;
	}

	public bool IsAtFrame(int FrameNumber)
	{
		// Does frame exist in sprite-sheet
		if (FrameNumber < Columns * Rows)
		{
			int RowFrameNumberIsOn = FrameNumber / Columns;
			int ColumnFrameNumberIsOn = FrameNumber % Columns;

			return IsAtRow(RowFrameNumberIsOn) && IsAtColumn(ColumnFrameNumberIsOn);
		}

		Console.WriteLine("ERROR from IsAtFrame(): Frame %u does not exist! %s sprite has frames from %u to %u.\n", FrameNumber, Name, 0, Rows * Columns - 1);
		return false;
	}

	public bool IsAtRow(int RowNumber)
	{
		if (RowNumber < Rows)
			return RowNumber == CurrentRow;

		Console.WriteLine("ERROR from IsAtRow(): Row does not exist!\n");
		return false;
	}

	public bool IsAtColumn(int ColumnNumber)
	{
		if (ColumnNumber < Columns)
		{
			return ColumnNumber == CurrentColumn;
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
		return CurrentRow == 0;
	}

	public bool IsAtFirstColumn()
	{
		return CurrentColumn == 0;
	}

	public bool IsAtFirstFrame()
	{
		return bContinuous ? IsAtFirstRow() && IsAtFirstColumn() : IsAtFirstColumn();
	}

	public bool IsAtLastFrame()
	{
		return bContinuous ? IsAtLastRow() && IsAtLastColumn() : IsAtLastColumn();
	}

	private void ResetTimer()
	{
		TimeRemainingFramesCounter = (float)(GetTotalTimeInFrames());
	}

	public Rectangle GetFrameRec()
	{
		return FrameRec;
	}

	public Texture2D GetSprite()
	{
		return Sprite;
	}

	public bool IsAtLastRow()
	{
		return CurrentRow == Rows - 1;
	}

	public bool IsAtLastColumn()
	{
		return CurrentColumn == Columns - 1;
	}

	public bool IsStartedPlaying()
	{
		if (IsAtFirstFrame())
		{
			ResetTimer();
			return true;
		}

		return bHasStartedPlaying;
	}

	public bool IsFinishedPlaying()
	{
		if (IsAtLastFrame())
		{
			ResetTimer();
			return true;
		}

		if (!bCanLoop)
			return bIsAnimationFinished;

		return bIsAnimationFinished;
	}

	public bool IsPlaying()
	{
		if (bCanLoop)
		{
			return !bPaused;
		}

		if (!bCanLoop && bContinuous)
		{
			return !bIsAnimationFinished;
		}

		return !bIsAnimationFinished;
	}
}
