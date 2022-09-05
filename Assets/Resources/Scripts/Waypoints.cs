using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Waypoints : MonoBehaviour
{
    private static Dictionary<(Vector3, Vector3), Vector3[]> paths = new Dictionary<(Vector3, Vector3), Vector3[]>();
    public static Tilemap tilemap;
    private static Grid grid;

    public static List<string> all_road_tiles = new List<string>() { "world_19", "world_40", "world_20", "world_41", "world_21", "world_42", "world_25" };
    public static List<string> straight_road_tiles = new List<string>() { "world_19", "world_40" };
  
    class PathNode
    {
        public Vector3Int pos;
        public PathNode prev;
        public string name;

        public PathNode(Vector3Int pos, PathNode prev, string name)
        {
            this.pos = pos;
            this.prev = prev;
            this.name = name;
        }
    }

    static Vector3Int[] circle = { new Vector3Int(0, 1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, 0, 0), };

    public void Awake()
    {
        var gridObj = GameObject.FindGameObjectWithTag("map");

        grid = gridObj.GetComponent<Grid>();
        tilemap = gridObj.GetComponentInChildren<Tilemap>();

    }

    public static Vector3[] GetPath(Vector3 start_point, Vector3 end_point)
    {
        if (paths.ContainsKey((start_point, end_point)))
        {
            return paths[(start_point, end_point)];
        }
        else
        {
            var _path = FindPath(tilemap, grid.WorldToCell(start_point), grid.WorldToCell(end_point));

            Vector3 offset = new Vector3(grid.cellSize.x / 2, grid.cellSize.y / 2, 0);
            Vector3[] path = new Vector3[_path.Count];

            for (int i = 0; i < _path.Count; i++)
            {
                path[i] = grid.CellToWorld(_path[_path.Count - 1 - i]) + offset;
            }
            
            return path;
        }
    }

    private static List<Vector3Int> FindPath(Tilemap tilemap, Vector3Int startPos, Vector3Int endPos)
    {
        List<PathNode> visited = new List<PathNode>();
        Queue<PathNode> to_visit = new Queue<PathNode>();
        List<Vector3Int> path = new List<Vector3Int>();

        //string road_sprite_name = ((Tile)tilemap.GetTile(startPos)).sprite.name;

        to_visit.Enqueue(new PathNode(startPos, null, null));

        while (to_visit.Count != 0)
        {
            var cur_cell = to_visit.Dequeue();
            visited.Add(cur_cell);

            if (cur_cell.pos == endPos)
            {
                path.Add(endPos);
                while (cur_cell != null)
                {
                    if (!straight_road_tiles.Contains(cur_cell.name))
                    {
                        path.Add(cur_cell.pos);
                    }
                    cur_cell = cur_cell.prev;
                }
                break;
            }

            foreach (var dir in circle)
            {
                Vector3Int pos = cur_cell.pos + dir;

                foreach (var node in visited)
                {
                    if (node.pos == pos) goto skip;
                }
                foreach (var node in to_visit)
                {
                    if (node.pos == pos) goto skip;
                }

                Tile tile = (Tile)tilemap.GetTile(pos);

                if (tile != null && all_road_tiles.Contains(tile.sprite.name))
                {
                    to_visit.Enqueue(new PathNode(pos, cur_cell, tile.sprite.name));
                }

                skip:
                continue;
            }
        }
        return path;
    }
}
