#pragma once

#pragma managed(push, off)

#include "GLFW/glfw3.h"

#pragma managed(pop)

#include <msclr/marshal.h>

namespace Snowflake {
namespace Windows {
	using namespace Snowflake;
	using namespace Snowflake::Events;
	
	delegate void CursorPosDelegate(GLFWwindow* window, double xpos, double ypos);
	delegate void KeyDelegate(GLFWwindow* window, int key, int scancode, int action, int mods);
	delegate void MouseButtonDelegate(GLFWwindow* window, int button, int action, int mods);
	delegate void ScrollDelegate(GLFWwindow* window, double xoffset, double yoffset);
	delegate void WindowCloseDelegate(GLFWwindow* window);
	delegate void WindowSizeDelegate(GLFWwindow* window, int width, int height);

	value struct WindowData
	{
		System::String^ Title;
		unsigned int Width, Height;
		bool VSync;

		System::Action<Event^>^ EventCallback;
	};

	public ref class WindowsWindow : public IWindow
	{
	public:
		WindowsWindow(WindowProps^ props);
		virtual ~WindowsWindow();

		virtual property unsigned int Width { unsigned int get() { return m_data.Width; } }
		virtual property unsigned int Height { unsigned int get() { return m_data.Height; } }
		
		virtual void OnUpdate();
		virtual void SetEventCallback(System::Action<Event^>^ callback) { m_data.EventCallback = callback; }
		virtual void SetVSync(bool enabled);
		virtual bool IsVSync();
	protected:
		virtual void OnCursorPos(GLFWwindow* window, double xPos, double yPos);
		virtual void OnKey(GLFWwindow* window, int key, int scancode, int action, int mods);
		virtual void OnMouseButton(GLFWwindow* window, int button, int action, int mods);
		virtual void OnScroll(GLFWwindow* window, double xOffset, double yOffset);
		virtual void OnWindowClose(GLFWwindow* window);
		virtual void OnWindowSize(GLFWwindow* window, int width, int height);

	private:
		void Init(WindowProps^ props);
		void Shutdown();
		
	private:
		GLFWwindow* m_window;
		WindowData m_data;
	};
}
}