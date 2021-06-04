using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public MazeCell MazeCellPrefab;
    public MinimapCell MinimapCellPrefab;
    public Minimap Minimap;
    public Vector3 MazeCellSize = new Vector3(1, 1, 0);

    public int MazeWidth = 10;
    public int MazeHeight = 10;

    private void Start()
    {
        var generator = new MazeGenerator(MazeWidth, MazeHeight);
        var maze = generator.GenerateMaze();

        for (var x = 0; x < maze.GetLength(0); x++)
        {
            for (var y = 0; y < maze.GetLength(1); y++)
            {
                MazeCell cell = Instantiate(MazeCellPrefab,
                    new Vector3(x * MazeCellSize.x, y * MazeCellSize.y, y * MazeCellSize.z), Quaternion.identity)
                    .GetComponent<MazeCell>();
                MinimapCell minimapCell = Instantiate(MinimapCellPrefab,
                    new Vector3(x - 10, 0, y - 10), Quaternion.identity)
                    .GetComponent<MinimapCell>();

                cell.WallLeft.SetActive(maze[x, y].WallLeft);
                cell.WallLeftExit.SetActive(maze[x, y].WallLeftExit);

                cell.WallBottom.SetActive(maze[x, y].WallBottom);
                cell.WallBottomExit.SetActive(maze[x, y].WallBottomExit);

                cell.Floor.SetActive(maze[x, y].Floor);
                cell.Ceiling.SetActive(maze[x, y].Ceiling);
                cell.LightOnWall.SetActive(maze[x, y].Light);
                var lightOnWall = cell.LightOnWall.GetComponent<LightOnWall>();
                var light = lightOnWall.BlockLight.GetComponent<Light>();
                light.color = maze[x, y].BlockLightColor;


                minimapCell.WallLeft.SetActive(maze[x, y].WallLeft);
                minimapCell.WallBottom.SetActive(maze[x, y].WallBottom);
                minimapCell.Floor.SetActive(maze[x, y].Floor);
                var camera = Minimap.MinimapCamera;
                camera.transform.position = new Vector3(
                    maze.GetLength(0) / 2 - 10.5f,
                    1,
                    maze.GetLength(1) / 2 - 10.5f);
            }
        }
    }
}
