using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    public string portName = "COM4";
    public int baudRate = 9600;

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;

    void Awake()
    {
        Open();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isNewMessageReceived_)
        {
            OnDataReceived(message_);
            Debug.Log(message_);
        }
        if (Shot.Getblinkled()) //LEDì_ìîÇ∑ÇÈéû
        {
            serialPort_.Write("H");
            Invoke("Resetled", 3); //3ïbä‘ì_ìîÇµÇΩÇÁè¡ìî
        }
        else if (!Shot.Getblinkled()) //LEDì_ìîÇµÇ»Ç¢éû
        {
            serialPort_.Write("L");
        }
    }

    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        serialPort_.Open();

        isRunning_ = true;

        thread_ = new Thread(Read);
        thread_.Start();

        serialPort_.DtrEnable = true;
        serialPort_.RtsEnable = true;
    }

    private void Close()
    {
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            try
            {
                //if (serialPort_.BytesToRead > 0)
                message_ = serialPort_.ReadLine();
                isNewMessageReceived_ = true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public void Write(string message)
    {
        try
        {
            serialPort_.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    public void Resetled() //LEDÇè¡ìîÇ∑ÇÈ
    {
        Shot.Resetled();
    }
}
