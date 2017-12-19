/////////////////////////////////////////////////////////////////////////////////////////////////////
// MainWindow.xaml.cs - Demonstrate all GUI related onclickhandler and window handler operations   //
//                                                                                                 //
// Author: Repaka RamaTeja,rrepaka@syr.edu                                                         //
// Application: CSE681 Project 3-Remote Build Server Prototypes                                    //
// Environment: C# console                                                                         //
/////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * ---------------------
 * Demonstrates operations for starting the process pool, sending BuildRequest messages to MockRepository
 * It also provides operations for creation of build request by selecting test drivers and source files
 * Finally shutting down the process pool by specifying no of processes through input textbox
 * 
 * 
 * Public Interface
 * ------------------
 * It does not contain any public methods
 * But only one public constructor MainWindow --- which initializes the main component through method InitializeComponent
 * Required Files:
 * ---------------
 * BuildRequest.cs,App.xaml,MainWinodw.xaml,MainWinodw.xaml.cs
 * 
 * Build Process
 * ---------------
 * Required files:   App.xaml,MainWinodw.xaml,MainWinodw.xaml.cs,BuildRequest.cs
 * Compiler command: csc App.xaml MainWinodw.xaml MainWinodw.xaml.cs BuildRequest.cs
 * 
 * Maintenance History:
 * --------------------
 * ver 1.0 : 26 Oct 2017
 * - first release
 * 
 */
using Build_Request;
using MessagePassingComm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Utilities;
using WinForms = System.Windows.Forms;


