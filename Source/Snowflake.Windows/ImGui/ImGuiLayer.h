#pragma once

#pragma managed(push, off)

struct ImGuiContext {};

#pragma managed(pop)

namespace Snowflake {
namespace Windows {
	using namespace Snowflake;
	using namespace Snowflake::Events;

	public ref class ImGuiLayer : public Layer
	{
	public:
		ImGuiLayer();
		~ImGuiLayer();

		void OnAttach() override;
		void OnDetach() override;
		void OnUpdate() override;
		void OnEvent(Event^ event) override;
	private:
		float m_time = 0.0f;
	};
}
}