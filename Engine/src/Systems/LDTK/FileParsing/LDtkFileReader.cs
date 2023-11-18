namespace Engine.Systems.LDtk.Parsing;
using System.Text.Json;

/// <summary> LDtkFileReader. </summary>
public class LDtkFileReader : LDtkFile
{
    /// <summary> Read. </summary>
    /// <param name="input"> The ContentReader. </param>
    /// <param name="existingInstance"> The existingInstance. </param>
    /// <returns> The LDtkFile read from the content pipeline. </returns>
    protected override LDtkFile Read(LDtkFile fileString)
    {
        return existingInstance ?? JsonSerializer.Deserialize<LDtkFile>(input.ReadString(), Constants.SerializeOptions)!;
    }
}
