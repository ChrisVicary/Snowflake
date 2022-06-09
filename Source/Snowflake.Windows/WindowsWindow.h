#pragma once

#pragma managed(push, off)

#include "GLFW/glfw3.h"

#pragma managed(pop)

#include <msclr/marshal.h>

namespace Snowflake {
namespace Windows {
	using namespace Snowflake;
	using namespace Snowflake::Events;
	
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
	private:
		void Init(WindowProps^ props);
		void Shutdown();
	private:
		GLFWwindow* m_window;

		

		WindowData m_data;
	};
}
}