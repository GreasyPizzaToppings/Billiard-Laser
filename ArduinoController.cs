using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;

public class ArduinoController : IDisposable
{
    private SerialPort serialPort;
    private const string LASER_OFF = "0";
    private const string LASER_ON = "1";
    private const string LEFT = "l";
    private const string RIGHT = "r";
    private const string UP = "u";
    private const string DOWN = "d";
    private const string STEPAMOUNT = "s";
    private const string HANDSHAKE_REQUEST = "h";
    private const string EXPECTED_HANDSHAKE_RESPONSE = "ARDUINO_LASER";
    private bool laserOn = false;
    private bool isHandshakeVerified = false;

    private TaskCompletionSource<bool> connectionTaskSource;

    public Task ConnectionTask => connectionTaskSource?.Task ?? Task.CompletedTask;
    public bool IsLaserOn { get => laserOn; }
    public bool IsConnected { get => isHandshakeVerified && (serialPort?.IsOpen ?? false); }
    public string PortName { get => serialPort?.PortName; }

    public ArduinoController()
    {
        connectionTaskSource = new TaskCompletionSource<bool>();
        
        // Start async connection
        Task.Run(AutoConnectAsync);
    }

    private async Task AutoConnectAsync()
    {
        try
        {
            string[] availablePorts = SerialPort.GetPortNames();

            foreach (string port in availablePorts)
            {
                if (await Task.Run(() => TryConnect(port)))
                {
                    Console.WriteLine($"Successfully connected to Arduino on port {port}");
                    connectionTaskSource.SetResult(true);
                    return;
                }
            }

            connectionTaskSource.SetException(new Exception("Could not find Arduino device on any available port"));
        }
        catch (Exception ex)
        {
            connectionTaskSource.SetException(ex);
        }
    }

    private bool TryConnect(string portName, int baudRate = 9600, int timeout = 1000)
    {
        try
        {
            // Clean up existing connection if any
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                    serialPort.Close();
                serialPort.Dispose();
            }

            // Create new connection
            serialPort = new SerialPort(portName, baudRate)
            {
                ReadTimeout = timeout,
                WriteTimeout = timeout
            };

            serialPort.Open();
            
            Thread.Sleep(250); // Allow Arduino to reset after connection

            // Send handshake request
            serialPort.WriteLine(HANDSHAKE_REQUEST);

            // Read response
            string response = serialPort.ReadLine().Trim();

            if (response == EXPECTED_HANDSHAKE_RESPONSE)
            {
                isHandshakeVerified = true;
                return true;
            }

            // If handshake failed, clean up
            serialPort.Close();
            serialPort.Dispose();
            serialPort = null;
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect on port {portName}: {ex.Message}");
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                    serialPort.Close();
                serialPort.Dispose();
                serialPort = null;
            }

            isHandshakeVerified = false;
            return false;
        }
    }

    private async void SendCommand(string command)
    {
        try
        {
            if (IsConnected) serialPort.WriteLine(command);
            else Console.WriteLine("Arduino is not connected or the port is closed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending command: {ex.Message}");
            throw;
        }
    }

    // Existing methods remain the same
    public void LaserOn() => SendCommand(LASER_ON);
    public void LaserOff() => SendCommand(LASER_OFF);
    public void ToggleLaser()
    {
        laserOn = !laserOn;
        if (laserOn) LaserOn();
        else LaserOff();
    }
    public void MoveLeft() => SendCommand(LEFT);
    public void MoveRight() => SendCommand(RIGHT);
    public void MoveUp() => SendCommand(UP);
    public void MoveDown() => SendCommand(DOWN);

    public void SetStepAmount(int amount)
    {
        SendCommand(STEPAMOUNT + amount.ToString());
        if (IsConnected)
        {
            Console.WriteLine(serialPort.ReadLine()); //response
        }
    }

    public void Dispose()
    {
        if (serialPort != null)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            serialPort.Dispose();
        }
    }
}