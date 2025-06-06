using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStage : MonoBehaviour
{
    public SerialHandler serialHandler;
    public GameObject stage;
    bool swpressed; //スイッチが押されたか

    private List<Vector3> angleCache = new List<Vector3>();
    public int angleCacheNum = 10;
    public Vector3 angle
    {
        private set
        {
            angleCache.Add(value);
            if (angleCache.Count > angleCacheNum)
            {
                angleCache.RemoveAt(0);
            }
        }
        get
        {
            if (angleCache.Count > 0)
            {
                var sum = Vector3.zero;
                angleCache.ForEach(angle => { sum += angle; });
                return sum / angleCache.Count;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    // Update is called once per frame
    void Update()
    {
        stage.transform.rotation = Quaternion.Euler(angle);
    }

    void OnDataReceived(string message)
    {
        if(string.Equals(message, "s")) //スイッチが押された時
        {
            Shot.Setswpressed();
            return;
        }
        var data = message.Split(
                new string[] { "\t" }, System.StringSplitOptions.None);
        if (data.Length < 2) return;

        try
        {
            var angleX = float.Parse(data[0]);
            var angleY = float.Parse(data[1]);
            if (angleX >= 0)
            {
                angle = new Vector3(angleX + 90, -angleY, -angleY);
            }
            else
            {
                angle = new Vector3(angleX + 90, angleY, angleY);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
