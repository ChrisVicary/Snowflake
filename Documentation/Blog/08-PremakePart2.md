# Premake - Part 2

## Maybe I Will Use Premake

It took me a while to get the Snowflake.Windows library up and running with GLFW, initially using pre-built binaries. As I was combining everything together I realised that a lot of this might have been easier if I had invested a little more time into getting premake working.

## Premake for .Net 6

After my initial investigations of premake I knew that some of the newer features of .Net were not supported by premake out of the box. The features that I wanted to use were:

- Nullable reference types
- Implicit usings
- C++/CLI

While the implicit usings are pretty minor, the nullable references types could be a huge benefit in a project like this where performance is important I don't want to waste cycles checking for and handling null objects. Adding the extra API for the nullable reference types and implicit usings was actually quite straight forward once I found some examples of extending the premake API in the documentation.

### Premake API for Nullable Reference Types
```lua
premake.api.register {
	name = "nullable",
	scope = "project",
	kind = "boolean",
}

local function enableNullable(prj)
   premake.w('<Nullable>enable</Nullable>')
end


```