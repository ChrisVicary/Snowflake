# ImGui

## Setup

Setting up ImGui was a bit more challenging than some of the other videos. I have tried to stick to a similar tactic which is just get it working. After adding ImGui as a git submodule I quickly realised that the structure of the repository has changed quite a bit in the 3+ years since TheChero made his video. While he copied a bunch of code from the ImGui examples, it seemed like a lot of that had now been separated into the `backends` folder as different implementations for different platforms. When constructing the premake file for ImGui I decided to just pull in the extra backend implementations that I needed for OpenGL and GLFW. It might be that these get removed later on as the rendering layer matures, but this seemed to be the easiest way to get things up and running initially.

```lua
project "ImGui"
	...

    files
	{
		"%{SourceDir.ImGui}/backends/imgui_impl_glfw.cpp",
		"%{SourceDir.ImGui}/backends/imgui_impl_glfw.h",
		"%{SourceDir.ImGui}/backends/imgui_impl_opengl3.cpp",
		"%{SourceDir.ImGui}/backends/imgui_impl_opengl3.h",
		...
	}
    ...
```

After setting this up I was able to generate the ImGui project and compile it successfully.

## ImGui Layer

I created the `ImGuiLayer` in my C++/CLI project as I need to reference the ImGui classes directly. My `ImGuiLayer` class very closely resembles the initial one from Hazel. The first issue I came across was that ImGui keymap doesn't seemd to exist anymore, so I skipped copying that for now. There is a function for getting different keycodes which I might have to use later. Next the call to initialize ImGui for OpenGL `ImGui_ImplOpenGL3_Init` passes in an OpenGL version string, having not worked with OpenGL much in the past I didn't know what this version number was or where it came from. In the video the version string used was `"#version 410"`. At first I just copied this without thinking about it, but when I tried to run the application I would see the ImGui was failing to initialize properly. After a bit of googling and debugging I was able to find the version that I had was `"#version 460"` after making this change things got a little further but still not loading property. Stepping through the startup code I realised that the `OnAttach` of the layer was getting called before the `Init` method on the window, due to the window not being created until the application `Run` method. The window `Init` method is used to initialize the OpenGL context, so ImGui was trying to initialize to an OpenGL context which hadn't been created yet. I moved the creation and initialization of the window to the application constructor and finally everything was running as it should be. My `OnAttach` method ended up like this.

```cpp
void ImGuiLayer::OnAttach()
{	
	ImGui::CreateContext();
	ImGui::StyleColorsDark();

	ImGuiIO& io = ImGui::GetIO();
	io.BackendFlags |= ImGuiBackendFlags_HasMouseCursors;
	io.BackendFlags |= ImGuiBackendFlags_HasSetMousePos;

	ImGui_ImplOpenGL3_Init("#version 460");
}
```

Setting up the `OnUpdate` method was much easier, everything works in the same way as the video. One thing I didn't like was the static application instance and the function to get the window. Ideally if the layer depends on the window it should get passed into the constructor or passed through one of the functions. Currently the concept of layers are somewhat abstract at this point, not all layers have to be visual or care about the size of the window. I expect that most layers are likely to be in the future, but it probably doesn't need to be part of the API. For now I have maintained consistency with the video series and added the static instance and getter. So the `OnUpdate` method looks very similar to the example.

```cpp
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
```

Finally adding the `ImGuiLayer` to dependency injection system in `SandboxBootstrapper` and passing it into the `SandboxApp` constructor I could add the layer to the application.

```cs
internal class SandboxBootstrapper : Bootstrapper<SandboxApp>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        ...
        services.AddSingleton<ImGuiLayer>();
    }

    protected override SandboxApp CreateApplication(IServiceProvider serviceProvider) 
        => new SandboxApp(
            serviceProvider.GetRequiredService<ILogger<Application>>(), 
            serviceProvider.GetRequiredService<Func<IWindow>>(),
            serviceProvider.GetRequiredService<ExampleLayer>(),
            serviceProvider.GetRequiredService<ImGuiLayer>());
}
```
```cs
internal class SandboxApp : Application
{
    public SandboxApp(ILogger<Application> logger, Func<IWindow> windowFactory, ExampleLayer exampleLayer, ImGuiLayer imGuiLayer)
        : base(logger, windowFactory) 
    {
        PushLayer(exampleLayer);
        PushOverlay(imGuiLayer);
    }
}
```
The constructor for the SandboxApp is starting to grow more than I would like. I could inject the layers as a collection, but I don't currently have a way to decide whether the layer should be an overlay or not. This also refers back to the reservations I had around the layers and the layertack, adding to the fact that the window is now created in the application constuctor. There is probably an opportunity to have a couple of services to managing windows and layers, that are fed into the application, these services could then be initialized in the correct order.

After putting this all together, the main OpenGL window should now look something like this:

![ImGui](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/Images/12-ImGui-01.png "ImGui")

## Refactoring Premake Scripts

As I was including another submodule I took this opportunity to refactor my premake files a little bit. I wanted to keep the submodules completely clean, by this I mean that I didn't want to have an additional files added to the submodule folders. Currently the premake scripts would generate project files in the root folder of the submodule. So I modified the premake files for the dependencies to generate the project files in the Build directory along with the Bin and Obj folders.

## Video Link

[TheCherno - Game Engine Series - ImGui](https://www.youtube.com/watch?v=r74WxFMIEdU&list=PLlrATfBNZ98dC-V-N3m0Go4deliWHPFwT&index=15&ab_channel=TheCherno)

## Next
[ImGui Events](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/13-ImGuiEvents.md)