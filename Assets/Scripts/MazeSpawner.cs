using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public MazeCell MazeCellPrefab;
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

                cell.WallLeft.SetActive(maze[x, y].WallLeft);
                cell.WallBottom.SetActive(maze[x, y].WallBottom);
            }
        }
    }
}
