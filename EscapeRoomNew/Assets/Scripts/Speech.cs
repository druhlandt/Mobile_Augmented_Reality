using HoloToolkit.Unity;
using UnityEngine;

public class Speech : MonoBehaviour
{
    public SpatialUnderstandingCustomMesh SpatialUnderstandingMesh;


    public void ToggleMesh()
    {
        SpatialUnderstandingMesh.DrawProcessedMesh = !SpatialUnderstandingMesh.DrawProcessedMesh;
    }

}