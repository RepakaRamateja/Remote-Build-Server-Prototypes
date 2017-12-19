///////////////////////////////////////////////////////////////////////////////////////////////////
// MockRepo.cs - Demonstrate RepoMock  operations                                                //
//                                                                                               //
// Author: Repaka RamaTeja,rrepaka@syr.edu                                                       //
// Application: CSE681 Project 3-Remote Build Server Prototypes                                  //
// Environment: C# console                                                                       //
///////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * ====================
 * Demonstrates Mock Repo  operations like  listenforincomingmessages , sending Mother builder BuildRequest messages
 * and handlingincomingmessages
 * 
 *  Public Interface
 * ----------------
 * MockRepo repo = new MockRepo(); //for doing mock repo operations 
 * repo.sendrequest(); //sending the request to the Mother Builder
 * repo.handleincomingmessages();//handles incoming messages to Mock Repository
 * repo.sendbuildermessage();//sends reply messages to the sender i.e is here Mother builder
 * repo.listenforincomingmessages();//used to create a  thread for handling incoming messages
 * 
 * Required Files:
 * ---------------
 * MockRepo.cs, MPCommService.cs, IMPCommService.cs
 * 
 *  Build Process
 * --------------
 * Required files: MockRepo.cs, MPCommService.cs, IMPCommService.cs
 * Compiler command: csc  MockRepo.cs MPCommService.cs IMPCommService.cs
 * 
 * Maintenance History:
 * --------------------
 *ver 1.0 : 26 Oct 2017
 * - first release
 */
using MessagePassingComm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace MockRepository
{
    //class which contains operations related to MockRepo like handling incoming messages, sending requests
    class MockRepo
    {
        Comm comm = null;
        private static int startport { get; set; } = 0;
        private static string repoaddress { get; set; } = null;
        private static string serveraddress { get; set; } = null;
        Thread msgHandler;
        //<-------------constructor which takes mock repo starting port and serverport------------------>
        MockRepo(int startport, int serverport)
        {
            comm = new Comm("http://localhost", startport);
            repoaddress = "http://localhost:" + startport.ToString() + "/IMessagePassingComm";
            serveraddress = "http://localhost:" + serverport.ToString() + "/IMessagePassingComm";
        }

        //<--------------function used for sending the request to the Mother Builder-------------------->
        public void sendrequest(string path)
        {
            string[] fileList = Directory.GetFiles(path, "*.*");
           
            for (int i = 0; i < fileList.Length; i++)
            {
                Console.WriteLine("Sending the BuildRequest Message to Mother Builder: "+ serveraddress);
                Console.WriteLine("\n");
                CommMessage csndMsg = new CommMessage(CommMessage.MessageType.request);
                csndMsg.command = "BuildRequestFileName";
                csndMsg.author = "RamaTeja";
                string filename = Path.GetFileName(fileList[i]);
                csndMsg.filename = filename;
                csndMsg.to = serveraddress;
                csndMsg.from = repoaddress;
                this.comm.postMessage(csndMsg);
            }
        }
        //<--------------function that handles incoming messages to Mock Repository--------------------->
       public  void handleincomingmessages()
        {
            while (true)
            {
                CommMessage msg = comm.getMessage();
                if (msg.command == "Please send the specified file in the Message")
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Mock Repository recieved message from  "+ msg.from);
                    msg.show(); Console.WriteLine("\n");
                    Console.WriteLine("Mock repository processing the Request"); Console.WriteLine("\n");
                    bool val = this.comm.sendfile(msg.filename, msg.from);
                    Console.WriteLine("File Transfer status  " + val);
                    Console.WriteLine("\n");
                    sendbuildermessage(msg);
                }

                if(msg.command =="BuildRequestFileName")
                {
                    Console.WriteLine("Mock repo recived request from Mock client");
                    Console.WriteLine("\n");
                    msg.show();
                    Console.WriteLine("forwarding the request to Build server");
                    CommMessage csndMsg = msg;
                    csndMsg.to = serveraddress;
                    csndMsg.from = repoaddress;
                    this.comm.postMessage(csndMsg);
                    csndMsg.show();
                }
            }
        }

        //<--------------function that sends reply messages to the sender i.e is here Mother builder------>
       public void sendbuildermessage(CommMessage msg)
        {
            Console.WriteLine("Sending Reply Message to "+ msg.from);
            CommMessage csndMsg = new CommMessage(CommMessage.MessageType.request);
            csndMsg.command = "filesent";
            csndMsg.author = "RamaTeja";
            csndMsg.filename = msg.filename;
            csndMsg.to = msg.from;
            csndMsg.from = msg.to;
            this.comm.postMessage(csndMsg);
            csndMsg.show();
        }

        //<-----function used to create a  thread for handling incoming messages----------------->
        public void listenforincomingmessages()
        {
            msgHandler = new Thread(handleincomingmessages);
            msgHandler.Start();
        }

 //<------------------------------------------TEST STUB----------------------------------------------------->
#if (TEST_MockRepo)
          
        //Driver Logic
        static void Main(string[] args)
        {
            int startport = int.Parse(args[0]);
            int serverport = int.Parse(args[1]);
            MockRepo mock = new MockRepo(startport, serverport);
            string repopath = "../../../MockRepository/RepoStorage";
            Console.WriteLine("-----------------------------Start of Mock Repository at " + args[0] + " -------------------------------------");
            Console.WriteLine("\n");
            mock.sendrequest(repopath);
            Console.WriteLine("Listens for incoming Comm messages\n");
            mock.listenforincomingmessages();
        }
#endif

    }
}




