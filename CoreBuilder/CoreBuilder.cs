///////////////////////////////////////////////////////////////////////////////////////////////////
//CoreBuilder.cs - Demonstrate Serialization and DeSerialization on BuildServer Data structures  //
//                                                                                               //
// Author: Repaka RamaTeja,rrepaka@syr.edu                                                       //
// Application: CSE681 Project 3-Remote Build Server Prototypes                                  //
// Environment: C# console                                                                       //
///////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 *  Demonstrates operations like handling build requests sent from Mother builder,
 *  requesting for files from Mock Repository,Communicating with Comm messages and files by using WCF
 * 
 * Public Interface
 * ----------------
 *   CoreBuilder builder = new CoreBuilder(port,mockport)  // corebuilder constructor which takes two arguments CoreBuilder port number and Mockrepository port number
 *   builder.handlebuildrequest(string filename);//function responsbible for handling BuildRequest
 *   builder.sendReadyMessage(string tosend);//function responsible for sending ready messages to MotherBuilder
 *   builder.handlebuilds(CommMessage msg);//function responsible for handling builds by sending file requests to the Mock Repository
 *   builder.startchildprocess();//function that is responsible for creating a thread which keeps on listening for incoming messages
 *   
 * Required Files:
 * ---------------
 * CoreBuilder.cs , MPCommService.cs, IMPCommService.cs
 * 
 * Build Process
 * -------------
 * Required files:   CoreBuilder.cs , MPCommService.cs, IMPCommService.cs
 * Compiler command: csc CoreBuilder.cs  MPCommService.cs IMPCommService.cs
 * 
 * Maintenance History:
 * --------------------
 * ver 1.0 : 26 Oct 2017
 * - first release
 * 
 */
using MessagePassingComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreBuilder
{
    //class that has operations related to core builder like handlebuildrequest ,sendreadymessages etc
   public class CoreBuilder
    {
        string myAddress { get; set; } = null;
        string mockrepoaddress { get; set; } = null;
        Comm comm;
        Thread msgHandler;
        private static int startport { get; set; } = 0;

        //<----------Constructor which takes Corebuilder port number and the mock Repository Port number------>
       public CoreBuilder(int startport, int mockport)
        {
            comm = new Comm("http://localhost", startport);
            myAddress = "http://localhost:" + startport.ToString() + "/IMessagePassingComm";
            mockrepoaddress = "http://localhost:" + mockport.ToString() + "/IMessagePassingComm";
        }

        //<--------------- function that keeps on listening for incoming messages---------------> 
        private void HandlerThreadProc()
        {
            while (true)
            {
                CommMessage msg = comm.getMessage();
                if (msg.command == "Are u ready?????")
                {
                    Console.WriteLine("Message recieved from:  " + msg.from);
                    msg.show();
                    sendReadyMessage(msg.from);
                }

                if (msg.command == "BuildRequestFileName")
                {
                    Console.WriteLine("\n");
                    Console.WriteLine(" Build Request Message recieved from:  " + msg.from);
                    bool retstatus = handlebuilds(msg);
                    if (retstatus)
                        sendReadyMessage(msg.from);
                }

                if (msg.command == "filesent")
                {
                    Console.WriteLine("\nMessage recieved from Mock Repository\n");
                    msg.show();
                    handlebuildrequest(msg.filename);
                }

                if (msg.command == "quit")
                {
                    Console.WriteLine(" Message recieved from:  " + msg.from); Console.WriteLine("\n");
                    msg.show();
                    Console.WriteLine("shutting down the process");
                    Console.WriteLine("\n");
                    Console.WriteLine("Now you cant use the process again");
                    Console.WriteLine("\n");
                    break;
                }
            }
        }

        //<------------ function responsbible for handling BuildRequest ------------------------->
        void handlebuildrequest(string filename)
        {
            Console.WriteLine("\n");
            Console.WriteLine("The Recieved file contents are\n");
            Console.WriteLine("\n");
            string filecontent = System.IO.File.ReadAllText(ServiceEnvironment.fileStorage + "/" + filename);
            Console.WriteLine(filecontent);
            Console.WriteLine("\n");
        }

        //<------------ function responsible for sending ready messages to MotherBuilder---------------------->
        void sendReadyMessage(string tosend)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Processing completed.........");
            Console.WriteLine("\n");
            Console.WriteLine("Sending ready message to   " + tosend);
            Console.WriteLine("\n");
            CommMessage csndMsg = new CommMessage(CommMessage.MessageType.request);
            csndMsg.command = "I am Ready";
            csndMsg.author = "RamaTejaRepaka";
            csndMsg.to = tosend;
            csndMsg.from = myAddress;
            comm.postMessage(csndMsg);
            csndMsg.show();
        }

        //<--------function responsible for handling builds by sending file requests to the Mock Repository------------>
        Boolean handlebuilds(CommMessage msg)
        {
            msg.show();
            Console.WriteLine("\n");
            Console.WriteLine("Processing the message.......\n");
            CommMessage csndMsg = new CommMessage(CommMessage.MessageType.request);
            csndMsg.command = "Please send the specified file in the Message";
            csndMsg.author = "RamaTejaRepaka";
            csndMsg.to = mockrepoaddress;
            csndMsg.from = myAddress;
            csndMsg.filename = msg.filename;
            Console.WriteLine("Sending the request to Mock Repository");
            Console.WriteLine("\n");
            this.comm.postMessage(csndMsg);
            csndMsg.show(); Console.WriteLine("\n");
            return true;
        }

        //<-------- function that is responsible for creating a thread which keeps on listening for incoming messages --------->
        public void startchildprocess()
        {
            msgHandler = new Thread(HandlerThreadProc);
            msgHandler.Start();
        }
    }

        //<------------------------------------------TEST STUB----------------------------------------------------->
#if (Test_CoreBuilder)

        class TestCoreBuilder
        { 
        //driver logic
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------------Start of Core Builder"+args[0]+"  at"+" "+ args[1]+ "------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Listening for Messages ..........");
            Console.WriteLine("\n");
            int port = int.Parse(args[1]);
            int mockport = int.Parse(args[2]);
            CoreBuilder builder = new CoreBuilder(port,mockport);
            Console.Title = "Builder";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("\n Child Process Created");
            Console.Write("\n ====================");
            if (args.Count() == 0)
            {
                Console.Write("\n  please enter integer value on command line");
                return;
            }
            else
            {
                Console.Write("\nFrom child process #{0}\n\n", args[0]);   
                builder.startchildprocess();
                Console.WriteLine("\n");
            }
        }
#endif
        }
}
