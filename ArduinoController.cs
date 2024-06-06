using System;
using System.IO.Ports;

public class ArduinoController
{
    private SerialPort serialPort;

    private const string LASER_OFF = "0";
    private const string LASER_ON = "1";
    private const string LEFT = "l";
    private const string RIGHT = "r";
    private const string UP = "u";
    private const string DOWN = "d";


    public ArduinoController(string portName,
                             int baudRate = 9600)
    {
        Connect(portName, baudRate);
    }

    private void Connect(string portName, int baudRate)
    {
        try
        {
            this.serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
        }

        catch (Exception ex)
        {
            Console.WriteLine("Couldn't connect to arduino\n" + ex.Message);
        }
    }

    private void SendCommand(string command)
    {
        try
        {
            serialPort.WriteLine(command);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.GetBaseException().Message);
        }
    }

    public void LaserOn()
    {
        SendCommand(LASER_ON);
    }

    public void LaserOff()
    {
        SendCommand(LASER_OFF);
    }

    public void MoveLeft()
    {
        SendCommand(LEFT);
    }

    public void MoveRight()
    {
        SendCommand(RIGHT);
    }

    public void MoveUp()
    {
        SendCommand(UP);
    }

    public void MoveDown()
    {
        SendCommand(DOWN);
    }
}