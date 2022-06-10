project "Snowflake.Windows"
    kind "SharedLib"
    architecture "x64"
    language "C++"
    clr "On"
    dotnetframework "net6.0"
    targetdir ("../../Build/Bin/" .. outputdir .. "/%{prj.name}")
    objdir ("../../Build/Obj/" .. outputdir .. "/%{prj.name}")
   
    files 
    { 
        "%{prj.location}/**.h",
        "%{prj.location}/**.cpp",
    }

    removefiles { "%{prj.location}/x64/**.*" }

    includedirs
    {
        "%{wks.location}/Vendor/GLFW/include",
    }

    links
    {
        "Snowflake",
        "GLFW",
    }
   
    removefiles { "%{prj.location}/obj/**.*" }

   filter "configurations:Debug"
      defines { "DEBUG" }
      symbols "On"

   filter "configurations:Release"
      defines { "NDEBUG" }
      optimize "On"