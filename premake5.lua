include "./Scripts/dotnet.lua"

workspace "Snowflake"
   configurations { "Debug", "Release" }

outputdir = "%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}"

IncludeDir = {}
IncludeDir["GLFW"] = "%{wks.location}/Vendor/GLFW/include"
IncludeDir["Glad"] = "%{wks.location}/Vendor/Glad/include"
IncludeDir["ImGui"] = "%{wks.location}/Vendor/imgui"

SourceDir = {}
SourceDir["GLFW"] = "%{wks.location}/Vendor/GLFW"
SourceDir["Glad"] = "%{wks.location}/Vendor/Glad"
SourceDir["ImGui"] = "%{wks.location}/Vendor/imgui"

group "Dependencies"
    include "./Scripts/glad.lua"
    include "./Scripts/glfw.lua"
    include "./Scripts/imgui.lua"
group ""

include "Source/Snowflake"
include "Source/Snowflake.Windows"

group "Tests"
    include "Test/Snowflake.Test"

group "Examples"
    include "Example/Sandbox"