#include "../pch.h"

#include "ImGuiLayer.h"

#include "imgui.h"
#include "backends/imgui_impl_glfw.h"
#include "backends/imgui_impl_opengl3.h"
#include "GLFW/glfw3.h"

namespace Snowflake {
namespace Windows {

ImGuiLayer::ImGuiLayer()
	:Layer("ImGuiLayer")
{
}

ImGuiLayer::~ImGuiLayer()
{
}

void ImGuiLayer::OnAttach()
{	
	ImGui::CreateContext();
	ImGui::StyleColorsDark();

	ImGuiIO& io = ImGui::GetIO();
	io.BackendFlags |= ImGuiBackendFlags_HasMouseCursors;
	io.BackendFlags |= ImGuiBackendFlags_HasSetMousePos;

	ImGui_ImplOpenGL3_Init("#version 460");
}

void ImGuiLayer::OnDetach()
{
}

void ImGuiLayer::OnUpdate()
{
	ImGuiIO& io = ImGui::GetIO();
	Application^ app = Application::Get();
	io.DisplaySize = ImVec2((float)app->Window->Width, (float)app->Window->Height);

	float time = (float)glfwGetTime();
	io.DeltaTime = m_time > 0.0f ? (time - m_time) : (1.0f / 60.0f);
	m_time = time;

	ImGui_ImplOpenGL3_NewFrame();
	ImGui::NewFrame();

	static bool show = true;
	ImGui::ShowDemoWindow(&show);

	ImGui::Render();
	ImGui_ImplOpenGL3_RenderDrawData(ImGui::GetDrawData());
}

void ImGuiLayer::OnEvent(Event^ e)
{
}

}
}