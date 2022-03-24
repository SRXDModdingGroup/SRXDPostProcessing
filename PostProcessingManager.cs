using System.Collections.Generic;

namespace SRXDPostProcessing; 

public static class PostProcessingManager {
    internal static List<PostProcessingInstance> PostProcessingInstances { get; } = new();

    public static void AddPostProcessingInstance(PostProcessingInstance instance) {
        int index = PostProcessingInstances.BinarySearch(instance);

        if (index >= 0)
            return;
        
        PostProcessingInstances.Insert(~index, instance);
    }

    public static void RemovePostProcessingInstance(PostProcessingInstance instance) => PostProcessingInstances.Remove(instance);
}