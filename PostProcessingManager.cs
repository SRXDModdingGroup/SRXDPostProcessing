using System.Collections.Generic;

namespace SRXDPostProcessing; 

/// <summary>
/// Utility class for adding and removing post processing effects
/// </summary>
public static class PostProcessingManager {
    internal static List<PostProcessingInstance> PostProcessingInstances { get; } = new();

    /// <summary>
    /// Adds a new post processing effect
    /// </summary>
    /// <param name="instance">The instance to add</param>
    public static void AddPostProcessingInstance(PostProcessingInstance instance) {
        int index = PostProcessingInstances.BinarySearch(instance);

        if (index >= 0)
            return;
        
        PostProcessingInstances.Insert(~index, instance);
    }

    /// <summary>
    /// Removes a post processing effect
    /// </summary>
    /// <param name="instance">The instance to remove</param>
    public static void RemovePostProcessingInstance(PostProcessingInstance instance) => PostProcessingInstances.Remove(instance);
}