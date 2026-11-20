using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame.Text;

internal static class EmbeddedFont
{
    private static readonly ConditionalWeakTable<GraphicsDevice, Texture2D> Textures = new();

    internal static Texture2D GetTexture(GraphicsDevice device) =>
        Textures.GetValue(device, LoadTexture);

    private static Texture2D LoadTexture(GraphicsDevice device)
    {
        using var stream = typeof(EmbeddedFont).Assembly.GetManifestResourceStream("RetroGame.Font.png")
            ?? throw new InvalidOperationException("The embedded RetroGame font is missing.");
        var texture = Texture2D.FromStream(device, stream, ProcessPixels);
        // Fonts belong to their device, rather than whichever game draws text first.
        device.Disposing += (_, _) =>
        {
            Textures.Remove(device);
            texture.Dispose();
        };
        return texture;
    }

    internal static void ProcessPixels(byte[] pixels)
    {
        // Match the former content pipeline: magenta color key and premultiplied alpha.
        for (var i = 0; i < pixels.Length; i += 4)
        {
            if (pixels[i] == 255 && pixels[i + 1] == 0 && pixels[i + 2] == 255 && pixels[i + 3] == 255)
                pixels[i + 3] = 0;
            var alpha = pixels[i + 3];
            pixels[i] = (byte)(pixels[i] * alpha / 255);
            pixels[i + 1] = (byte)(pixels[i + 1] * alpha / 255);
            pixels[i + 2] = (byte)(pixels[i + 2] * alpha / 255);
        }
    }
}
