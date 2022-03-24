using System;
using UnityEngine;

namespace SRXDPostProcessing; 

public class PostProcessingInstance : IComparable<PostProcessingInstance> {
    public bool Enabled { get; set; } = true;
    
    public Material Material { get; }
    
    public int Priority { get; }

    public PostProcessingInstance(Material material, int priority) {
        Material = material;
        Priority = priority;
    }

    public int CompareTo(PostProcessingInstance other) => Priority.CompareTo(other.Priority);
}