namespace TestRequestBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> // class responsible for all the creating all the GUI
    public partial class MainWindow : Window
    {
        BuildRequest buildrequest = new BuildRequest();

        Comm comm = new Comm("http://localhost", 8070);

        private string buildserveraddress { get; set; } = "http://localhost:8090/IMessagePassingComm";

        private string clientaddress { get; set; } = "http://localhost:8070/IMessagePassingComm";

        private string MockRepoaddress { get; set; } = "http://localhost:8080/IMessagePassingComm";

        //constructor which initializes the mainwindow through method InitializeComponent 
        public MainWindow()
        {
            InitializeComponent();
        }

        //<------function i.e window Loaded Event handler which automates the Requirements demonstration with out manual input---------------> 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Createbuildrequest( sender, e); //creates build request
            Initializeprocesspool(sender, e); //creates the process pool
            Initializesendtomock(sender, e); // send request to mock repo
            Initializepoolshutdown(sender,e); //shutting down the process pool
        }
       
        //<------  automated async function i.e used to demonstrate creating process pool with out manual input
        private async void Initializepoolshutdown(object sender, RoutedEventArgs e)
        {
            await Task.Delay(40000); //added delay to show requirements clearly
            quitmessage(sender, e);
            await Task.Delay(20000);
            Console.WriteLine("sending Mock Repo a request After Shutting down Process Pool");
            Inputfilename.Text = "BuildRequest2.xml";
            sendtomock(sender, e);

        }
        
        //<--- aysnc function which is used to send build requests to mock repository before shutting down process pool
        private async void Initializesendtomock(object sender, RoutedEventArgs e)
        {
            
            await Task.Delay(30000);
            Console.WriteLine("sending Mock Repo a request before Shutting down Process Pool");
            Inputfilename.Text = "BuildRequest1.xml";
            sendtomock(sender, e);
        }

        //<---- function which is used to demonstrate the  create process pool requirement with out manual input------------->
        private void Initializeprocesspool(object sender, RoutedEventArgs e)
        {
            inputnumber.Text = "3";
            createpool(sender, e);

        }

        //<--- function used for demonstarting build request generation with out manual input--------->
        private void Createbuildrequest(object sender, RoutedEventArgs e)
        {
            driverlist.Items.Add("TestDriver1.cs");
            filelist.Items.Add("SourceFile1.cs");
            filelist.Items.Add("SourceFile2.cs");
            filename.Text = "TestBuildRequest1.xml";
            generaterequest(sender, e);
            savecontents(sender, e);
            cleartextblock(sender, e);
            driverlist.Items.Add("TestDriver2.cs");
            filelist.Items.Add("SourceFile3.cs");
            filelist.Items.Add("SourceFile4.cs");
            filename.Text = "TestBuildRequest2.xml";
            generaterequest(sender, e);
            savecontents(sender, e);
        }

        //<------ A event handler for selectsource button which opens a dialog box to select directory
        private void getsourcefiles(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            WinForms.DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                filelist.Items.Clear();
                String sPath = folderDialog.SelectedPath;
                DirectoryInfo folder = new DirectoryInfo(sPath);
                if (folder.Exists)
                {
                    foreach (FileInfo fileInfo in folder.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        filelist.Items.Add(fileInfo.Name);
                    }

                }
            }

        }

        //<------ A event handler for the show selected button which shows only selected items in the listbox
        private void getselectedsource(object sender, RoutedEventArgs e)
        {
            List<object> filenames = new List<object>();
            foreach (var item in filelist.SelectedItems)
            {
                filenames.Add(item);
            }
            filelist.Items.Clear();
            foreach (object obj in filenames)
            {
                filelist.Items.Add(obj);
            }
        }
       
        //<----A event handler for the generate button which generates build request from the values in the listbox
        private void generaterequest(object sender, RoutedEventArgs e)
        {
            BuildItem element = new BuildItem();
            List<object> filenames = new List<object>();
            foreach (var item in filelist.Items)
            {
                filenames.Add(item);
            }
            List<object> drivers = new List<object>();
            foreach (var item in driverlist.Items)
            {
                drivers.Add(item);
            }

            foreach (string item in drivers)
            {
                file one = new file();
                one.name = item;
                element.addDriver(one);
            }

            foreach (string item in filenames)
            {
                file one = new file();
                one.name = item;
                element.addCode(one);  
            }
            buildrequest.Builds.Add(element);
            string xml = buildrequest.ToXml();
            Console.WriteLine("The build Request generated is");
            Console.WriteLine("\n");
            Console.WriteLine(xml);
            Console.WriteLine("\n");
            XmlTextBlock.Text = xml;
        }

        //<------ A event handler for select driver button which opens a dialog box to select directory
        private void getdriver(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            WinForms.DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                driverlist.Items.Clear();
                String sPath = folderDialog.SelectedPath;
                DirectoryInfo folder = new DirectoryInfo(sPath);
                if (folder.Exists)
                {
                    foreach (FileInfo fileInfo in folder.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        driverlist.Items.Add(fileInfo.Name);
                    }
                }
            }

        }

        //<------ A event handler for the show selected button which shows only selected items in the listbox
        private void showselecteddrivers(object sender, RoutedEventArgs e)
        {
            List<object> filenames = new List<object>();
            foreach (var item in driverlist.SelectedItems)
            {
                filenames.Add(item);
            }

            driverlist.Items.Clear();

            foreach (object obj in filenames)
            {
                driverlist.Items.Add(obj);
            }


        }

        //<----- A event handler for the clear button which clears all the values in the GUI elements
        private void cleartextblock(object sender, RoutedEventArgs e)
        {
            XmlTextBlock.Text = "Xml Contents";
            filelist.Items.Clear();
            driverlist.Items.Clear();
            filename.Text = "Enter file name";
            buildrequest = null;
            buildrequest = new BuildRequest();
        }

        //<--------- A event handler function for save contents button which saves files MockClient Location
        private void savecontents(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("\nSaving the contents\n");
            string name = filename.Text;
            string content = XmlTextBlock.Text;
            string path = "../../../MockClient/" + name;
            File.WriteAllText(path, content);
            Console.WriteLine("File could be found at the path as " + path);
        }

        //<------- A event handler function for the Create Pool which creates process pool
        private void createpool(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("\nSending create process pool message\n");
            var number=inputnumber.Text;
            int n = int.Parse(number);
            
            CommMessage csndMsg = new CommMessage(CommMessage.MessageType.request);
            csndMsg.command = "processpool";
            csndMsg.author = "RamaTeja";
            csndMsg.to = buildserveraddress;
            csndMsg.from = clientaddress;
            csndMsg.Noofprocess = n;
            comm.postMessage(csndMsg);
            csndMsg.show();


        }

        //<----- A event handler function for the shutdown pool button which sends shutdown comm messages
        private void quitmessage(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("\nSending Quit message\n");
            var number = inputnumber.Text;
            int n = int.Parse(number);
            CommMessage csndMsg = new CommMessage(CommMessage.MessageType.request);
            csndMsg.command = "quit";
            csndMsg.author = "RamaTeja";
            csndMsg.Noofprocess = n;
            csndMsg.to = buildserveraddress;
            csndMsg.from = clientaddress;
            this.comm.postMessage(csndMsg);
        }

        //<------A event handler function for the sendtomock button which sends Comm messages to Mock Repository
        private void sendtomock(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("\nsending BuildRequest to Mock Repository\n");
            string filename = Inputfilename.Text;
            CommMessage csndMsg = new CommMessage(CommMessage.MessageType.request);
            csndMsg.command = "BuildRequestFileName";
            csndMsg.author = "RamaTeja";
            csndMsg.filename = filename;
            csndMsg.to = MockRepoaddress;
            csndMsg.from = clientaddress;
            this.comm.postMessage(csndMsg);
        }
    
    }
}
