project "Glad"
	kind "StaticLib"
    architecture "x64"
	language "C"
    location "%{wks.location}/Build/Glad"
	editandcontinue "Off"
	
	targetdir ("%{wks.location}/Build/Bin/" .. outputdir .. "/%{prj.name}")
	objdir ("%{wks.location}/Build/Obj/" .. outputdir .. "/%{prj.name}")

    files
	{
		"%{SourceDir.Glad}/include/glad/glad.h",
		"%{SourceDir.Glad}/include/KHR/khrplatform.h",
		"%{SourceDir.Glad}/src/glad.c",
	}

    includedirs
	{
		"%{IncludeDir.Glad}",
	}

    filter "system:windows"
		systemversion "latest"

    filter "configurations:Debug"
		runtime "Debug"
		symbols "on"

	filter "configurations:Release"
		runtime "Release"
		optimize "on"
