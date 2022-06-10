project "Snowflake.Test"
    kind "SharedLib"
    architecture "x64"
    language "C#"
    dotnetframework "net6.0"
    targetdir ("%{wks.location}/Build/Bin/" .. outputdir .. "/%{prj.name}")
    objdir ("%{wks.location}/Build/Obj/" .. outputdir .. "/%{prj.name}")
    nullable "true"
    implicitusings "true"
    appendtargetframeworktooutputpath "false"
    appendruntimeidentifiertooutputpath "false"
    
    files { "**.cs" }
    removefiles { "%{prj.location}/bin/**.*", "%{prj.location}/obj/**.*" }
   
    nuget 
    {
        "Microsoft.NET.Test.Sdk:17.1.0",
        "Moq:4.17.2",
        "NUnit:3.13.3",
        "NUnit3TestAdapter:4.2.1",
    }

    links
    {
        "Snowflake"
    }
   
    filter "configurations:Debug"
        defines { "DEBUG" }
        symbols "On"

    filter "configurations:Release"
        defines { "NDEBUG" }
        optimize "On"