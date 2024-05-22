using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;


public class Compass : MonoBehaviour
{
    public RawImage CompassImage;

    // Start is called before the first frame update
    public ConnectionHandler connectionHandler;
    private GatewayConnection conn;

    float lastTime;
    float cameraOffset;

    void Start()
    {
        conn = connectionHandler.GetConnection();
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        /*float cameraYaw = Camera.main.transform.rotation.eulerAngles.y;

        if (Time.time - lastTime > 1) 
        {
            lastTime = Time.deltaTime;
            string IMUstring = conn.GetIMUJsonString();

            // Load IMU data into map
            JObject jo = JObject.Parse(IMUstring);
            float tssHeading = jo["imu"]["eva1"]["heading"].ToObject<float>();
            cameraOffset = tssHeading - cameraYaw;
            //Debug.Log(cameraOffset);
        }


        // Update with tss server/prevent desync
        cameraYaw += cameraOffset;

        //Debug.Log(cameraYaw);

        // Rotate image
        CompassImage.uvRect = new Rect(cameraYaw / 360, 0, 1, 1);*/
    }
}
