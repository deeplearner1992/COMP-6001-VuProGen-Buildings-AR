using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    [SerializeField]
    private ProceduralGenerator proGen;

    [SerializeField]
    private GameObject cornerARows;

    [SerializeField]
    private GameObject cornerAColumns;

    [SerializeField]
    private GameObject cornerBRows;

    [SerializeField]
    private GameObject cornerBColumns;

    [SerializeField]
    private GameObject cornerCRows;

    [SerializeField]
    private GameObject cornerCColumns;

    [SerializeField]
    private GameObject cornerDRows;

    [SerializeField]
    private GameObject cornerDColumns;

    [SerializeField]
    private float offset = 0.5f;

    public void UpdatePositions()
    {
        cornerARows.transform.localPosition = new Vector3(-2 - offset, 0, 1 - offset + proGen.Columns);
        cornerAColumns.transform.localPosition = new Vector3(-1 - offset, 0, 2 - offset + proGen.Columns);

        cornerBRows.transform.localPosition = new Vector3(-2 - offset, 0, -1 - offset);
        cornerBColumns.transform.localPosition = new Vector3(-1 - offset, 0, -2 - offset);

        cornerCRows.transform.localPosition = new Vector3(2 - offset + proGen.Rows, 0, -1 - offset);
        cornerCColumns.transform.localPosition = new Vector3(1 - offset + proGen.Rows, 0, -2 - offset);

        cornerDRows.transform.localPosition = new Vector3(2 - offset + proGen.Rows, 0, 1 - offset + proGen.Columns);
        cornerDColumns.transform.localPosition = new Vector3(1 - offset + proGen.Rows, 0, 2 - offset + proGen.Columns);
    }
}
