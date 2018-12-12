using System;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class ObjectPlacer : MonoBehaviour
{
    public bool DrawDebugBoxes = false;
    public bool DrawBuildings = true;
    public bool DrawTrees = true;

    public SpatialUnderstandingCustomMesh SpatialUnderstandingMesh;

    private readonly List<BoxDrawer.Box> _lineBoxList = new List<BoxDrawer.Box>();

    private readonly Queue<PlacementResult> _results = new Queue<PlacementResult>();

    private bool _timeToHideMesh;
    private BoxDrawer _boxDrawing;

    private bool boxInstanciated;
    private bool padInstanciated;
    private bool isPlacingStarted;

    public GameObject gameController;
    public GameObject boxPrefab;
    public GameObject padPrefab;

    private GameObject box;
    private GameObject pad;

    // Use this for initialization
    void Start()
    {
        //gc = GetComponent<GameController>();

        if (DrawDebugBoxes)
        {
            _boxDrawing = new BoxDrawer(gameObject);
        }

    }

    void Update()
    {
        ProcessPlacementResults();

        if (_timeToHideMesh)
        {
            SpatialUnderstandingState.Instance.HideText = true;
            HideGridEnableOcclulsion();
            _timeToHideMesh = false;
        }

        if (DrawDebugBoxes)
        {
            _boxDrawing.UpdateBoxes(_lineBoxList);
        }

        if (!gameController.activeSelf && isPlacingStarted)
        {
            if (!boxInstanciated)
            {
                box = PlaceObject(boxPrefab);
                boxInstanciated = true;
            }
            
            if(box && !padInstanciated)
            {
                if (!box.GetComponent<TapToPlace>().IsBeingPlaced)
                {
                    pad = PlaceObject(padPrefab);
                    padInstanciated = true;
                }
            }

            if (box && !pad.GetComponent<TapToPlace>().IsBeingPlaced)
            {
                pad.GetComponent<TapToPlace>().enabled = false;
                box.GetComponent<TapToPlace>().enabled = false;
                gameController.GetComponent<GameController>().box = box;
                gameController.GetComponent<GameController>().doorlock = pad;
                gameController.SetActive(true);
            }
         }

    }

    private GameObject PlaceObject(GameObject go)
    {
        Vector3 playerPos = Camera.main.transform.position;
        Vector3 playerDirection = Camera.main.transform.forward;
        Quaternion playerRotation = Camera.main.transform.rotation;
        float spawnDistance = 1.8f;

        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
       
        GameObject spawned = Instantiate(go, spawnPos, playerRotation);

        return spawned;
    }

    private void HideGridEnableOcclulsion()
    {
        SpatialUnderstandingMesh.DrawProcessedMesh = false;
    }

    public void CreateScene()
    {
        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        SpatialUnderstandingDllObjectPlacement.Solver_Init();

        SpatialUnderstandingState.Instance.SpaceQueryDescription = "Generating World";
        _timeToHideMesh = true;
        isPlacingStarted = true;


        //Ab HIER alles weg machen und Objekte über Tap to Place plazieren. In Update wenn GameController !isActive, !boxplaced, !pincodeplaced (pincode taptoplace abschalten)
        /*
        List<PlacementQuery> queries = new List<PlacementQuery>();

        if (DrawBuildings)
        {
            queries.AddRange(AddBuildings());
            //gc.StartPauseTimer();
        }

        if (DrawTrees)
        {
            queries.AddRange(AddTrees());
        }

        GetLocationsFromSolver(queries);
        */
    }

    public List<PlacementQuery> AddBuildings()
    {

        var queries = CreateLocationQueriesForSolver(ObjectCollectionManager.Instance.WideBuildingPrefabs.Count, ObjectCollectionManager.Instance.WideBuildingSize, ObjectType.WideBuilding);
        queries.AddRange(CreateLocationQueriesForSolver(ObjectCollectionManager.Instance.SquareBuildingPrefabs.Count, ObjectCollectionManager.Instance.SquareBuildingSize, ObjectType.SquareBuilding));
        queries.AddRange(CreateLocationQueriesForSolver(ObjectCollectionManager.Instance.TallBuildingPrefabs.Count, ObjectCollectionManager.Instance.TallBuildingSize, ObjectType.TallBuilding));
        return queries;
    }

    public List<PlacementQuery> AddTrees()
    {
        var queries = CreateLocationQueriesForSolver(ObjectCollectionManager.Instance.TreePrefabs.Count, ObjectCollectionManager.Instance.TreeSize, ObjectType.Tree);

        return queries;
    }

    private int _placedSquareBuilding;
    private int _placedTallBuilding;
    private int _placedWideBuilding;
    private int _placedTree;

    private void ProcessPlacementResults()
    {
        if (_results.Count > 0)
        {
            var toPlace = _results.Dequeue();
            // Output
            if (DrawDebugBoxes)
            {
                DrawBox(toPlace, Color.red);
            }

            var rotation = Quaternion.LookRotation(toPlace.Normal, Vector3.up);

            switch (toPlace.ObjType)
            {
                case ObjectType.SquareBuilding:
                    ObjectCollectionManager.Instance.CreateSquareBuilding(_placedSquareBuilding++, toPlace.Position, rotation);
                    break;
                case ObjectType.TallBuilding:
                    ObjectCollectionManager.Instance.CreateTallBuilding(_placedTallBuilding++, toPlace.Position, rotation);
                    break;
                case ObjectType.WideBuilding:
                    ObjectCollectionManager.Instance.CreateWideBuilding(_placedWideBuilding++, toPlace.Position, rotation);
                    break;
                case ObjectType.Tree:
                    ObjectCollectionManager.Instance.CreateTree(_placedTree++, toPlace.Position, rotation);
                    break;
            }
        }
    }

    private void DrawBox(PlacementResult boxLocation, Color color)
    {
        if (boxLocation != null)
        {
            _lineBoxList.Add(
                new BoxDrawer.Box(
                    boxLocation.Position,
                    Quaternion.LookRotation(boxLocation.Normal, Vector3.up),
                    color,
                    boxLocation.Dimensions * 0.5f)
            );
        }
    }

    private void GetLocationsFromSolver(List<PlacementQuery> placementQueries)
    {
#if UNITY_WSA && !UNITY_EDITOR
        System.Threading.Tasks.Task.Run(() =>
        {
            // Go through the queries in the list
            for (int i = 0; i < placementQueries.Count; ++i)
            {
                var result = PlaceObject(placementQueries[i].ObjType.ToString() + i,
                                         placementQueries[i].PlacementDefinition,
                                         placementQueries[i].Dimensions,
                                         placementQueries[i].ObjType,
                                         placementQueries[i].PlacementRules,
                                         placementQueries[i].PlacementConstraints);
                if (result != null)
                {
                    _results.Enqueue(result);
                }
            }

            _timeToHideMesh = true;
        });
#else
        _timeToHideMesh = true;
#endif
    }

    private PlacementResult PlaceObject(string placementName,
        SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition placementDefinition,
        Vector3 boxFullDims,
        ObjectType objType,
        List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule> placementRules = null,
        List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint> placementConstraints = null)
    {

        // New query
        if (SpatialUnderstandingDllObjectPlacement.Solver_PlaceObject(
                placementName,
                SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(placementDefinition),
                (placementRules != null) ? placementRules.Count : 0,
                ((placementRules != null) && (placementRules.Count > 0)) ? SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(placementRules.ToArray()) : IntPtr.Zero,
                (placementConstraints != null) ? placementConstraints.Count : 0,
                ((placementConstraints != null) && (placementConstraints.Count > 0)) ? SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(placementConstraints.ToArray()) : IntPtr.Zero,
                SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticObjectPlacementResultPtr()) > 0)
        {
            SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult placementResult = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticObjectPlacementResult();

            return new PlacementResult(placementResult.Clone() as SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult, boxFullDims, objType);
        }

        return null;
    }

    private List<PlacementQuery> CreateLocationQueriesForSolver(int desiredLocationCount, Vector3 boxFullDims, ObjectType objType)
    {
        List<PlacementQuery> placementQueries = new List<PlacementQuery>();

        var halfBoxDims = boxFullDims * .5f;

        var disctanceFromOtherObjects = halfBoxDims.x > halfBoxDims.z ? halfBoxDims.x * 3f : halfBoxDims.z * 3f;

        for (int i = 0; i < desiredLocationCount; ++i)
        {
            var placementRules = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule>
            {
                SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule.Create_AwayFromOtherObjects(disctanceFromOtherObjects)
            };

            var placementConstraints = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint>();

            SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition placementDefinition = SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition.Create_OnFloor(halfBoxDims);

            placementQueries.Add(
                new PlacementQuery(placementDefinition,
                    boxFullDims,
                    objType,
                    placementRules,
                    placementConstraints
                ));
        }

        return placementQueries;
    }

}