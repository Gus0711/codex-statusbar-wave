using System.Drawing.Imaging;

namespace CodexStatusbar;

internal static class WavePreview
{
    public static int Run(string directory)
    {
        Directory.CreateDirectory(directory);
        using var form = new TokenStripForm();
        form.SetTransparentBackground(false);
        form.ApplyLayout(new OverlayLayoutResult(
            OverlayVisualState.Collapsed, CollapsedDisplayMode.TwoFields,
            ExpansionDirection.Down, 96,
            new IntRect(0, 0, 363, 37), new IntRect(0, 0, 363, 37), default, 0, 110));
        using var first = form.RenderSurfaceBitmap(new Size(363, 37), 96);
        first.Save(Path.Combine(directory, "wave-1.png"), ImageFormat.Png);
        Thread.Sleep(2250);
        using var second = form.RenderSurfaceBitmap(new Size(363, 37), 96);
        second.Save(Path.Combine(directory, "wave-2.png"), ImageFormat.Png);
        var changed = 0;
        for (var y = 0; y < first.Height; y++)
            for (var x = 0; x < first.Width; x++)
                if (first.GetPixel(x, y) != second.GetPixel(x, y)) changed++;
        var rounded = first.GetPixel(0, 0).A == 0 && second.GetPixel(0, 0).A == 0;
        Console.WriteLine($"Wave: {changed} pixels changed; transparent rounded corners: {rounded}");
        return changed > 1000 && rounded ? 0 : 1;
    }
}
