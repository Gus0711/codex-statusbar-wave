using System.Text.Json;

namespace CodexStatusbar;

internal sealed record QuotaWindow(double UsedPercent, int Minutes, long? ResetsAt)
{
    public double RemainingPercent => Math.Clamp(100 - UsedPercent, 0, 100);
    public static QuotaWindow? Parse(JsonElement element)
    {
        if (!element.TryGetProperty("used_percent", out var used)
            || !used.TryGetDouble(out var value) || !double.IsFinite(value)
            || !element.TryGetProperty("window_minutes", out var duration)
            || !duration.TryGetInt32(out var minutes) || minutes <= 0) return null;
        long? resets = element.TryGetProperty("resets_at", out var reset) && reset.TryGetInt64(out var timestamp)
            ? timestamp : null;
        return new QuotaWindow(value, minutes, resets);
    }
    public string Label(string name)
    {
        // Once the recorded window expires, wait for an official refreshed reading.
        if (ResetsAt is long reset && DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= reset)
            return name + " —";
        return $"{name} {RemainingPercent:0}% rest.";
    }
}
