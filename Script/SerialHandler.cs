using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour {

	public delegate void SerialDataRecievedEventHandler(string message);//別の場所で処理.
	public event SerialDataRecievedEventHandler onDataRecieved;

	private SerialPort serialPort;
	private Thread thread;
	private bool isRunning = false;

	public string message;
	private bool isNewMessageRecieved = false;

	void Awake(){
		Open();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(isNewMessageRecieved){
			onDataRecieved(message);
		}
		//Debug.Log(isRunning);
	}

	void OnDestroy(){
		Close();
	}

	void Open(){
//		foreach(string str in SerialPort.GetPortNames())
//		{
//			Debug.Log(string.Format("Existing COM port: {0}"+str));
//		}
		if(Data_holder.port_name == "Port Name") return;
		serialPort = new SerialPort(Data_holder.port_name,9600,Parity.None,8,StopBits.One);
		if(serialPort == null) return;
		serialPort.Open();

		isRunning = true;


		thread = new Thread(Read);
		thread.Start ();
	}

	void Close(){
		isRunning = false;

		if(thread != null && thread.IsAlive){
			thread.Join(); //呼び出し元のスレッドをブロック.
		}

		if(serialPort != null && serialPort.IsOpen){
			serialPort.Close();
			serialPort.Dispose();
		}

	}

	private void Read(){
		while(isRunning && serialPort != null && serialPort.IsOpen){
			try{
				//if(serialPort.BytesToRead > 0){
					message = serialPort.ReadLine(); //ここで読み込み.
					isNewMessageRecieved = true;
					//Debug.Log(message);
				//}
			}catch(System.Exception error){ //例外.
				Debug.LogWarning(error.Message);
			}
		}
	}

	public void Write(string message){
		try{
			serialPort.Write(message); //ここで書き込み.
		}catch(System.Exception error){ //例外.
			Debug.LogWarning(error.Message);
		}
	}
}
