Please run run.bat as adminstrator just a reminder

Mock repo  contains two command line arguments  

1) Mock repo port number

2) Server port number


Mother builder contains two arguments

1)Child process start port number

2)Mock Repo port number

----------------------------------Note:--------------------------------------------------------------------

I automated the gui process to demonstrate the requirements clearly i introduced delays

so please wait for 2-3 minutes after gui pops up and starts executing messages 

In the automated process 

Iam generating two buildrequests first

which could be found at SMAproject3\MockClient

then iam creating 3 pool process by entering 3 in the input and clicking create process pool

which clearly shows all the communication both messages and files using WCF

Now

I introduced delays to clearly show shut down process requirement

please wait for 2-3 minutes to see everthing clearly

delay 1 added here

after the process pool is created i am sending build request from the gui using sendtomock button

then mother builder will forward to the corresponding child builder basing on ready queue request 

delay 2 added here

then i am shutting down the process through shutdown process pool button

then observe the child core builders consoles to see the shutdown messages

delay 3 added here

then iam again sending build request to build server from sendtomock which forwards from mockrepo to mother builder

so now as the pool is in shutdown state it will not forward the messages to child core builders 

hope this clearly demonstrates the requirements




