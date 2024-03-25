using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject thingPrefab;
    float theta;
    public float thingRange;
    public float spinRate;
    Vector3 dPos;
    public Vector3 pos, pos0;

    public MeshGenerator myLevelGenInf;
    public GameObject theTerrain;

    public Vector3 Pos0
    {
        get { return pos0; }
        set { pos0 = value; }
    }
    public Vector3 Pos
    {
        get { return pos; }
        set { pos = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        myLevelGenInf = theTerrain.GetComponent<MeshGenerator>();
        pos0 = myLevelGenInf.TerrainCenter + Vector3.up * 3.0f;
        thingPrefab = Instantiate(thingPrefab, pos0, Quaternion.identity);

        pos = pos0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myLevelGenInf = theTerrain.GetComponent<MeshGenerator>();
        pos0 = myLevelGenInf.TerrainCenter + (Vector3.up * 3.0f);

        theta = spinRate * 2.0f * Mathf.PI * Time.time;
        //dPos = Vector3.right * Mathf.Sin(theta) + Vector3.forward * Mathf.Cos(theta);
        //pos = pos0 + thingRange * dPos.normalized;
        dPos = 10 * Time.deltaTime * Vector3.back;
        pos += dPos;
        thingPrefab.transform.position = pos;
        //Debug.Log(pos);
    }
}
