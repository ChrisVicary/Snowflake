# Premake - Part 2

## Maybe I Will Use Premake

It took me a while to get the Snowflake.Windows library up and running with GLFW, initially using pre-built binaries. As I was combining everything together I realised that a lot of this might have been easier if I had invested a little more time into getting premake working.

## Premake for .Net 6

After my initial investigations of premake I knew that some of the newer features of .Net were not supported by premake out of the box. The features that I wanted to use were:

- Nullable reference types
- Implicit usings
- C++/CLI

While the implicit usings are pretty minor, the nullable references types could be a huge benefit in a project like this where performance is important I don't want to waste cycles checking for and handling null objects. Adding the extra API for the nullable reference types and implicit usings was actually quite straight forward once I found some examples of extending the premake API in the documentation.

Adding the Premake API for nullable reference types:
```lua
premake.api.register {
	name = "nullable",
	scope = "project",
	kind = "boolean",
}

local function enableNullable(prj)
   premake.w('<Nullable>enable</Nullable>')
end

premake.override(premake.vstudio.cs2005.elements, "projectProperties", function(base, prj)
   local calls = base(prj)
   if premake.vstudio.dotnetbase.isNewFormatProject(prj) then
      table.insertafter(calls, premake.vstudio.dotnetbase.netcore.targetFramework, platforms)
      if prj.nullable then
         table.insertafter(calls, premake.vstudio.dotnetbase.netcore.targetFramework, enableNullable)
      end
   end
   return calls
end)
```
I then added a couple of extra options to disable the appending of target framework and runtime identifier to the output path, which is usually true by default.

## Adding Support for C++/CLI in .Net 6

All the examples I found for C++/CLI projects using premake were using .Net Framework rather than .Net 6, looking through the premake source code I couldn't see any options to enable newer versions of .Net or .Net Core. From what I could tell the `dotnetframework` project property would only support numbers i.e. `4.5` or `4.8`, rather than the newer style `net6.0`. To handle this I added a new function to support parsing `net6.0` and using that to override existing functions and modify the output to support .Net.

All of these additions can be found in the `Scripts\Premake\dotnet.lua` file.

## Next
[Window Events](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/09-WindowEvents.md)