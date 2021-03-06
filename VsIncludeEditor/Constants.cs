﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VsIncludeEditor
{
    public static class Constants
    {
        //Regex
        public const string RGX_CONTENT = @"<Content[\s\S]*?(</Content >|/>)";
        public const string RGX_INCLUDE = "Include=\"[\\s\\S] *? \"";
        public const string RGX_REFERENCE = @"<Reference[\s\S]*?</Reference>";

        // .csproj Identifiers
        public const string CSPROJ_PROJECTTYPE = "ProjectTypeGuids";
        public const string CSPROJ_WEB_GUID = "{349c5851-65df-11da-9384-00065b846f21}";
        public const string CSPROJ_WPF_GUID = "{60dc8134-eba5-43b8-bcc9-bb4bc16c2548}";

        // General
        public const string CSPROJ_CONTENT = "Content";
        public const string CSPROJ_FOLDER = "Folder";
        public const string CSPROJ_INCLUDE = "Include";
        public const string CSPROJ_ITEMGROUP = "ItemGroup";
        public const string CSPROJ_NONE = "None";
        public const string CSPROJ_REFERENCE = "Reference";

        // Non-Web
        public const string CSPROJ_APPDEF = "ApplicationDefinition";
        public const string CSPROJ_COMPILE = "Compile";
        public const string CSPROJ_EMBRESOURCE = "EmbeddedResource";
        public const string CSPROJ_PAGE = "Page";

        public static readonly string[] CSPROJ_EXE_INCLUDE_TAGS =
            {
                CSPROJ_APPDEF,
                CSPROJ_COMPILE,
                CSPROJ_EMBRESOURCE,
                CSPROJ_PAGE
            };

        public static readonly string[] CSPROJ_GEN_INCLUDE_TAGS =
            {
                CSPROJ_CONTENT,
                CSPROJ_FOLDER,
                CSPROJ_NONE
            };        
    }
}
