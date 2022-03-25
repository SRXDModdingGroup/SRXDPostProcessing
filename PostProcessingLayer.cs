namespace SRXDPostProcessing; 

/// <summary>
/// Specifies which layer a post processing effect should be applied to
/// </summary>
public enum PostProcessingLayer {
    /// <summary>
    /// Apply the effect to the entire screen
    /// </summary>
    Foreground,
    /// <summary>
    /// Apply the effect to the background only
    /// </summary>
    Background
}