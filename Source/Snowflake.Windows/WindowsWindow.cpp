#include "pch.h"

#include "WindowsWindow.h"

namespace Snowflake {
namespace Windows {
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Snowflake;

#define SET_CALLBACK_DELEGATE(delegateType, delegateFunction, callbackType, callbackFunction) \
{ \
	delegateType^ del = gcnew delegateType(this, &WindowsWindow::delegateFunction); \
	GCHandle handle = GCHandle::Alloc(del); \
	IntPtr ptr = Marshal::GetFunctionPointerForDelegate(del); \
	callbackFunction(m_window, static_cast<callbackType>(ptr.ToPointer())); \
}

static bool s_glfwInitialized = false;

static void GLFWErrorCallback(int error, const char* description)
{
	// TODO: Log error somehow!
}

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
		glfwSetErrorCallback(GLFWErrorCallback);
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

	SET_CALLBACK_DELEGATE(CursorPosDelegate, OnCursorPos, GLFWcursorposfun, glfwSetCursorPosCallback)
	SET_CALLBACK_DELEGATE(KeyDelegate, OnKey, GLFWkeyfun, glfwSetKeyCallback)
	SET_CALLBACK_DELEGATE(MouseButtonDelegate, OnMouseButton, GLFWmousebuttonfun, glfwSetMouseButtonCallback)
	SET_CALLBACK_DELEGATE(ScrollDelegate, OnScroll, GLFWscrollfun, glfwSetScrollCallback)
	SET_CALLBACK_DELEGATE(WindowCloseDelegate, OnWindowClose, GLFWwindowclosefun, glfwSetWindowCloseCallback)
	SET_CALLBACK_DELEGATE(WindowSizeDelegate, OnWindowSize, GLFWwindowsizefun, glfwSetWindowSizeCallback)

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

void WindowsWindow::OnCursorPos(GLFWwindow* window, double xPos, double yPos)
{
	MouseMoveEvent^ mouseMoveEvent = gcnew MouseMoveEvent((float)xPos, (float)yPos);
	m_data.EventCallback(mouseMoveEvent);
}

void WindowsWindow::OnKey(GLFWwindow* window, int key, int scancode, int action, int mods)
{
	KeyEvent^ keyEvent;
	switch (action)
	{
	case GLFW_PRESS:
		keyEvent = gcnew KeyPressEvent(key, 0);
		break;
	case GLFW_RELEASE:
		keyEvent = gcnew KeyReleaseEvent(key);
		break;
	case GLFW_REPEAT:
		keyEvent = gcnew KeyPressEvent(key, 1);
		break;
	}
	
	if (keyEvent)
		m_data.EventCallback(keyEvent);
}

void WindowsWindow::OnMouseButton(GLFWwindow* window, int button, int action, int mods)
{
	MouseButtonEvent^ mouseButtonEvent;
	switch (action)
	{
	case GLFW_PRESS:
		mouseButtonEvent = gcnew MouseButtonPressEvent(button);
		break;
	case GLFW_RELEASE:
		mouseButtonEvent = gcnew MouseButtonReleaseEvent(button);
		break;
	}

	if (mouseButtonEvent)
		m_data.EventCallback(mouseButtonEvent);
}

void WindowsWindow::OnScroll(GLFWwindow* window, double xOffset, double yOffset)
{
	MouseScrollEvent^ mouseScrollEvent = gcnew MouseScrollEvent((float)xOffset, (float)yOffset);
	m_data.EventCallback(mouseScrollEvent);
}

void WindowsWindow::OnWindowClose(GLFWwindow* window)
{
	WindowCloseEvent^ closeEvent = gcnew WindowCloseEvent();
	m_data.EventCallback(closeEvent);
}

void WindowsWindow::OnWindowSize(GLFWwindow* window, int width, int height)
{
	m_data.Width = width;
	m_data.Height = height;

	WindowResizeEvent^ resizeEvent = gcnew WindowResizeEvent(width, height);
	m_data.EventCallback(resizeEvent);
}

}
}