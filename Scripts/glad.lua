project "Glad"
	kind "StaticLib"
    architecture "x64"
	language "C"
    location "%{wks.location}/Vendor/GLAD"
	editandcontinue "Off"
	
	targetdir ("%{wks.location}/Build/Bin/" .. outputdir .. "/%{prj.name}")
	objdir ("%{wks.location}/Build/Obj/" .. outputdir .. "/%{prj.name}")

    files
	{
		"%{prj.location}/include/glad/glad.h",
		"%{prj.location}/include/KHR/khrplatform.h",
		"%{prj.location}/src/glad.c",
	}

    includedirs
	{
		"%{prj.location}/include",
	}

    filter "system:windows"
		systemversion "latest"

    filter "configurations:Debug"
		runtime "Debug"
		symbols "on"

	filter "configurations:Release"
		runtime "Release"
		optimize "on"
