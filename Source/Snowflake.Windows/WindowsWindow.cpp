#include "pch.h"

#include "WindowsWindow.h"

namespace Snowflake {
namespace Windows {
using namespace System::Runtime::InteropServices;
using namespace Snowflake;

static bool s_glfwInitialized = false;

WindowsWindow::WindowsWindow(WindowProps^ props)
{
	Init(props);
}

WindowsWindow::~WindowsWindow()
{
}

void WindowsWindow::Init(WindowProps^ props)
{
	m_data.Title = props->Title;
	m_data.Width = props->Width;
	m_data.Height = props->Height;

	if (!s_glfwInitialized)
	{
		int success = glfwInit();
		s_glfwInitialized = true;
	}

	msclr::interop::marshal_context^ context = gcnew msclr::interop::marshal_context();
	const char* title = context->marshal_as<const char*>(m_data.Title);
	m_window = glfwCreateWindow((int)props->Width, (int)props->Height, title, nullptr, nullptr);

	/*System::IntPtr dataPtr = Marshal::AllocHGlobal(Marshal::SizeOf(m_data));
	Marshal::StructureToPtr(m_data, dataPtr, false);
	glfwSetWindowUserPointer(m_window, dataPtr.ToPointer());*/

	glfwMakeContextCurrent(m_window);
	SetVSync(true);

	delete context;
}

void WindowsWindow::Shutdown()
{
	glfwDestroyWindow(m_window);
}

void WindowsWindow::OnUpdate()
{
	glfwPollEvents();
	glfwSwapBuffers(m_window);
}

void WindowsWindow::SetVSync(bool enabled)
{
	if (enabled)
		glfwSwapInterval(1);
	else
		glfwSwapInterval(0);

	m_data.VSync = enabled;
}

bool WindowsWindow::IsVSync()
{
	return m_data.VSync;
}

}
}