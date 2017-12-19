///////////////////////////////////////////////////////////////////////////////////////////////////
//Executive.cs - Demonstration of Requirements for Project 3 Remote Build Server Prototypes      //
//                                                                                               //
// Author: Repaka RamaTeja,rrepaka@syr.edu                                                       //
// Application: CSE681 Project 3-Remote Build Server Prototypes                                  //
// Environment: C# console                                                                       //
///////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 *  Demonstrates all the project3: Remote Build Server Prototypes Requirements in a clear and  understandable way
 * 
 * Public Interface
 * ----------------
 * This does not contain any public methods
 *   
 * Required Files:
 * ---------------
 * Executive.cs
 * 
 * Build Process
 * -------------
 * Required files:   Executive.cs
 * Compiler command: csc Executive.cs
 * 
 * Maintenance History:
 * --------------------
 * ver 1.0 : 26 Oct 2017
 * - first release
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExecutive
{

    class Executive
    {
        //<---------------- function used for demonstrating requirement one----------------------->
        private void demonstrateRequirement1()
        {
            Console.WriteLine("--------------------------------Test Executive started-------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Demonstration of Requirement 1");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Implemented using C# langauge as you can see all .cs files in the SMAproject3 folder");
            Console.WriteLine("\n");
            Console.WriteLine("Using C# Framework Version" + Environment.Version.ToString());
            Console.WriteLine("\n");
            Console.WriteLine("Implemented code using visual studio as you can see the solution file SMAproject3.sln in the SMAproject3 folder");
            Console.WriteLine("\n");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
        }

        //<----------------- function used for demonstrating requirements from 2 to 3----------------------------->
        private void demonstrateRequirementupto3()
        {
            Console.WriteLine("Demonstration of Requirement 2");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Included a Message Passing service built using WCF which can be found at location \n  "+ "  SMAproject3/MessagePassingCommService    SMAproject3/IService");
            Console.WriteLine("\n");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Demonstration of Requirement 3");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Mother Builder and Core Builders(childs) are communicating each other using the MessagePassingCommService");
            Console.WriteLine("Observation  from Mother Builder Console, CoreBuilders(child) Console and Mock Repository Console are:");
            Console.WriteLine("Mother builder sends Two types of Requests to Core Builders(child)");
            Console.WriteLine("1.BuildRequest using MessagePassingCommService");
            Console.WriteLine("2.ReadyRequest to know the status of Builder using MessagePassingCommService");
            Console.WriteLine("Core Builders processes the Requests");
            Console.WriteLine("If it is a Ready request then it sends ready response using MessagePassingCommService");
            Console.WriteLine("If it is a Build request then it asks for xml file specfied in the build request from Mock repository");
            Console.WriteLine("Mock Repository sends the BuildRequest xml file using MessagePassingCommService to Corresponding Core Builder by \n using its recieved Comm message");
            Console.WriteLine("Mock Repository also sends Acknowledgement message file sent to Core Builder");
            Console.WriteLine("So pool process and Mother builder will be using MessagePassingCommService to transfer BuildRequests and Files  \n which clearly shows Requirement 3");
            Console.WriteLine("\n");
        }

        //<--------------- function used for demonstrating Requirement 4------------------------------------->
        private void Requirement4()
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Demonstration of Requirement 4");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Included a Process Pool component Mother Builder which creates No of child processes on Command");
            Console.WriteLine("\nMother Builder can be found at the location SMAproject3/MotherBuilder\n");
        }
        //<------------- function used for demonstrating Requirement 5 and 6--------------------------------------->
        private void Requirement5to6()
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Demonstration of Requirement 5");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Observations from  Mother builder and Core Builder(child) Consoles");
            Console.WriteLine("Core Builders are using the MessagePassingCommService prototype to access messages from MotherBuilder");
            Console.WriteLine("You can clearly see the flow of messages from: ");
            Console.WriteLine("Mother builder to Core Builder");
            Console.WriteLine("Core builder to Mother Builder");
            Console.WriteLine("This continues until shutdown");
            Console.WriteLine("Observe the Mother builder sending Shutdown message to Core builders");
            Console.WriteLine(" Core builder console you can notice shut down message sent From BuildServer");
            Console.WriteLine("From now you can't see any messages from Build Builder to Core builders");
            Console.WriteLine("This clearly demonstrates Requirement 5\n");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Demonstration of Requirement 6");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Included a TestRequestBuilder Package which is built using WPF \n which could be found at location :  SMAproject3/TestRequestBuilder");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------\n\n");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!      Note      !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n\n");
            Console.WriteLine("--------------------To Demonstrate GUI requirements --------------------------\nI have to use delays so please wait for 2-3 minutes to clearly observe communication");
            Console.WriteLine("I automated the gui process for creation of Build requests");
            Console.WriteLine(" creation of process pool");
            Console.WriteLine(" shutdown of process pool");
            Console.WriteLine("validating the process pool shutdown by sending messages from gui to mock and then to build server");
            Console.WriteLine("If process pool is ready state then forwards to Core builders otherwise it stores in build request queue");
            Console.WriteLine("This can be done by using sendtomock button which forwards the request from  mock repo to build server");

        }

        //<------------- function used for demonstrating Requirement 6 to 9--------------------------------------->
        private void requirementupto9()
        {
            Console.WriteLine("\n--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Demonstration of Requirement 7");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Observation from the WPF application");
            Console.WriteLine("Navigate to WPF application");
            Console.WriteLine("Open process pool tab");
            Console.WriteLine("There is an input text box by which you can provide the number of builders to be started");
            Console.WriteLine("User can click on createpool button then Corresponding number of builders will be created");
            Console.WriteLine("I automated the process using window onload function");
            Console.WriteLine("You can see the 3 in the Input text box so three Child Builders are opened");
            Console.WriteLine("you can see the shutdown pool button which shutdown all the process");
            Console.WriteLine("you can see the Core builders consoles which contains shutdown messages at the end");
            Console.WriteLine("Now if you try to send any Build request message to Mother builder it will not send to core builders because they are in shutdown state");
            Console.WriteLine("provided a text input by which user enter build request name");
            Console.WriteLine("When user clicks on sendtomock button if processpool is runnning then build server will be forwarding the messages to Core builders if not they will not forward");
            Console.WriteLine("This clearly shows the requirement 7 and also provides conformation of shutdown pool process by using the sendtorepo button");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Demonstration of Requirement 8");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("If you see the RequestBuilder tab of Wpf application you can clearly see the how the build request is generated and where it is saved");
            Console.WriteLine("Build Request generated could be found at SMAproject3/MockClient");
            Console.WriteLine("this clearly shows the demonstration of requirement 8");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Demonstration of Requirement 9");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Include three prototypes into a single functional Visual Studio Solution, with a Visual Studio project for each\n");
        }


        //<------------------------------------------TEST STUB----------------------------------------------------->
#if (Test_Executive)
       //driver logic
    static void Main(string[] args)
    {
            Executive exec = new Executive();
            exec.demonstrateRequirement1();
            exec.demonstrateRequirementupto3();
            exec.Requirement4();
            exec.Requirement5to6();
            exec.requirementupto9();
    }
    #endif
    }
}
