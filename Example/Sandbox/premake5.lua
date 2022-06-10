project "Sandbox"
    kind "ConsoleApp"
    architecture "x64"
    language "C#"
    dotnetframework "net6.0"
    targetdir ("%{wks.location}/Build/Bin/" .. outputdir .. "/%{prj.name}")
    objdir ("%{wks.location}/Build/Obj/" .. outputdir .. "/%{prj.name}")
    nullable "true"
    implicitusings "true"
    appendtargetframeworktooutputpath "false"
    appendruntimeidentifiertooutputpath "false"
    
    files { "%{prj.location}/**.cs" }
    removefiles { "%{prj.location}/bin/**.*", "%{prj.location}/obj/**.*" }
   
    links
    {
        "Snowflake",
        "Snowflake.Windows",
    }
   
    filter "configurations:Debug"
        defines { "DEBUG" }
        symbols "On"

    filter "configurations:Release"
        defines { "NDEBUG" }
        optimize "On"