namespace DonutEngine.Shaders
{
    using Raylib_cs;
    using static Raylib_cs.ShaderUniformDataType;
    using static Raylib_cs.Color;
    using static Raylib_cs.Raylib;
    using System.Numerics;
    public struct OutlineShader
    {
        public OutlineShader(Texture2D texture2D, Vector4 color, float outlineSize)
        {
            texture = texture2D;
            shader = Raylib.LoadShader(null ,DonutFilePaths.app+"Shaders/outline.fs");
            this.outlineSize = outlineSize;
            outlineColor = new[] 
            { 
                color.X,
                color.Y, 
                color.Z, 
                color.W 
            };
            textureSize = new[]
            {
                (float)texture2D.width,
                (float)texture2D.height 
            };
            outlineSizeLoc = GetShaderLocation(shader, "outlineSize");
            outlineColorLoc = GetShaderLocation(shader, "outlineColor");
            textureSizeLoc = GetShaderLocation(shader, "textureSize");
            Raylib.SetShaderValue(shader, outlineSizeLoc, this.outlineSize, SHADER_UNIFORM_FLOAT);
            Raylib.SetShaderValue(shader, outlineColorLoc, outlineColor, SHADER_UNIFORM_VEC4);
            Raylib.SetShaderValue(shader, textureSizeLoc, textureSize, SHADER_UNIFORM_VEC2);
        }

        public Texture2D texture;
        public Shader shader;
        public float outlineSize;

        // Normalized RED color
        public float[] outlineColor;
        public float[] textureSize;

        // Get shader locations
        public int outlineSizeLoc;
        public int outlineColorLoc;
        public int textureSizeLoc;
        
    }
}