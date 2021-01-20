# Tee

A bare-bones implementation of Linux tee in .NET Core 3.1  Tee provides a way to duplicate standard output to both the console and a file.
This application was written to provide the most basic, common use of Tee in Windows, without a need to install GNU or other environments.

- **myapplication.exe** would write all output to the console
- **myapplication.exe > file.txt** would write all output to the file, but nothing to the console.

### Differences from Linux tee
- It does not support arguments, other than the filename to contain the standard output.
- If the file exists, the content is overwritten.  Appending to existing output is not supported.

### Usage
*Tee filename.txt*

Example: **myapplication.exe 2>>&1 | Tee stdout_content.txt**

The above example merges the standard error (2) to standard output, then the pipe routes that output to Tee, which:
- Creates a temp file
- Writes the standard output to both the console and the temp file/
- At the end of the input stream, moves the temp file to the file name in the argument.

### Exit Codes
- 0 = success
- 1 = exception detected.

### Installation
- Ensure .NET Core 3.1 is installed on your system.
- Change current directory to where the project folder should be cloned

```
git clone https://github.com/rambotech/Tee.git
cd Tee
# change "folder" below to where the binary files should be copied
dotnet build -c release -o folder
```

- Add the folder to your path, or use the explicit path in front of the application.









