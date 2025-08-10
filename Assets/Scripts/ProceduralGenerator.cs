using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    [SerializeField]
    private bool isMainMenu = false;

    [SerializeField]
    private GameObject mainMenuParent;

    [SerializeField]
    private GameObject imageTarget;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private GameObject roofPrefab;

    [SerializeField]
    private GameObject[] roofPrefabs;

    [SerializeField]
    private bool randomizeRoofSelection;

    [SerializeField]
    private GameObject doorPrefab;

    [SerializeField]
    private GameObject windowPrefab;

    [SerializeField]
    private GameObject[] windowPrefabs;

    [SerializeField]
    private bool randomizeWindowSelection;

    [SerializeField] 
    private GameObject groundPrefab;

    [SerializeField]
    private bool includeRoof = true;

    [SerializeField]
    private bool keepInsideWalls = false;

    [SerializeField]
    private float windowPercentChance = 0.6f;

    [SerializeField]
    private float doorPercentChance = 0.3f;

    [SerializeField]
    private float roofPercentChance = 0.3f;

    [SerializeField]
    private float instructionsSize = 1f;

    [Header("Grid Options")]
    [SerializeField]
    [Range(1, 20)]
    private int rows = 3;

    [SerializeField]
    [Range(0, 20)]
    private int topRowsUpperLimit = 3;

    [SerializeField]
    [Range(0, 20)]
    private int topRowsLowerLimit = 0;

    [SerializeField]
    [Range(0, 20)]
    private int bottomRowsUpperLimit = 3;

    [SerializeField]
    [Range(0, 20)]
    private int bottomRowsLowerLimit = 0;

    [SerializeField]
    [Range(1, 20)]
    private int columns = 3;

    [SerializeField]
    [Range(0, 20)]
    private int leftColumnsUpperLimit = 3;

    [SerializeField]
    [Range(0, 20)]
    private int leftColumnsLowerLimit = 0;

    [SerializeField]
    [Range(0, 20)]
    private int rightColumnsUpperLimit = 3;

    [SerializeField]
    [Range(0, 20)]
    private int rightColumnsLowerLimit = 0;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float cellUnitSize = 1;

    [SerializeField]
    [Range(1, 20)]
    private int numberOfFloors = 1;

    [SerializeField]
    private float scaleMultiplier = 0.02f;

    [SerializeField]
    private Floor[] floors;

    private List<GameObject> rooms = new List<GameObject>();

    [SerializeField]
    private int prefabCounter = 0;

    public int Rows
    {
        set
        {
            rows = value;
        }

        get
        {
            return rows;
        }
    }

    public int Columns
    {
        set
        {
            columns = value;
        }

        get
        {
            return columns;
        }
    }

    public int NumberOfFloors
    {
        set
        {
            numberOfFloors = value;
        }

        get
        {
            return numberOfFloors;
        }
    }

    public int TopRowsUpperLimit
    {
        set
        {
            topRowsUpperLimit = value;
        }

        get
        {
            return topRowsUpperLimit;
        }
    }

    public int TopRowsLowerLimit
    {
        set
        {
            topRowsLowerLimit = value;
        }

        get
        {
            return topRowsLowerLimit;
        }
    }

    public int BottomRowsUpperLimit
    {
        set
        {
            bottomRowsUpperLimit = value;
        }

        get
        {
            return bottomRowsUpperLimit;
        }
    }

    public int BottomRowsLowerLimit
    {
        set
        {
            bottomRowsLowerLimit = value;
        }

        get
        {
            return bottomRowsLowerLimit;
        }
    }

    public int LeftColumnsUpperLimit
    {
        set
        {
            leftColumnsUpperLimit = value;
        }

        get
        {
            return leftColumnsUpperLimit;
        }
    }
    public int LeftColumnsLowerLimit
    {
        set
        {
            leftColumnsLowerLimit = value;
        }

        get
        {
            return leftColumnsLowerLimit;
        }
    }

    public int RightColumnsUpperLimit
    {
        set
        {
            rightColumnsUpperLimit = value;
        }

        get
        {
            return rightColumnsUpperLimit;
        }
    }

    public int RightColumnsLowerLimit
    {
        set
        {
            rightColumnsLowerLimit = value;
        }

        get
        {
            return rightColumnsLowerLimit;
        }
    }

    public float ScaleMultiplier
    {
        set
        {
            scaleMultiplier = value;
        }

        get
        {
            return scaleMultiplier;
        }
    }
    public bool IncludeRoof
    {
        set
        {
            includeRoof = value;
        }

        get
        {
            return includeRoof;
        }
    }

    public float InstructionsSize
    {
        set
        {
            instructionsSize = value;
        }

        get
        {
            return instructionsSize;
        }
    }

    private void Awake()
    {
        if (isMainMenu)
        {
            InvokeRepeating("GenerateMainMenuBuilding", 0f, 5f);
        } 
        else
        {
            Generate();
            CenterBuilding();
            ReScale(scaleMultiplier);
        }
        
    }

    private void Update()
    {
        if (isMainMenu)
        {
            float temp = this.mainMenuParent.transform.eulerAngles.y + 1f;
            this.mainMenuParent.transform.eulerAngles = new Vector3(0, temp, 0);

            if (this.mainMenuParent.transform.eulerAngles.y >= 360)
            {
                this.mainMenuParent.transform.eulerAngles = Vector3.zero;
            }
        }
    }

    public void Generate()
    {
        prefabCounter = 0;

        Clear();

        BuildDataStructure();

        Render();

        if (!keepInsideWalls)
        {
            RemoveInsideWalls();
        }
    }

    void BuildDataStructure()
    {
        floors = new Floor[numberOfFloors];

        int floorCount = 0;

        foreach(Floor floor in floors)
        {
            Room[,] rooms = new Room[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                
                for (int column = 0; column < columns; column++)
                {

                    var roomPosition = new Vector3(row * cellUnitSize, floorCount, column * cellUnitSize);
                    rooms[row, column] = new Room(roomPosition, includeRoof ? (floorCount == floors.Length - 1) : false);

                    rooms[row, column].Walls = new Wall[4];

                    rooms[row, column].Walls[0] = new Wall(roomPosition, Quaternion.Euler(0, 0, 0));
                    rooms[row, column].Walls[1] = new Wall(roomPosition, Quaternion.Euler(0, 90, 0));
                    rooms[row, column].Walls[2] = new Wall(roomPosition, Quaternion.Euler(0, 180, 0));
                    rooms[row, column].Walls[3] = new Wall(roomPosition, Quaternion.Euler(0, -90, 0));
                }
            }

            floors[floorCount] = new Floor(floorCount++, rooms);
        }
    }

    void Render()
    {
        foreach (Floor floor in floors)
        {
            for (int row = 0; row < floor.Rows; row++)
            {
                for (int column = 0; column < floor.Columns; column++)
                {
                    bool toGenerate = true;

                    if ((row < bottomRowsLowerLimit && column < leftColumnsLowerLimit) ||
                       (row >= bottomRowsUpperLimit && column < rightColumnsLowerLimit) ||
                       (row < topRowsLowerLimit && column >= leftColumnsUpperLimit) ||
                       (row >= topRowsUpperLimit && column >= rightColumnsUpperLimit))
                    {
                        toGenerate = false;
                    }

                    if (toGenerate)
                    {
                        Room room = floor.rooms[row, column];
                        GameObject roomGo = new GameObject($"Room_{floor.FloorNumber}_{row}_{column}");
                        rooms.Add(roomGo);
                        roomGo.transform.parent = transform;

                        if (floor.FloorNumber == 0)
                            RoomPlacement(UnityEngine.Random.Range(0.0f, 1.0f) <= doorPercentChance ? doorPrefab : wallPrefab, room, roomGo);
                        else
                        {
                          
                            if (UnityEngine.Random.Range(0.0f, 1.0f) < windowPercentChance)
                            {
                                if (randomizeWindowSelection)
                                {
                                    int windowIndex = UnityEngine.Random.Range(0, windowPrefabs.Length);
                                    RoomPlacement(windowPrefabs[windowIndex], room, roomGo);
                                }
                                else
                                {
                                    RoomPlacement(windowPrefabs[0], room, roomGo);
                                }
                            }
                            else
                                RoomPlacement(wallPrefab, room, roomGo);

                        }
                    }
                }
            }
        }
    }

    private void RoomPlacement(GameObject prefab, Room room, GameObject roomGo)
    {
        SpawnPrefab(prefab, roomGo.transform, room.Walls[0].Position, room.Walls[0].Rotation);
        SpawnPrefab(prefab, roomGo.transform, room.Walls[1].Position, room.Walls[1].Rotation);
        SpawnPrefab(prefab, roomGo.transform, room.Walls[2].Position, room.Walls[2].Rotation);
        SpawnPrefab(prefab, roomGo.transform, room.Walls[3].Position, room.Walls[3].Rotation);

        SpawnPrefab(groundPrefab, roomGo.transform, room.Walls[0].Position, room.Walls[0].Rotation);

        if (room.HasRoof)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) < roofPercentChance)
            {
                if (randomizeRoofSelection)
                {
                    int roofIndex = UnityEngine.Random.Range(0, roofPrefabs.Length);
                    SpawnPrefab(roofPrefabs[roofIndex], roomGo.transform, room.Walls[0].Position, room.Walls[0].Rotation);
                }
                else
                {
                    SpawnPrefab(roofPrefabs[0], roomGo.transform, room.Walls[0].Position, room.Walls[0].Rotation);
                }
            }
            else
                SpawnPrefab(roofPrefab, roomGo.transform, room.Walls[0].Position, room.Walls[0].Rotation);
        }
    }

    void SpawnPrefab(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        var GameObject = Instantiate(prefab, position, rotation);
        GameObject.transform.parent = parent;
        prefabCounter++;
    }

    void RemoveInsideWalls()
    {
        var wallComponents = GameObject.FindObjectsByType<WallComponent>(FindObjectsSortMode.None);
        var childs = wallComponents.Select(c => c.transform.GetChild(0).position.ToString()).ToList();

        var dupPositions = childs.GroupBy(c => c)
            .Where(c => c.Count() > 1)
            .Select(grp => grp.Key)
            .ToList();

        foreach (WallComponent w in wallComponents)
        {
            var childTransform = w.transform.GetChild(0);
            if (dupPositions.Contains(childTransform.position.ToString()))
            {
                DestroyImmediate(childTransform.gameObject);
            }
        }
    }

    void Clear()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            DestroyImmediate(rooms[i]);
        }
        rooms.Clear();
    }

    public void ReScale(float value)
    {
        this.transform.localScale = new Vector3(value, value, value);
    }

    void CenterBuilding()
    {
        float x = (-rows / 2) / (1 / scaleMultiplier);
        float z = (-columns / 2) / (1 / scaleMultiplier);
        transform.localPosition = new Vector3(x, 0, z);
    }

    void ResetImageTargetLocation()
    {
        imageTarget.transform.localPosition = new Vector3 (0, 0, 0);
        imageTarget.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void ReGenerate()
    {
        prefabCounter = 0;

        Clear();
        ReScale(1);
        ResetImageTargetLocation();
        BuildDataStructure();
        Render();
        CenterBuilding();
        ReScale(scaleMultiplier);

        if (!keepInsideWalls)
        {
            RemoveInsideWalls();
        }

    }

    private void RandomizeParemeters()
    {
        this.rows = UnityEngine.Random.Range(5, 11);
        this.columns = UnityEngine.Random.Range(5, 11);
        this.numberOfFloors = UnityEngine.Random.Range(1, 21);

        this.topRowsUpperLimit = UnityEngine.Random.Range(this.topRowsLowerLimit, this.rows + 1);
        this.topRowsLowerLimit = UnityEngine.Random.Range(0, this.topRowsUpperLimit);

        this.bottomRowsUpperLimit = UnityEngine.Random.Range(this.bottomRowsLowerLimit, this.rows + 1);
        this.bottomRowsLowerLimit = UnityEngine.Random.Range(0, this.bottomRowsUpperLimit);

        this.leftColumnsUpperLimit = UnityEngine.Random.Range(this.leftColumnsLowerLimit, this.columns + 1);
        this.leftColumnsLowerLimit = UnityEngine.Random.Range(0, this.leftColumnsUpperLimit);

        this.rightColumnsUpperLimit= UnityEngine.Random.Range(this.rightColumnsLowerLimit, this.columns + 1);
        this.rightColumnsLowerLimit = UnityEngine.Random.Range(0, this.rightColumnsUpperLimit);
    }

    private void ResetMainMenuBuildingPosition()
    {
        this.transform.localPosition = Vector3.zero;
    }

    private void CenterMainMenuBuilding()
    {
        float x = - this.rows / 2 * 10;
        float z = - this.columns / 2 * 10;
        this.transform.localPosition = new Vector3 (x, 0, z);
    }

    private float ResetMainMenuParentRotationY()
    {
        float temp = this.transform.localEulerAngles.y;
        this.mainMenuParent.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        return temp;
    }

    private void ReOrientMainMenuParentY(float temp)
    {
        this.mainMenuParent.transform.localEulerAngles = new Vector3 (0f, temp, 0f);
    }

    private void GenerateMainMenuBuilding()
    {
        RandomizeParemeters();
        ReScale(1);
        ResetMainMenuBuildingPosition();
        float temp = ResetMainMenuParentRotationY();
        Generate();
        ReOrientMainMenuParentY(temp);
        CenterMainMenuBuilding();
        ReScale(10);
    }
}
