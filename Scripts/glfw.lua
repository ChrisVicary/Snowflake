project "GLFW"
	kind "StaticLib"
    architecture "x64"
	language "C"
    location "%{wks.location}/Build/GLFW"
	editandcontinue "Off"

	targetdir ("%{wks.location}/Build/Bin/" .. outputdir .. "/%{prj.name}")
	objdir ("%{wks.location}/Build/Obj/" .. outputdir .. "/%{prj.name}")

    files
	{
		"%{SourceDir.GLFW}/include/GLFW/glfw3.h",
		"%{SourceDir.GLFW}/include/GLFW/glfw3native.h",
		"%{SourceDir.GLFW}/src/glfw_config.h",
		"%{SourceDir.GLFW}/src/context.c",
		"%{SourceDir.GLFW}/src/init.c",
		"%{SourceDir.GLFW}/src/input.c",
		"%{SourceDir.GLFW}/src/monitor.c",
		"%{SourceDir.GLFW}/src/null_init.c",
		"%{SourceDir.GLFW}/src/null_joystick.c",
		"%{SourceDir.GLFW}/src/null_monitor.c",
		"%{SourceDir.GLFW}/src/null_window.c",
		"%{SourceDir.GLFW}/src/platform.c",
		"%{SourceDir.GLFW}/src/vulkan.c",
		"%{SourceDir.GLFW}/src/window.c",
	}

    filter "system:windows"
		systemversion "latest"

		files
		{
			"%{SourceDir.GLFW}/src/win32_init.c",
			"%{SourceDir.GLFW}/src/win32_joystick.c",
            "%{SourceDir.GLFW}/src/win32_module.c",
			"%{SourceDir.GLFW}/src/win32_monitor.c",
			"%{SourceDir.GLFW}/src/win32_time.c",
			"%{SourceDir.GLFW}/src/win32_thread.c",
			"%{SourceDir.GLFW}/src/win32_window.c",
			"%{SourceDir.GLFW}/src/wgl_context.c",
			"%{SourceDir.GLFW}/src/egl_context.c",
			"%{SourceDir.GLFW}/src/osmesa_context.c"
		}

		defines 
		{ 
			"_GLFW_WIN32",
			"_CRT_SECURE_NO_WARNINGS"
		}

    filter "configurations:Debug"
		runtime "Debug"
		symbols "on"

	filter "configurations:Release"
		runtime "Release"
		optimize "on"
