AGL Code Exercise ReadMe

Please find attached a .Net solution that meets the specified requirements i.e. delivers the requested/ expected output - whereby for each gender (male then female) the cat names are listed and ordered by name based on the data returned from the specified API.

The solution has been created inline with the SOLID principles and written in a Clean Code style.

As requested various libraries have been used for example:

- .Net WebRequest (optimised to improve get performance)
- LINQ - to sort and order the results from the API
- Use of App.config to store configurable settings (for ease of maintainabilty) 
- SEQ and Serilog for optional logging

Some explanatory notes have been added within the code - simply search Notes and they can be found.

Regarding testing, due to time constraints I have been unable to get this coded - however as can be seen I was intending to use NUnit in conjunction with FakeItEasy; and had broken the code into separate pieces where dependencies can be injected; to make the code testable
and to keep each test's code focussed on thing being tested rather than setting up non-pertinent dependencies that would otherwise affect the easy of test maintainibilty. 

If there are any questions then please feel free to contact me.

Kind Regards,

Paul Kernaghan
pgkernaghan@hotmail.com
0402727796

P.S. Note: IMO It's been a guess of judging/ balancing what is being sought beyond what was specified- simplicity where adequate vs demonstrating skills more appropriate in larger solutions.
As a result I've aimed to deliver a solution inline with my normal coding style and good practices more apt for larger solutions - instead of focussing on brevity or simple structure; with the thinking/ coding approach focussed on maintainablity, readability and extensibility/ robust to change.