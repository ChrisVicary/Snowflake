# Project Setup

## Create the Repository

I started with a very similar setup to the tutorial series, setting the repository in GitHub and cloning to a local folder on my PC.

## Create the Solution

C# and C++ projects differ a little bit so I needed to do something a little bit different for the solution and project setup. In Visual Studio I created a C# Class Library for the main engine assembly. The a C# Console App project for the Sandbox application. After creating the projects I replaced the AnyCPU platform with only x64 for both projects, and modified the project files to point the OutputPath and IntermediateOutputPath properties to a root folder in the repository called Build. A nuance with Visual Studio and MSBuild is that despite adding the IntermediateOutputPath a folder called obj is still created under the project so I needed to create a PostBuild step to remove that and keep my source folders clean.

The initial setup of the Snowflake.csproj file looks like this:

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <Platforms>x64</Platforms>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <AppendRuntimeIdentifierToOutputPath>false</AppendRuntimeIdentifierToOutputPath>
  </PropertyGroup>

  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|x64'">
    <OutputPath>$(SolutionDir)\Build\Bin\$(Configuration)-$(Platform)\$(MSBuildProjectName)\</OutputPath>
    <IntermediateOutputPath>$(SolutionDir)\Build\Obj\$(Configuration)-$(Platform)\$(MSBuildProjectName)\</IntermediateOutputPath>
  </PropertyGroup>

  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|x64'">
    <OutputPath>$(SolutionDir)\Build\Bin\$(Configuration)-$(Platform)\$(MSBuildProjectName)\</OutputPath>
    <IntermediateOutputPath>$(SolutionDir)\Build\Obj\$(Configuration)-$(Platform)\$(MSBuildProjectName)\</IntermediateOutputPath>
  </PropertyGroup>

  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="rd &quot;$(ProjectDir)obj&quot; /s /q" />
  </Target>

</Project>
```

I also moved the projects to a subfolder of the solution called Source, as I wanted to add folders for other things like documentation and tests. Finally I added a reference to the Snowflake project frm the Sandbox project.

## Create Test Code

To ensure that everything was working, similar to the tutorial, I created a new class called Test in the Snowflake project with a single static Print method.

```
namespace Snowflake;

public class Test
{
    public static void Print() => Console.WriteLine("Welcome to Snowflake!");
}
```

In the Sandbox project I had kept the Program.cs file that was part of the project template but modified it to call my Print function on the Test class.

```
Snowflake.Test.Print();
```
> Note that there is no namespace or `Program` class in this file as I am using .Net 6 which supports top-level statements.

## Video Link

[TheCherno - Game Engine Series - Project Setup](https://www.youtube.com/watch?v=KG8cAGvn9d4&list=PLlrATfBNZ98dC-V-N3m0Go4deliWHPFwT&index=5&ab_channel=TheCherno)
