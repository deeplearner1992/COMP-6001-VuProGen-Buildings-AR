
using UnityEngine;

[System.Serializable]
public class Wall
{
    public enum WallType
    {
        Normal,
        Blank
    }

    public WallType WallTypeSelected {  get; set; } = WallType.Normal;

    public Vector3 Position { get; private set; }

    public Quaternion Rotation { get; private set; }

    public Wall(Vector3 position, Quaternion rotation, WallType wallType = WallType.Normal)
    {
        WallTypeSelected = wallType;
        this.Position = position;
        this.Rotation = rotation;
    }
}
