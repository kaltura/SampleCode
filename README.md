SampleCode
==========

This repository contiains 4 projects (in .NET, Java, Python and Ruby)
which can be used to interact with cielo24 public Web API.
Each folder contains contains source code for the core library,
a sample command line tool, compiled packages (.dll, .jar, .gem)
as well as some miscellaneous stuff (such as unit tests).

More detailed information about each project can be found below:

.NET
----
  
* Core library path:

    cielo24_.NET/Cielo24/bin/Release/Cielo24.dll (depends on Newtonsoft.JSON)
    cielo24_.NET/Cielo24/bin/Release/Cielo24Merged.dll (no dependencies)
    
* NuGet:

    Run the following command in the Package Manager Console in Visual Studio
    to install Cielo24 assembly as well as any dependencies:
      `Install-Package Cielo24`
      
    More information can be found here: https://www.nuget.org/packages/Cielo24/
    
* Command Line Interface path:

    cielo24_.NET/CommandLineTool/bin/Release/CommandLineToolMerged.exe (no dependencies)
    
    Usage: `CommandLineToolMerged.exe [action] [options]`

Java
----

* Core jar path:

    cielo24_Java/cielo24/bin/cielo24.jar (no dependencies)
    
    Dependent jar can be compiled from source code (3rd party dependencies are in the /lib folder).
    In Eclipse - click cielo24.jardesc to compile.
    
* Command Line Interface jar path:

    cielo24_Java/cielo24cli/bin/cielo24cli.jar (no dependencies)
    
    Usage: `java -jar cielo24cli.jar [action] [options]`
    
Python
------

* Core sources:

    cielo24_Python/cielo24/cielo24/ (no dependencies)
    
* PyPI:

    Run the following command to install сielo24 package as well as any dependencies:
      `pip install cielo24`
    
    More information can be found here: https://pypi.python.org/pypi/cielo24
    
    Run the following command to install сielo24cli package as well as any dependencies:
      `pip install cielo24cli`
      
    More information can be found here: https://pypi.python.org/pypi/cielo24cli
    
    
* Command Line Interface path:

    cielo24_Python/cielo24cli/cielo24cli.py (depends on compago)
    
    Usage: `python cielo24cli.py [action] [options]`

Ruby
----

* Core gem path:

    cielo24_Ruby/cielo24_gem/cielo24-X.X.X.gem (depends on httpclient, hashie)
    
* Ruby Gems:

    Run the following command to install сielo24 gem as well as any dependencies:
      `gem install cielo24`
    
    More information can be found here: http://rubygems.org/gems/cielo24
    
    Run the following command to install сielo24-cli gem as well as any dependencies:
      `gem install cielo24-cli`
      
    Usage: `cielo24cli [action] [options]`
    More information can be found here: https://rubygems.org/gems/cielo24-cli
    
* Command Line Interface gem path:

    cielo24_Ruby/cielo24_command/cielo24-cli-X.X.X.gem (depends on cielo24, thor)
