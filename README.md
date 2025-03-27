# SharpCompiler

Toy programming language transpiler to C++ written in C#. The idea is to build a language like C#
and use C++ as a backend for efficient compilation.

## Prerequisites

### dotnet SDK
Install dotnet 9.0 SDK (https://dotnet.microsoft.com)

### Invoke-Build

```
dotnet tool install --global ib
```

### g++ (GNU GCC)

On Windows, install using Cygwin (https://www.cygwin.com).

On Ubuntu

```
sudo apt install g++
```

On MacOS you can use clang (g++ is already a symbolic link to clang++)

## Building

To build the transpiler (shc):

```
ib build
```

To run the transpiler against a demo program:

```
cd shc
dotnet run samples\hello.shl
samples\hello.exe
```

To run the test suite:

```
ib test
```
