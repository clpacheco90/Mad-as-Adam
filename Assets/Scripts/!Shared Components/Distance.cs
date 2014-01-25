using UnityEngine;
using System.Collections;

[System.Serializable]
public class Distance {
//-----------------------------------------------------------------------------------------------------------------------------//	
    public Vector2 minimumDistance;
    public Vector2 maximumDistance;
    public float X;
    public float Y;
    public float XAbs;
    public float YAbs;
//-----------------------------------------------------------------------------------------------------------------------------//	
    public static float DistanceNonAbs(float a, float b) {
        return a - b;
    }
//-----------------------------------------------------------------------------------------------------------------------------//	
    public static float DistanceAbs(float a, float b) {
        return Mathf.Abs(a - b);
    }
//-----------------------------------------------------------------------------------------------------------------------------//	
}
