include "./Scripts/Premake/dotnet.lua"

workspace "Snowflake"
   configurations { "Debug", "Release" }

outputdir = "%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}"

group "Dependencies"
    include "./Scripts/GLFW/glfw.lua"
group ""

include "Source/Snowflake"
include "Source/Snowflake.Windows"

group "Tests"
    include "Test/Snowflake.Test"

group "Examples"
    include "Example/Sandbox"