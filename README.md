This is Software modelling analysis course project 


####################  Remote Build Server Prototypes  ################

Background Information:

In order to successfully implement big systems we need to partition code into relatively small parts and thoroughly test each of the parts before inserting them into the software baseline2. As new parts are added to the baseline and as we make changes to fix latent errors or performance problems we will re-run test sequences for those parts and, perhaps, for the entire baseline. Because there are so many packages the only way to make this intensive testing practical is to automate the process.


The process, described above, supports continuous integration. That is, when new code is created for a system, we build and test it in the context of other code which it calls, and which call it. As soon as all the tests pass, we check in the code and it becomes part of the current baseline. There are several services necessary to efficiently support continuous integration.


a Federation of servers, each providing a dedicated service for continuous integration.

The Federation consists of:


Repository:
Holds all code and documents for the current baseline, along with their dependency relationships. It also holds test results and may cache build images.


Build Server:
Based on build requests and code sent from the Repository, the Build Server builds test libraries for submission to the Test Harness. On completion, if successful, the build server submits test libraries and test requests to the Test Harness, and sends build logs to the Repository.



Test Harness:
Runs tests, concurrently for multiple users, based on test requests and libraries sent from the Build Server. Clients will checkin, to the Repository, code for testing, along with one or more test requests. The repository sends code and requests to the Build Server, where the code is built into libraries and the test requests and libraries are then sent to the Test Harness. The Test Harness executes tests, logs results, and submits results to the Repository. It also notifies the author of the tests of the results.



Client:
The user's primary interface into the Federation, serves to submit code and test requests to the Repository. Later, it will be used view test results, stored in the repository.





In this project, you will build prototypes for Process Pools, Socket-based Message-passing Communication, and for a Graphical User Interface (GUI), packages all needed for the softwrare modelling analysis final course project. These are relatively small "proof-of-concept" projects in which you experiment with design and implementation strategies.


So,  we will develop the prototypes:



Message-passing Communication Channel:
All members of the Federation3 use Message-Passing Communication, implemented with Windows Communication Foundation (WCF). The Process Pool members will also communicate with the mother Builder using WCF4.


Process Pool:
The build server may have very heavy work loads just before customer demos and releases. We want to make the throughput for building code as high as is reasonably possible. To do that the build server will use a "Process Pool". That is, a limited set of processes spawned at startup. The build server provides a queue of build requests, and each pooled process retrieves a request, processes it, sends the build log and, if successful, libraries to the test harness, then retrieves another request.
Malformed code may cause one of the processes to crash, perhaps by a circular set of C++ #include statements which overflow the process stack. This however, won't stop the Builder, which simply creates a new process replacement, and reports the build error to the repository. Note that the process pools will need to communicate with the mother Builder process. That's one use for the first prototype.
Each pooled process has the functionality of the Core Builder we constructed in Project Core builder



Graphical User Interface (GUI):
The Remote Builder will be accessed remotely from a GUI built using Windows Presentation Foundation (WPF). This prototype will be relatively simple5 step toward the final GUI used in Remote Build server


Requirements:


Shall be prepared using C#, the .Net Frameowrk, and Visual Studio 2017.


Shall include a Message-Passing Communication Service built with WCF. You are welcome to build on the Comm prototype demo.


The Communication Service shall support accessing build requests by Pool Processes from the mother Builder process, sending and receiving build requests, and sending and receiving files.


Shall provide a Process Pool component that creates a specified number of processes on command.

Pool Processes shall use Communication prototype to access messages from the mother Builder process. You may simply have them write the message contents to their consoles, demonstrating that they continue to access messages from the shared mother's queue, until shut down.


Shall include a Graphical User Interface, built using WPF.


The GUI shall provide mechanisms to start the main Builder (mother process), specifying the number of child builders to be started, and shall provide the facility to ask the mother Builder to shut down its Pool Processes. It may do that by sending a single quit message.


The GUI shall enable building test requests by selecting file names from the Mock Repository.


Your submission shall integrate these three prototypes into a single functional Visual Studio Solution, with a Visual Studio project for each.