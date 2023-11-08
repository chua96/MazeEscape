using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject MazeParent;

    [Header("Floor")]
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private List<GameObject> floor = new List<GameObject>();
    private bool SpawnFloor;

    [Header("Wall")]
    [SerializeField] private GameObject DoorPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject borderPrefab;
    [SerializeField] private List<GameObject> border = new List<GameObject>();
    [SerializeField] private GameObject selectedBorder;
    [SerializeField] private List<GameObject> wall = new List<GameObject>();

    [Header("Player")]
    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private GameObject Goal;
    [SerializeField] private GameObject Treasure;

    private bool SpawnHorizontalBorder;
    private bool SpawnVerticalBorder;
    private bool SpawnHorizontalWall;
    private bool SpawnVerticalWall;
    private bool SpawnBorder;
    private bool DoorIsCreated;
    private bool PlayerSpawned;
    private bool TreasureSpawned;
    private bool GoalSpawned;

    [SerializeField] private float MinMazeScale;
    [SerializeField] private float MaxMazeScale;
    [SerializeField] private float DoorNum;          // Number of door in Maze
    private float MazeScale;


    // Start is called before the first frame update
    private void OnEnable()
    {
        SetFloorScale();
        SpawnFloor = true;
        SpawnHorizontalBorder = true;
        SpawnVerticalBorder = true;
        SpawnHorizontalWall = true;
        SpawnVerticalWall = true;
        SpawnBorder = true;


        CreateFloor();
        if (SpawnBorder)
        {
            CreateHorzontalBorder();
            CreateVerticalBorder();
            SpawnBorder = false;
        }
        CreateHorizontalWall();
        CreateVerticalWall();

    }
    void Start()
    {
        CreateDoor();
        PlayerSpawnPoint();
        SetTreasure();
        SetGoal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetFloorScale()            // randomize floor scale
    {
        MazeScale = Random.Range(MinMazeScale, MaxMazeScale);
    }

    #region Maze Floor
    private void CreateFloor()          // Spawn Floor based on floor scale
    {
        if (SpawnFloor)
        {
            for (int i = 0; i < MazeScale; i++)
            {
                for (int j = 0; j < MazeScale; j++)
                {
                    // Instantiate floor then add the floor to the list
                    
                    GameObject Floor = Instantiate(floorPrefab, new Vector3(i * 10, 0, j * 10), Quaternion.identity);

                    Floor.transform.parent = MazeParent.transform;

                    floor.Add(Floor);

                    if (i <= MazeScale && j <= MazeScale)
                    {
                        SpawnFloor = false;
                    }
                }
            }
        }
    }
    #endregion

    #region Maze Border
    private void CreateHorzontalBorder()
    {
        if (SpawnHorizontalBorder)
        {
            for (int i = 0; i < MazeScale; i++)
            {
                // spawn first border from origin
                GameObject LeftBorder = Instantiate(borderPrefab, new Vector3((i * 10), 3.25f, -5), Quaternion.Euler(90, 0, 0));

                // spawn last border based on the maze scale
                GameObject RightBorder = Instantiate(borderPrefab, new Vector3((i * 10), 3.25f, (MazeScale * 10) - 5), Quaternion.Euler(90, 0, 0));

                LeftBorder.transform.parent = MazeParent.transform;
                RightBorder.transform.parent = MazeParent.transform;

                // add border to list
                border.Add(LeftBorder);
                border.Add(RightBorder);

                if (i <= MazeScale)
                {
                    SpawnHorizontalBorder = false;
                }
            }
        }
    }

    private void CreateVerticalBorder()
    {
        if (SpawnVerticalBorder)
        {
            for (int i = 0; i < MazeScale; i++)
            {
                // spawn border based on the origin
                GameObject FrontBorder = Instantiate(borderPrefab, new Vector3(-5, 3.25f, (i * 10)), Quaternion.Euler(90, 90, 0));

                // spawn border based on the maze scale
                GameObject BackBorder = Instantiate(borderPrefab, new Vector3((MazeScale * 10) - 5, 3.25f, (i * 10)), Quaternion.Euler(90, 90, 0));

                FrontBorder.transform.parent = MazeParent.transform;
                BackBorder.transform.parent = MazeParent.transform;

                // add border to list
                border.Add(FrontBorder);
                border.Add(BackBorder);

                if (i <= MazeScale)
                {
                    SpawnVerticalBorder = false;
                }
            }
        }
    }
#endregion

#region Maze Wall
    private void CreateHorizontalWall()
    {
        if (SpawnHorizontalWall)
        {
            for (int i = 0; i < MazeScale; i++)
            {
                // maze scale -1 to stop spawning wall at border position

                for (int j = 0; j <MazeScale - 1; j++)
                {
                    Debug.Log(i);

                    GameObject HorizontalWall = Instantiate(wallPrefab, new Vector3((i * 10), 3.25f, (j * 10) + 5), Quaternion.Euler(90, 0, 0));

                    HorizontalWall.transform.parent = MazeParent.transform;

                    wall.Add(HorizontalWall);

                    if (i <= MazeScale && j <= MazeScale)
                    {
                        SpawnHorizontalWall = false;
                    }
                }
            }
        }
    }

    private void CreateVerticalWall()
    {
        if (SpawnVerticalWall)
        {
            for (int i = 0; i < MazeScale; i++)
            {
                // maze scale -1 to stop spawning wall at border position

                for (int j = 0; j < MazeScale - 1; j++)
                {
                    GameObject VerticalWall = Instantiate(wallPrefab, new Vector3((j * 10) + 5, 3.25f, (i * 10)), Quaternion.Euler(90, 90, 0));

                    VerticalWall.transform.parent = MazeParent.transform;

                    wall.Add(VerticalWall);

                    if (i <= MazeScale && j <= MazeScale)
                    {
                        SpawnVerticalWall = false;
                    }
                }
            }
        }
    }
#endregion

    private void CreateDoor()
    {
        if (!DoorIsCreated && !SpawnHorizontalWall && !SpawnVerticalWall)
        {
            DoorNum = wall.Count * 0.8f;


            for (int i=0; i < DoorNum; i++)
            {
                int Rand = Random.Range(0, wall.Count);

                GameObject Door = Instantiate(DoorPrefab, wall[Rand].transform.position, wall[Rand].transform.rotation);

                Door.transform.parent = MazeParent.transform;

                Destroy(wall[Rand]);
                wall.RemoveAt(Rand);

                if (i >= DoorNum - 1)
                {
                    DoorIsCreated = true;
                }
            }
        }
    }

    private void PlayerSpawnPoint()
    {
        if (!PlayerSpawned)
        {
            int Rand = Random.Range(0, floor.Count);

            GameObject sp = Instantiate(SpawnPoint, new Vector3(floor[Rand].transform.position.x, 1.5f, floor[Rand].transform.position.z), floor[Rand].transform.rotation);

            sp.transform.parent = MazeParent.transform;

            floor.RemoveAt(Rand);

            PlayerSpawned = true;
        }
    }

    private void SetTreasure()
    {
        if (!TreasureSpawned)
        {
            int Rand = Random.Range(0, floor.Count);

            GameObject Key = Instantiate(Treasure, new Vector3(floor[Rand].transform.position.x, 1.5f, floor[Rand].transform.position.z), floor[Rand].transform.rotation);

            Key.transform.parent = MazeParent.transform;

            floor.RemoveAt(Rand);

            TreasureSpawned = true;
        }
    }

    private void SetGoal()
    {
        if (!GoalSpawned)
        {
            int Rand = Random.Range(0, floor.Count);

            GameObject exit = Instantiate(Goal, new Vector3(floor[Rand].transform.position.x, 0.25f, floor[Rand].transform.position.z), floor[Rand].transform.rotation);

            exit.transform.parent = MazeParent.transform;

            floor.RemoveAt(Rand);

            GoalSpawned = true;
        }
    }
}
