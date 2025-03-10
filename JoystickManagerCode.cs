using System.IO.Ports;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    private static JoystickManager instance;
    private SerialPort serialPort;
    public string portName = "COM3"; 
    public int baudRate = 9600;
    
    private string lastDirection = "";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSerial();
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void CloseSerialPort()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.BaseStream.Close(); 
            serialPort.Close();
            
            serialPort.Dispose(); 
            serialPort = null;    
        }
    }


    public void InitializeSerial()
    {
        CloseSerialPort(); 

        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 50;
            Debug.Log("Serial port opened successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }


    public static string ReadJoystick()
    {
        if (instance == null || instance.serialPort == null || !instance.serialPort.IsOpen)
            return "";

        try
        {
            string direction = instance.serialPort.ReadLine().Trim();
            instance.lastDirection = direction;
            return direction;
        }
        catch (System.Exception)
        {
            return ""; 
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
