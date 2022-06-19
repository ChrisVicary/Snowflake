# Modern OpenGL

Setting up glad on my C++/CLI project was basically the same as the pure C++ version. I can definitely see the benefit of using premake as I add the additional dependencies. I have my directory structure setup a little differently, so all my vendor code is going into a root folder and I've added premake scripts for those dependencies in a scripts folder. This allows me to add those dependencies as sub modules without editing them or forking them for the time being.

While I was getting this setup I also moved a few things around in my includes and header files, to remove all the build warnings.

## Video Link

[TheCherno - Game Engine Series - Modern OpenGL](https://www.youtube.com/watch?v=r74WxFMIEdU&list=PLlrATfBNZ98dC-V-N3m0Go4deliWHPFwT&index=14&ab_channel=TheCherno)

## Next
[ImGui](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/12-ImGui.md)