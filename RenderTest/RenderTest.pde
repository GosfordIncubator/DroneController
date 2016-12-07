import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;

ServerSocket serverSocket;
InputStream input;
int[] grid;
int xLength = 0;
int yLength = 0;

void setup() {
  size(90,90);
  
  try {
  serverSocket = new ServerSocket(8002, 10);
  input = serverSocket.accept().getInputStream();
  
  byte[] lenBytes = new byte[4];
  input.read(lenBytes, 0, 4);
  int len = (((lenBytes[3] & 0xff) << 24) | ((lenBytes[2] & 0xff) << 16) |
            ((lenBytes[1] & 0xff) << 8) | (lenBytes[0] & 0xff));
  byte[] received = new byte[len];
  input.read(received, 0, len);
  xLength = received[0];
  yLength = received[1];
  grid = new int[xLength*yLength];
  } catch (IOException e) {
    System.out.println("Error");
  }
}

void draw() {
  try {
        byte[] lenBytes = new byte[4];
        input.read(lenBytes, 0, 4);
        int len = (((lenBytes[3] & 0xff) << 24) | ((lenBytes[2] & 0xff) << 16) |
                  ((lenBytes[1] & 0xff) << 8) | (lenBytes[0] & 0xff));
        byte[] received = new byte[len];
        input.read(received, 0, len);
        
        System.out.println(received.length);
        for (int i = 0; i < received.length; i++) {
          grid[i] = received[i];
        }
  } catch (IOException e) {
    System.out.println("Error");
  }
  
  int i = 0;
  for(int g : grid) {
    if (g == 0) fill(255);
    if (g == 1) fill(0);
    int xPos = i%xLength;
    int yPos = (i-xPos)/xLength;
    rect(xPos*30,yPos*30,width/xLength,height/yLength);
    i++;
  }
}

void close() {
  try {
   serverSocket.close(); 
  } catch (IOException e) {
    
  }
}