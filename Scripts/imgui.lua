project "ImGui"
	kind "StaticLib"
	architecture "x64"
	language "C++"
    location "%{wks.location}/Build/imgui"
	editandcontinue "Off"
	
	targetdir ("%{wks.location}/Build/Bin/" .. outputdir .. "/%{prj.name}")
	objdir ("%{wks.location}/Build/Obj/" .. outputdir .. "/%{prj.name}")

    files
	{
		"%{SourceDir.ImGui}/backends/imgui_impl_glfw.cpp",
		"%{SourceDir.ImGui}/backends/imgui_impl_glfw.h",
		"%{SourceDir.ImGui}/backends/imgui_impl_opengl3.cpp",
		"%{SourceDir.ImGui}/backends/imgui_impl_opengl3.h",
		"%{SourceDir.ImGui}/imconfig.h",
        "%{SourceDir.ImGui}/imgui_demo.cpp",
        "%{SourceDir.ImGui}/imgui_draw.cpp",
        "%{SourceDir.ImGui}/imgui_internal.h",
		"%{SourceDir.ImGui}/imgui_tables.cpp",
        "%{SourceDir.ImGui}/imgui_widgets.cpp",
		"%{SourceDir.ImGui}/imgui.h",
		"%{SourceDir.ImGui}/imgui.cpp",
        "%{SourceDir.ImGui}/imstb_rectpack.h",
        "%{SourceDir.ImGui}/imstb_textedit.h",
        "%{SourceDir.ImGui}/imstb_truetype.h",
	}

    includedirs
	{
		"%{IncludeDir.GLFW}",
        "%{IncludeDir.ImGui}",
	}

    links
    {
        "opengl32.lib"
    }

    filter "system:windows"
		systemversion "latest"

	filter "configurations:Debug"
		runtime "Debug"
		symbols "on"

	filter "configurations:Release"
		runtime "Release"
		optimize "on"
