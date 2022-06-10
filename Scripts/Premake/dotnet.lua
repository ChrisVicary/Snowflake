premake.api.register {
	name = "nullable",
	scope = "project",
	kind = "boolean",
}

premake.api.register {
	name = "implicitusings",
	scope = "project",
	kind = "boolean",
}

premake.api.register {
	name = "appendtargetframeworktooutputpath",
	scope = "project",
	kind = "boolean",
}

premake.api.register {
	name = "appendruntimeidentifiertooutputpath",
	scope = "project",
	kind = "boolean",
}

require('vstudio')

local p = premake
local vstudio = p.vstudio
local cs2005 = p.vstudio.cs2005
local dotnetbase = p.vstudio.dotnetbase

local function enableNullable(prj)
   premake.w('<Nullable>enable</Nullable>')
end

local function enableImplicitUsings(prj)
   premake.w('<ImplicitUsings>enable</ImplicitUsings>')
end

local function disableAppendTargetFrameworkToOutputPath(prj)
   premake.w('<AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>')
end

local function disableAppendRuntimeIdentifierToOutputPath(prj)
   premake.w('<AppendRuntimeIdentifierToOutputPath>false</AppendRuntimeIdentifierToOutputPath>')
end

local function platforms(prj)
   local arch = dotnetbase.arch(prj)
   premake.w('<Platforms>%s</Platforms>', arch)
end

p.override(cs2005.elements, "projectProperties", function(base, prj)
   local calls = base(prj)
   if dotnetbase.isNewFormatProject(prj) then
      table.insertafter(calls, dotnetbase.netcore.targetFramework, platforms)
      if prj.nullable then
         table.insertafter(calls, dotnetbase.netcore.targetFramework, enableNullable)
      end
      if prj.implicitusings then
         table.insertafter(calls, dotnetbase.netcore.targetFramework, enableImplicitUsings)
      end
      if not prj.appendtargetframeworktooutputpath then
         table.insertafter(calls, dotnetbase.netcore.targetFramework, disableAppendTargetFrameworkToOutputPath)
      end
      if not prj.appendruntimeidentifiertooutputpath then
         table.insertafter(calls, dotnetbase.netcore.targetFramework, disableAppendRuntimeIdentifierToOutputPath)
      end
      if prj.appendruntimeidentifiertooutputpath then
         table.insertafter(calls, dotnetbase.netcore.targetFramework, disableAppendRuntimeIdentifierToOutputPath)
      end
   end
   return calls
end)

local vc2010 = p.vstudio.vc2010

local function isNetCore(cfg)
   local framework = cfg.dotnetframework
   if not framework then
      return false
   end

   if framework:find('^net') ~= nil then
      return true
   end

   return false
end

p.override(vc2010, "clrSupport", function(base, cfg)
   if isNetCore(cfg) then
      vc2010.element("CLRSupport", nil, "NetCore")
   else
      base(cfg)
   end
end)

p.override(vc2010, "keyword", function(base, cfg)
   if isNetCore(cfg) then
      vc2010.element("Keyword", nil, "NetCoreCProj")
      vc2010.targetFramework(cfg)
      vc2010.element("RootNamespace", nil, "%s", cfg.name)
   else
      base(cfg)
   end
end)

p.override(vc2010, "targetFramework", function(base, cfg)
   if isNetCore(cfg) then
      p.w('<TargetFramework>%s</TargetFramework>', cfg.dotnetframework)
   else
      base(cfg)
   end
end)