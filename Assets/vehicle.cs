using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vehicle : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float heading;

    private float x, y;

    public int order;
    Transform transform;
    private static StreamWriter writer = null;

    public GameObject M, V;

    Scene currentScene;
    void Start()
    {
        if (writer == null)
        {
            writer = File.AppendText(filePath);
        }
        currentScene = SceneManager.GetActiveScene();

        Time.timeScale = 100.0f;
        V = GameObject.Find("V");
        M = GameObject.Find("M");

        Random.InitState(System.DateTime.Now.Millisecond * this.gameObject.GetHashCode());
        transform = this.GetComponent<Transform>();
        x = Random.Range(-20, 20);
        y = Random.Range(-20, 20);
        heading = Random.Range(0f, 360f);
        // heading = System.DateTime.Now.Millisecond * 100 % 360;
        transform.position = new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(0, 0, heading);
        speed = Random.Range(0f, 5f);

        if (this.name == "M")
        {
            Invoke("Timeout", 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(this.GetComponent<Rigidbody2D>().velocity.magnitude);
    }

    void Timeout()
    {
        // Debug.Log("timeout");
        // string positionX = (M.GetComponent<vehicle>().x - V.GetComponent<vehicle>().x).ToString();
        // string positionY = (M.GetComponent<vehicle>().y - V.GetComponent<vehicle>().y).ToString();
        // string angleM = ((360f - M.GetComponent<vehicle>().heading) * Mathf.Deg2Rad).ToString();
        // string angleV = ((360f - V.GetComponent<vehicle>().heading) * Mathf.Deg2Rad).ToString();
        // string speedM = M.GetComponent<vehicle>().speed.ToString();
        // string speedV = V.GetComponent<vehicle>().speed.ToString();
        // string ans = "0";

        // string textToAppend = positionX + "," + positionY + "," + angleM + "," + angleV + "," + speedM + "," + speedV + "," + ans;

        // // Open the file in append mode using StreamWriter
        // // StreamWriter writer = File.AppendText(filePath);
        // // Write the text to append to the file
        // writer.WriteLine(textToAppend);

        // writer.Close();

        Scene currentScene = SceneManager.GetActiveScene();
        // Reload the current scene by its name or index
        SceneManager.LoadScene(currentScene.name);

    }

    void FixedUpdate()
    {
        // Debug.Log("FIXED UPDATE");
        // Get the Rigidbody2D component
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();

        // Get the current rotation angle
        float rotationAngle = heading;

        // Convert the rotation angle to radians
        float angleInRadians = (rotationAngle + 90) * Mathf.Deg2Rad;

        // Calculate the movement direction based on the rotation angle
        Vector2 movementDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // Calculate the desired velocity based on the movement direction and speed
        Vector2 velocity = movementDirection * speed;

        // Assign the velocity to the Rigidbody2D
        rb2D.velocity = velocity;
        // Debug.Log(this.name + " :: " + x + ", " + y + ", " + heading + ", " + speed + ", " + velocity.magnitude);
        // speed = velocity.magnitude;

    }

    static string filePath = "/home/ahmed/hi.txt";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.name == "V")
            return;
        // if (other.tag == "Wall")
        // {
        //     // string positionX = (M.GetComponent<vehicle>().x - V.GetComponent<vehicle>().x).ToString();
        //     // string positionY = (M.GetComponent<vehicle>().y - V.GetComponent<vehicle>().y).ToString();
        //     // string angleM = ((360f - M.GetComponent<vehicle>().heading) * Mathf.Deg2Rad).ToString();
        //     // string angleV = ((360f - V.GetComponent<vehicle>().heading) * Mathf.Deg2Rad).ToString();
        //     // string speedM = M.GetComponent<vehicle>().speed.ToString();
        //     // string speedV = V.GetComponent<vehicle>().speed.ToString();
        //     // string ans = "0";

        //     // string textToAppend = positionX + "," + positionY + "," + angleM + "," + angleV + "," + speedM + "," + speedV + "," + ans;


        //     // // Open the file in append mode using StreamWriter
        //     // StreamWriter writer = File.AppendText(filePath);
        //     // Debug.Log(textToAppend);

        //     // // Write the text to append to the file
        //     // writer.WriteLine(textToAppend);

        //     // writer.Close();
        // }
        // else if (other.tag == "Vehicle")
        // {

        string positionX = (M.GetComponent<vehicle>().x - V.GetComponent<vehicle>().x).ToString();
        string positionY = (M.GetComponent<vehicle>().y - V.GetComponent<vehicle>().y).ToString();
        string angleM = ((360f - M.GetComponent<vehicle>().heading) * Mathf.Deg2Rad).ToString();
        string angleV = ((360f - V.GetComponent<vehicle>().heading) * Mathf.Deg2Rad).ToString();
        // string angleV = ((360f - V.GetComponent<vehicle>().heading) * Mathf.Deg2Rad).ToString();
        string speedM = M.GetComponent<vehicle>().speed.ToString();
        string speedV = V.GetComponent<vehicle>().speed.ToString();
        string ans = "1";

        string textToAppend = positionX + "," + positionY + "," + angleM + "," + angleV + "," + speedM + "," + speedV + "," + ans;

        // Open the file in append mode using StreamWriter
        Debug.Log(textToAppend);

        // Write the text to append to the file
        writer.WriteLine(textToAppend);

        // }


        // Reload the current scene by its name or index
        SceneManager.LoadScene(currentScene.name);
    }


    private void OnApplicationQuit()
    {
        if(this.name == "V")
            return;
        Debug.Log("CLOSE");
        writer.Close();
    }

    // string ipAddress = "127.0.0.1";
    // int port = 8889;

    // // Create a TCP client and connect to the Python server
    // TcpClient client = new TcpClient();
    // client.Connect(ipAddress, port);

    // // Get the network stream for sending data
    // NetworkStream stream = client.GetStream();

    // // Convert the data to send into a byte array
    // string dataToSend = "Hello, Python!";
    // byte[] data = Encoding.ASCII.GetBytes(dataToSend);

    // // Send the data
    // stream.Write(data, 0, data.Length);

    // // Close the stream and client
    // stream.Close();
    // client.Close();

    //     Scene currentScene = SceneManager.GetActiveScene();
    //     // Reload the current scene by its name or index
    //     SceneManager.LoadScene(currentScene.name);


}

