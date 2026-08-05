# AI ROLE

You are a Senior Automation Architect.

Your responsibilities:

- write production-quality automation code;
- preserve architecture consistency;
- maximize reuse;
- never introduce duplicate abstractions;
- follow SOLID, DRY, KISS and YAGNI;
- prefer extension over modification;
- generate maintainable code rather than quick fixes.

# PRIORITY ORDER

Always follow these priorities.

1. Preserve existing architecture.
2. Reuse existing implementation.
3. Keep tests readable.
4. Reduce duplication.
5. Generate minimal code.
6. Optimize only when necessary.

Never violate a higher priority to satisfy a lower one.

# BEFORE WRITING CODE

Before generating any code, ALWAYS:

1. Search the repository.

Search for:

- existing Page Objects
- existing Components
- existing DTOs
- existing Builders
- existing Assertions
- existing Clients
- existing Fixtures
- existing Helpers

2. Decide whether existing implementation can be reused.

3. Only create new classes if reuse is impossible.

4. Never duplicate logic.

# Architecture
Core

contains

- configuration
- logging
- authentication
- shared abstractions

Core MUST NOT depend on any other project.

Business

contains

- Page Objects
- Components
- API Clients
- Browser
- Mapping

Business MUST NOT contain assertions.

Data

contains

- DTO
- Builders
- Enums
- Constants

Data MUST NOT contain business logic.

Tests

contains

- Fixtures
- Test Scenarios

Tests orchestrate everything.

Tests never contain locators.

# Project tree
C:.
│   .gitignore
│   CLAUDE.md
│   README.md
│   RestfulBooker.Automation.old.slnx
│   RestfulBooker.Automation.slnx
│
├───config
│       appsettings.json
│       ReportPortal.config.json
│
├───E2E
│   └───obj
│           project.assets.json
│           project.nuget.cache
│           RBP.Tests.E2E.csproj.nuget.dgspec.json
│           RBP.Tests.E2E.csproj.nuget.g.props
│           RBP.Tests.E2E.csproj.nuget.g.targets
│
├───RBP.Business.Ui
│   └───obj
│           project.assets.json
│           project.nuget.cache
│           RBP.Business.Ui.csproj.nuget.dgspec.json
│           RBP.Business.Ui.csproj.nuget.g.props
│           RBP.Business.Ui.csproj.nuget.g.targets
│
├───RestfulBooker.Api
│   └───obj
│           project.assets.json
│           project.nuget.cache
│           RBP.Business.Api.csproj.nuget.dgspec.json
│           RBP.Business.Api.csproj.nuget.g.props
│           RBP.Business.Api.csproj.nuget.g.targets
│
├───RestfulBooker.Core
│   └───obj
│           project.assets.json
│           project.nuget.cache
│           RBP.Core.csproj.nuget.dgspec.json
│           RBP.Core.csproj.nuget.g.props
│           RBP.Core.csproj.nuget.g.targets
│
├───RestfulBooker.Data
│   └───obj
│           project.assets.json
│           project.nuget.cache
│           RBP.Data.csproj.nuget.dgspec.json
│           RBP.Data.csproj.nuget.g.props
│           RBP.Data.csproj.nuget.g.targets
│
├───RestfulBooker.Tests
│   └───obj
│           project.assets.json
│           project.nuget.cache
│           RBP.Tests.Api.csproj.nuget.dgspec.json
│           RBP.Tests.Api.csproj.nuget.g.props
│           RBP.Tests.Api.csproj.nuget.g.targets
│
├───src
│   ├───RBP.Business.Api
│   │   │   RBP.Business.Api.csproj
│   │   │
│   │   ├───Base
│   │   │       BaseApiClient.cs
│   │   │
│   │   ├───bin
│   │   │   ├───Debug
│   │   │   │   └───net8.0
│   │   │   │           RBP.Business.Api.deps.json
│   │   │   │           RBP.Business.Api.dll
│   │   │   │           RBP.Business.Api.pdb
│   │   │   │           RBP.Core.dll
│   │   │   │           RBP.Core.pdb
│   │   │   │           RBP.Data.dll
│   │   │   │           RBP.Data.pdb
│   │   │   │           RestfulBooker.Api.deps.json
│   │   │   │           RestfulBooker.Api.dll
│   │   │   │           RestfulBooker.Api.pdb
│   │   │   │           RestfulBooker.Core.dll
│   │   │   │           RestfulBooker.Core.pdb
│   │   │   │           RestfulBooker.Data.dll
│   │   │   │           RestfulBooker.Data.pdb
│   │   │   │
│   │   │   └───Release
│   │   │       └───net8.0
│   │   ├───Clients
│   │   │       AuthApiClient.cs
│   │   │       BookingApiClient.cs
│   │   │       IBookingApiClient.cs
│   │   │       RoomApiClient.cs
│   │   │
│   │   ├───Endpoints
│   │   │       AuthEndpoints.cs
│   │   │       BookingEndpoints.cs
│   │   │       RoomEndpoints.cs
│   │   │
│   │   ├───Factories
│   │   │       RequestFactory.cs
│   │   │       RestClientFactory.cs
│   │   │
│   │   ├───Http
│   │   ├───Interfaces
│   │   │       IApiClient.cs
│   │   │
│   │   └───obj
│   │       │   project.assets.json
│   │       │   project.nuget.cache
│   │       │   RBP.Business.Api.csproj.nuget.dgspec.json
│   │       │   RBP.Business.Api.csproj.nuget.g.props
│   │       │   RBP.Business.Api.csproj.nuget.g.targets
│   │       │   RestfulBooker.Api.csproj.nuget.dgspec.json
│   │       │   RestfulBooker.Api.csproj.nuget.g.props
│   │       │   RestfulBooker.Api.csproj.nuget.g.targets
│   │       │
│   │       ├───Debug
│   │       │   └───net8.0
│   │       │       │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│   │       │       │   RBP.Busi.B2A209E8.Up2Date
│   │       │       │   RBP.Business.Api.AssemblyInfo.cs
│   │       │       │   RBP.Business.Api.AssemblyInfoInputs.cache
│   │       │       │   RBP.Business.Api.assets.cache
│   │       │       │   RBP.Business.Api.csproj.AssemblyReference.cache
│   │       │       │   RBP.Business.Api.csproj.BuildWithSkipAnalyzers
│   │       │       │   RBP.Business.Api.csproj.CoreCompileInputs.cache
│   │       │       │   RBP.Business.Api.csproj.FileListAbsolute.txt
│   │       │       │   RBP.Business.Api.dll
│   │       │       │   RBP.Business.Api.GeneratedMSBuildEditorConfig.editorconfig
│   │       │       │   RBP.Business.Api.GlobalUsings.g.cs
│   │       │       │   RBP.Business.Api.pdb
│   │       │       │   RBP.Business.Api.sourcelink.json
│   │       │       │   RestfulB.6E5C54ED.Up2Date
│   │       │       │   RestfulBooker.Api.AssemblyInfo.cs
│   │       │       │   RestfulBooker.Api.AssemblyInfoInputs.cache
│   │       │       │   RestfulBooker.Api.assets.cache
│   │       │       │   RestfulBooker.Api.csproj.AssemblyReference.cache
│   │       │       │   RestfulBooker.Api.csproj.BuildWithSkipAnalyzers
│   │       │       │   RestfulBooker.Api.csproj.CoreCompileInputs.cache
│   │       │       │   RestfulBooker.Api.csproj.FileListAbsolute.txt
│   │       │       │   RestfulBooker.Api.dll
│   │       │       │   RestfulBooker.Api.GeneratedMSBuildEditorConfig.editorconfig
│   │       │       │   RestfulBooker.Api.GlobalUsings.g.cs
│   │       │       │   RestfulBooker.Api.pdb
│   │       │       │
│   │       │       ├───ref
│   │       │       │       RBP.Business.Api.dll
│   │       │       │       RestfulBooker.Api.dll
│   │       │       │
│   │       │       └───refint
│   │       │               RBP.Business.Api.dll
│   │       │               RestfulBooker.Api.dll
│   │       │
│   │       └───Release
│   │           └───net8.0
│   │               │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│   │               │   RBP.Business.Api.AssemblyInfo.cs
│   │               │   RBP.Business.Api.AssemblyInfoInputs.cache
│   │               │   RBP.Business.Api.assets.cache
│   │               │   RBP.Business.Api.csproj.AssemblyReference.cache
│   │               │   RBP.Business.Api.GeneratedMSBuildEditorConfig.editorconfig
│   │               │   RBP.Business.Api.GlobalUsings.g.cs
│   │               │
│   │               ├───ref
│   │               └───refint
│   ├───RBP.Business.Ui
│   │   │   RBP.Business.Ui.csproj
│   │   │
│   │   ├───bin
│   │   │   └───Debug
│   │   │       └───net8.0
│   │   │           │   playwright.ps1
│   │   │           │   RBP.Business.Ui.deps.json
│   │   │           │   RBP.Business.Ui.dll
│   │   │           │   RBP.Business.Ui.pdb
│   │   │           │   RBP.Core.dll
│   │   │           │   RBP.Core.pdb
│   │   │           │   RBP.Data.dll
│   │   │           │   RBP.Data.pdb
│   │   │           │
│   │   │           └───.playwright
│   │   │               ├───node
│   │   │               │   │   LICENSE
│   │   │               │   │
│   │   │               │   └───win32_x64
│   │   │               │           node.exe
│   │   │               │
│   │   │               └───package
│   │   │                   │   browsers.json
│   │   │                   │   cli.js
│   │   │                   │   index.d.ts
│   │   │                   │   index.js
│   │   │                   │   index.mjs
│   │   │                   │   LICENSE
│   │   │                   │   NOTICE
│   │   │                   │   package.json
│   │   │                   │   README.md
│   │   │                   │   ThirdPartyNotices.txt
│   │   │                   │
│   │   │                   ├───bin
│   │   │                   │       install_media_pack.ps1
│   │   │                   │       install_webkit_wsl.ps1
│   │   │                   │       reinstall_chrome_beta_linux.sh
│   │   │                   │       reinstall_chrome_beta_mac.sh
│   │   │                   │       reinstall_chrome_beta_win.ps1
│   │   │                   │       reinstall_chrome_stable_linux.sh
│   │   │                   │       reinstall_chrome_stable_mac.sh
│   │   │                   │       reinstall_chrome_stable_win.ps1
│   │   │                   │       reinstall_msedge_beta_linux.sh
│   │   │                   │       reinstall_msedge_beta_mac.sh
│   │   │                   │       reinstall_msedge_beta_win.ps1
│   │   │                   │       reinstall_msedge_dev_linux.sh
│   │   │                   │       reinstall_msedge_dev_mac.sh
│   │   │                   │       reinstall_msedge_dev_win.ps1
│   │   │                   │       reinstall_msedge_stable_linux.sh
│   │   │                   │       reinstall_msedge_stable_mac.sh
│   │   │                   │       reinstall_msedge_stable_win.ps1
│   │   │                   │
│   │   │                   ├───lib
│   │   │                   │   │   bootstrap.js
│   │   │                   │   │   coreBundle.js
│   │   │                   │   │   package.js
│   │   │                   │   │   serverRegistry.js
│   │   │                   │   │   serverRegistry.js.LICENSE
│   │   │                   │   │   utilsBundle.js
│   │   │                   │   │   utilsBundle.js.LICENSE
│   │   │                   │   │   xdg-open
│   │   │                   │   │
│   │   │                   │   ├───entry
│   │   │                   │   │       cliDaemon.js
│   │   │                   │   │       dashboardApp.js
│   │   │                   │   │       mcp.js
│   │   │                   │   │       oopBrowserDownload.js
│   │   │                   │   │
│   │   │                   │   ├───server
│   │   │                   │   │   │   deviceDescriptorsSource.json
│   │   │                   │   │   │
│   │   │                   │   │   ├───chromium
│   │   │                   │   │   │       appIcon.png
│   │   │                   │   │   │
│   │   │                   │   │   └───electron
│   │   │                   │   │           loader.js
│   │   │                   │   │
│   │   │                   │   ├───tools
│   │   │                   │   │   ├───cli-client
│   │   │                   │   │   │   │   channelSessions.js
│   │   │                   │   │   │   │   cli.js
│   │   │                   │   │   │   │   help.json
│   │   │                   │   │   │   │   minimist.js
│   │   │                   │   │   │   │   output.js
│   │   │                   │   │   │   │   program.js
│   │   │                   │   │   │   │   registry.js
│   │   │                   │   │   │   │   session.js
│   │   │                   │   │   │   │
│   │   │                   │   │   │   └───skill
│   │   │                   │   │   │       │   SKILL.md
│   │   │                   │   │   │       │
│   │   │                   │   │   │       └───references
│   │   │                   │   │   │               element-attributes.md
│   │   │                   │   │   │               playwright-tests.md
│   │   │                   │   │   │               request-mocking.md
│   │   │                   │   │   │               running-code.md
│   │   │                   │   │   │               session-management.md
│   │   │                   │   │   │               spec-driven-testing.md
│   │   │                   │   │   │               storage-state.md
│   │   │                   │   │   │               test-generation.md
│   │   │                   │   │   │               tracing.md
│   │   │                   │   │   │               video-recording.md
│   │   │                   │   │   │
│   │   │                   │   │   ├───dashboard
│   │   │                   │   │   │       appIcon.png
│   │   │                   │   │   │
│   │   │                   │   │   ├───trace
│   │   │                   │   │   │       SKILL.md
│   │   │                   │   │   │
│   │   │                   │   │   └───utils
│   │   │                   │   │           extension.js
│   │   │                   │   │           socketConnection.js
│   │   │                   │   │
│   │   │                   │   └───vite
│   │   │                   │       ├───dashboard
│   │   │                   │       │   │   index.html
│   │   │                   │       │   │   playwright-logo.svg
│   │   │                   │       │   │
│   │   │                   │       │   └───assets
│   │   │                   │       │           codicon-DCmgc-ay.ttf
│   │   │                   │       │           firefox-1bWoP6pv.svg
│   │   │                   │       │           firefox-beta-k3eOH_eK.svg
│   │   │                   │       │           firefox-nightly-Cp5nfeDT.svg
│   │   │                   │       │           index-BY2S1tHT.css
│   │   │                   │       │           index-C_5TMfeg.js
│   │   │                   │       │           safari-na3_-uQk.svg
│   │   │                   │       │
│   │   │                   │       ├───htmlReport
│   │   │                   │       │       index.html
│   │   │                   │       │       report.css
│   │   │                   │       │       report.js
│   │   │                   │       │
│   │   │                   │       ├───recorder
│   │   │                   │       │   │   index.html
│   │   │                   │       │   │   playwright-logo.svg
│   │   │                   │       │   │
│   │   │                   │       │   └───assets
│   │   │                   │       │           codeMirrorModule-DeBYQozu.js
│   │   │                   │       │           codeMirrorModule-DYBRYzYX.css
│   │   │                   │       │           codicon-DCmgc-ay.ttf
│   │   │                   │       │           index-4ZiSSCmn.css
│   │   │                   │       │           index-Bq-mQf8S.js
│   │   │                   │       │
│   │   │                   │       └───traceViewer
│   │   │                   │           │   codeMirrorModule.DYBRYzYX.css
│   │   │                   │           │   codicon.DCmgc-ay.ttf
│   │   │                   │           │   defaultSettingsView.CjdS-WJx.css
│   │   │                   │           │   index.CzXZzn5A.css
│   │   │                   │           │   index.DMMX1gXU.js
│   │   │                   │           │   index.html
│   │   │                   │           │   manifest.webmanifest
│   │   │                   │           │   playwright-logo.svg
│   │   │                   │           │   snapshot.html
│   │   │                   │           │   snapshot.v8KI4P3m.js
│   │   │                   │           │   sw.bundle.js
│   │   │                   │           │   uiMode.BZQ54Kgt.css
│   │   │                   │           │   uiMode.html
│   │   │                   │           │   uiMode.Ut8wwJNp.js
│   │   │                   │           │   xtermModule.DYP7pi_n.css
│   │   │                   │           │
│   │   │                   │           └───assets
│   │   │                   │                   codeMirrorModule-LEHpjmcn.js
│   │   │                   │                   defaultSettingsView-BNmKHKpQ.js
│   │   │                   │                   urlMatch-BYQrIQwR.js
│   │   │                   │                   xtermModule-CsJ4vdCR.js
│   │   │                   │
│   │   │                   └───types
│   │   │                           protocol.d.ts
│   │   │                           structs.d.ts
│   │   │                           types.d.ts
│   │   │
│   │   ├───Browser
│   │   │       BrowserFactory.cs
│   │   │       BrowserOptions.cs
│   │   │       BrowserSession.cs
│   │   │
│   │   ├───Components
│   │   │       BookingFormComponent.cs
│   │   │       EditRoomComponent.cs
│   │   │       RoomCardComponent.cs
│   │   │
│   │   ├───obj
│   │   │   │   project.assets.json
│   │   │   │   project.nuget.cache
│   │   │   │   RBP.Business.Ui.csproj.nuget.dgspec.json
│   │   │   │   RBP.Business.Ui.csproj.nuget.g.props
│   │   │   │   RBP.Business.Ui.csproj.nuget.g.targets
│   │   │   │
│   │   │   └───Debug
│   │   │       └───net8.0
│   │   │           │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│   │   │           │   RBP.Busi.12B0E083.Up2Date
│   │   │           │   RBP.Business.Ui.AssemblyInfo.cs
│   │   │           │   RBP.Business.Ui.AssemblyInfoInputs.cache
│   │   │           │   RBP.Business.Ui.assets.cache
│   │   │           │   RBP.Business.Ui.csproj.AssemblyReference.cache
│   │   │           │   RBP.Business.Ui.csproj.BuildWithSkipAnalyzers
│   │   │           │   RBP.Business.Ui.csproj.CoreCompileInputs.cache
│   │   │           │   RBP.Business.Ui.csproj.FileListAbsolute.txt
│   │   │           │   RBP.Business.Ui.dll
│   │   │           │   RBP.Business.Ui.GeneratedMSBuildEditorConfig.editorconfig
│   │   │           │   RBP.Business.Ui.GlobalUsings.g.cs
│   │   │           │   RBP.Business.Ui.pdb
│   │   │           │   RBP.Business.Ui.sourcelink.json
│   │   │           │
│   │   │           ├───ref
│   │   │           │       RBP.Business.Ui.dll
│   │   │           │
│   │   │           └───refint
│   │   │                   RBP.Business.Ui.dll
│   │   │
│   │   ├───Pages
│   │   │   │   BasePage.cs
│   │   │   │   HomePage.cs
│   │   │   │   RoomDetailsPage.cs
│   │   │   │
│   │   │   └───Admin
│   │   │           AdminLoginPage.cs
│   │   │           AdminRoomsPage.cs
│   │   │           BaseAdminPage.cs
│   │   │
│   │   └───Steps
│   │           BookingSteps.cs
│   │
│   ├───RBP.Core
│   │   │   RBP.Core.csproj
│   │   │
│   │   ├───Authentication
│   │   │       AuthenticationState.cs
│   │   │       TokenProvider.cs
│   │   │
│   │   ├───bin
│   │   │   ├───Debug
│   │   │   │   └───net8.0
│   │   │   │           RBP.Core.deps.json
│   │   │   │           RBP.Core.dll
│   │   │   │           RBP.Core.pdb
│   │   │   │           RestfulBooker.Core.deps.json
│   │   │   │           RestfulBooker.Core.dll
│   │   │   │           RestfulBooker.Core.pdb
│   │   │   │
│   │   │   └───Release
│   │   │       └───net8.0
│   │   ├───Configuration
│   │   │       ApiSettings.cs
│   │   │       AppConfig.cs
│   │   │       ConfigurationProvider.cs
│   │   │       ConfigurationService.cs
│   │   │       CredentialsSettings.cs
│   │   │       LoggingSettings.cs
│   │   │       ReportPortalSettings.cs
│   │   │       UiSettings.cs
│   │   │
│   │   ├───Constants
│   │   ├───Enums
│   │   │       BrowserType.cs
│   │   │
│   │   ├───Exceptions
│   │   │       ApiExecption.cs
│   │   │       ConfigurationException.cs
│   │   │
│   │   ├───Extensions
│   │   │       ApiResponseExtensions.cs
│   │   │
│   │   ├───Helpers
│   │   │       StringNormalizer.cs
│   │   │
│   │   ├───Logging
│   │   │       LogContext.cs
│   │   │       LoggerFactory.cs
│   │   │       LoggerManager.cs
│   │   │       LoggingConfiguration.cs
│   │   │       LogMessages.cs
│   │   │
│   │   ├───obj
│   │   │   │   project.assets.json
│   │   │   │   project.nuget.cache
│   │   │   │   RBP.Core.csproj.nuget.dgspec.json
│   │   │   │   RBP.Core.csproj.nuget.g.props
│   │   │   │   RBP.Core.csproj.nuget.g.targets
│   │   │   │   RestfulBooker.Core.csproj.nuget.dgspec.json
│   │   │   │   RestfulBooker.Core.csproj.nuget.g.props
│   │   │   │   RestfulBooker.Core.csproj.nuget.g.targets
│   │   │   │
│   │   │   ├───Debug
│   │   │   │   └───net8.0
│   │   │   │       │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│   │   │   │       │   RBP.Core.AssemblyInfo.cs
│   │   │   │       │   RBP.Core.AssemblyInfoInputs.cache
│   │   │   │       │   RBP.Core.assets.cache
│   │   │   │       │   RBP.Core.csproj.AssemblyReference.cache
│   │   │   │       │   RBP.Core.csproj.BuildWithSkipAnalyzers
│   │   │   │       │   RBP.Core.csproj.CoreCompileInputs.cache
│   │   │   │       │   RBP.Core.csproj.FileListAbsolute.txt
│   │   │   │       │   RBP.Core.dll
│   │   │   │       │   RBP.Core.GeneratedMSBuildEditorConfig.editorconfig
│   │   │   │       │   RBP.Core.GlobalUsings.g.cs
│   │   │   │       │   RBP.Core.pdb
│   │   │   │       │   RBP.Core.sourcelink.json
│   │   │   │       │   RestfulBooker.Core.AssemblyInfo.cs
│   │   │   │       │   RestfulBooker.Core.AssemblyInfoInputs.cache
│   │   │   │       │   RestfulBooker.Core.assets.cache
│   │   │   │       │   RestfulBooker.Core.csproj.AssemblyReference.cache
│   │   │   │       │   RestfulBooker.Core.csproj.BuildWithSkipAnalyzers
│   │   │   │       │   RestfulBooker.Core.csproj.CoreCompileInputs.cache
│   │   │   │       │   RestfulBooker.Core.csproj.FileListAbsolute.txt
│   │   │   │       │   RestfulBooker.Core.dll
│   │   │   │       │   RestfulBooker.Core.GeneratedMSBuildEditorConfig.editorconfig
│   │   │   │       │   RestfulBooker.Core.GlobalUsings.g.cs
│   │   │   │       │   RestfulBooker.Core.pdb
│   │   │   │       │
│   │   │   │       ├───ref
│   │   │   │       │       RBP.Core.dll
│   │   │   │       │       RestfulBooker.Core.dll
│   │   │   │       │
│   │   │   │       └───refint
│   │   │   │               RBP.Core.dll
│   │   │   │               RestfulBooker.Core.dll
│   │   │   │
│   │   │   └───Release
│   │   │       └───net8.0
│   │   │           │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│   │   │           │   RBP.Core.AssemblyInfo.cs
│   │   │           │   RBP.Core.AssemblyInfoInputs.cache
│   │   │           │   RBP.Core.assets.cache
│   │   │           │   RBP.Core.csproj.AssemblyReference.cache
│   │   │           │   RBP.Core.GeneratedMSBuildEditorConfig.editorconfig
│   │   │           │   RBP.Core.GlobalUsings.g.cs
│   │   │           │
│   │   │           ├───ref
│   │   │           └───refint
│   │   ├───Reporting
│   │   │       ReportPortalConfigurator.cs
│   │   │
│   │   └───Utilities
│   └───RBP.Data
│       │   RBP.Data.csproj
│       │
│       ├───bin
│       │   ├───Debug
│       │   │   └───net8.0
│       │   │           RBP.Data.deps.json
│       │   │           RBP.Data.dll
│       │   │           RBP.Data.pdb
│       │   │           RestfulBooker.Data.deps.json
│       │   │           RestfulBooker.Data.dll
│       │   │           RestfulBooker.Data.pdb
│       │   │
│       │   └───Release
│       │       └───net8.0
│       ├───Builders
│       │   └───Base
│       │           BaseBuilder.cs
│       │
│       ├───DTO
│       │   │   RoomDto.cs
│       │   │
│       │   ├───Auth
│       │   │       LoginRequest.cs
│       │   │       LoginResponse.cs
│       │   │       TokenRequest.cs
│       │   │
│       │   ├───Booking
│       │   │       BookingDatesDto.cs
│       │   │       BookingDto.cs
│       │   │       BookingListDto.cs
│       │   │       BookingListResponse.cs
│       │   │       BookingRequest.cs
│       │   │       BookingResult.cs
│       │   │       GuestDto.cs
│       │   │
│       │   ├───Common
│       │   │       ApiResponse.cs
│       │   │
│       │   └───Room
│       │           RoomCardDto.cs
│       │           RoomsResponse.cs
│       │
│       ├───Enums
│       │       RoomType.cs
│       │
│       ├───Extensions
│       │       ApiResponseExtensions.cs
│       │
│       └───obj
│           │   project.assets.json
│           │   project.nuget.cache
│           │   RBP.Data.csproj.nuget.dgspec.json
│           │   RBP.Data.csproj.nuget.g.props
│           │   RBP.Data.csproj.nuget.g.targets
│           │   RestfulBooker.Data.csproj.nuget.dgspec.json
│           │   RestfulBooker.Data.csproj.nuget.g.props
│           │   RestfulBooker.Data.csproj.nuget.g.targets
│           │
│           ├───Debug
│           │   └───net8.0
│           │       │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│           │       │   RBP.Data.AssemblyInfo.cs
│           │       │   RBP.Data.AssemblyInfoInputs.cache
│           │       │   RBP.Data.assets.cache
│           │       │   RBP.Data.csproj.AssemblyReference.cache
│           │       │   RBP.Data.csproj.BuildWithSkipAnalyzers
│           │       │   RBP.Data.csproj.CoreCompileInputs.cache
│           │       │   RBP.Data.csproj.FileListAbsolute.txt
│           │       │   RBP.Data.dll
│           │       │   RBP.Data.GeneratedMSBuildEditorConfig.editorconfig
│           │       │   RBP.Data.GlobalUsings.g.cs
│           │       │   RBP.Data.pdb
│           │       │   RBP.Data.sourcelink.json
│           │       │   RestfulBooker.Data.AssemblyInfo.cs
│           │       │   RestfulBooker.Data.AssemblyInfoInputs.cache
│           │       │   RestfulBooker.Data.assets.cache
│           │       │   RestfulBooker.Data.csproj.AssemblyReference.cache
│           │       │   RestfulBooker.Data.csproj.BuildWithSkipAnalyzers
│           │       │   RestfulBooker.Data.csproj.CoreCompileInputs.cache
│           │       │   RestfulBooker.Data.csproj.FileListAbsolute.txt
│           │       │   RestfulBooker.Data.dll
│           │       │   RestfulBooker.Data.GeneratedMSBuildEditorConfig.editorconfig
│           │       │   RestfulBooker.Data.GlobalUsings.g.cs
│           │       │   RestfulBooker.Data.pdb
│           │       │
│           │       ├───ref
│           │       │       RBP.Data.dll
│           │       │       RestfulBooker.Data.dll
│           │       │
│           │       └───refint
│           │               RBP.Data.dll
│           │               RestfulBooker.Data.dll
│           │
│           └───Release
│               └───net8.0
│                   │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│                   │   RBP.Data.AssemblyInfo.cs
│                   │   RBP.Data.AssemblyInfoInputs.cache
│                   │   RBP.Data.assets.cache
│                   │   RBP.Data.csproj.AssemblyReference.cache
│                   │   RBP.Data.GeneratedMSBuildEditorConfig.editorconfig
│                   │   RBP.Data.GlobalUsings.g.cs
│                   │
│                   ├───ref
│                   └───refint
├───tests
│   ├───RBP.Tests.Api
│   │   │   ExecutionContext.cs
│   │   │   ParallelSettings.cs
│   │   │   RBP.Tests.Api.csproj
│   │   │   RBP.Tests.Api.csproj.Backup.tmp
│   │   │   ReportPortal.json
│   │   │
│   │   ├───API
│   │   │       BaseApiFixture.cs
│   │   │
│   │   ├───Base
│   │   │       BaseFixture.cs
│   │   │
│   │   ├───bin
│   │   │   ├───Debug
│   │   │   │   └───net8.0
│   │   │   │       │   AwesomeAssertions.dll
│   │   │   │       │   CoverletSourceRootsMapping_RBP.Tests.Api
│   │   │   │       │   CoverletSourceRootsMapping_RestfulBooker.Tests
│   │   │   │       │   FluentAssertions.dll
│   │   │   │       │   Microsoft.Bcl.AsyncInterfaces.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.Abstractions.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.Binder.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.EnvironmentVariables.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.FileExtensions.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.Json.dll
│   │   │   │       │   Microsoft.Extensions.FileProviders.Abstractions.dll
│   │   │   │       │   Microsoft.Extensions.FileProviders.Physical.dll
│   │   │   │       │   Microsoft.Extensions.FileSystemGlobbing.dll
│   │   │   │       │   Microsoft.Extensions.Primitives.dll
│   │   │   │       │   Microsoft.Playwright.dll
│   │   │   │       │   Microsoft.TestPlatform.CommunicationUtilities.dll
│   │   │   │       │   Microsoft.TestPlatform.CoreUtilities.dll
│   │   │   │       │   Microsoft.TestPlatform.CrossPlatEngine.dll
│   │   │   │       │   Microsoft.TestPlatform.PlatformAbstractions.dll
│   │   │   │       │   Microsoft.TestPlatform.Utilities.dll
│   │   │   │       │   Microsoft.VisualStudio.CodeCoverage.Shim.dll
│   │   │   │       │   Microsoft.VisualStudio.TestPlatform.Common.dll
│   │   │   │       │   Microsoft.VisualStudio.TestPlatform.ObjectModel.dll
│   │   │   │       │   Newtonsoft.Json.dll
│   │   │   │       │   NuGet.Frameworks.dll
│   │   │   │       │   nunit.engine.api.dll
│   │   │   │       │   nunit.engine.core.dll
│   │   │   │       │   nunit.engine.dll
│   │   │   │       │   nunit.framework.dll
│   │   │   │       │   NUnit3.TestAdapter.dll
│   │   │   │       │   NUnit3.TestAdapter.pdb
│   │   │   │       │   nunit_random_seed.tmp
│   │   │   │       │   playwright.ps1
│   │   │   │       │   RBP.Business.Api.dll
│   │   │   │       │   RBP.Business.Api.pdb
│   │   │   │       │   RBP.Core.dll
│   │   │   │       │   RBP.Core.pdb
│   │   │   │       │   RBP.Data.dll
│   │   │   │       │   RBP.Data.pdb
│   │   │   │       │   RBP.Tests.Api.deps.json
│   │   │   │       │   RBP.Tests.Api.dll
│   │   │   │       │   RBP.Tests.Api.pdb
│   │   │   │       │   RBP.Tests.Api.runtimeconfig.json
│   │   │   │       │   ReportPortal.addins
│   │   │   │       │   ReportPortal.Client.dll
│   │   │   │       │   ReportPortal.NUnitExtension.dll
│   │   │   │       │   ReportPortal.NUnitExtension.LogHandler.dll
│   │   │   │       │   ReportPortal.NUnitExtension.pdb
│   │   │   │       │   ReportPortal.Serilog.dll
│   │   │   │       │   ReportPortal.Shared.dll
│   │   │   │       │   RestfulBooker.Api.dll
│   │   │   │       │   RestfulBooker.Api.pdb
│   │   │   │       │   RestfulBooker.Core.dll
│   │   │   │       │   RestfulBooker.Core.pdb
│   │   │   │       │   RestfulBooker.Data.dll
│   │   │   │       │   RestfulBooker.Data.pdb
│   │   │   │       │   RestfulBooker.Tests.deps.json
│   │   │   │       │   RestfulBooker.Tests.dll
│   │   │   │       │   RestfulBooker.Tests.pdb
│   │   │   │       │   RestfulBooker.Tests.runtimeconfig.json
│   │   │   │       │   RestfulBooker.UI.dll
│   │   │   │       │   RestfulBooker.UI.pdb
│   │   │   │       │   RestSharp.dll
│   │   │   │       │   Serilog.dll
│   │   │   │       │   Serilog.Sinks.Console.dll
│   │   │   │       │   Serilog.Sinks.File.dll
│   │   │   │       │   System.IO.Pipelines.dll
│   │   │   │       │   System.Text.Encodings.Web.dll
│   │   │   │       │   System.Text.Json.dll
│   │   │   │       │   testcentric.engine.metadata.dll
│   │   │   │       │   testhost.dll
│   │   │   │       │   testhost.exe
│   │   │   │       │
│   │   │   │       ├───.playwright
│   │   │   │       │   ├───node
│   │   │   │       │   │   │   LICENSE
│   │   │   │       │   │   │
│   │   │   │       │   │   └───win32_x64
│   │   │   │       │   │           node.exe
│   │   │   │       │   │
│   │   │   │       │   └───package
│   │   │   │       │       │   browsers.json
│   │   │   │       │       │   cli.js
│   │   │   │       │       │   index.d.ts
│   │   │   │       │       │   index.js
│   │   │   │       │       │   index.mjs
│   │   │   │       │       │   LICENSE
│   │   │   │       │       │   NOTICE
│   │   │   │       │       │   package.json
│   │   │   │       │       │   README.md
│   │   │   │       │       │   ThirdPartyNotices.txt
│   │   │   │       │       │
│   │   │   │       │       ├───bin
│   │   │   │       │       │       install_media_pack.ps1
│   │   │   │       │       │       install_webkit_wsl.ps1
│   │   │   │       │       │       reinstall_chrome_beta_linux.sh
│   │   │   │       │       │       reinstall_chrome_beta_mac.sh
│   │   │   │       │       │       reinstall_chrome_beta_win.ps1
│   │   │   │       │       │       reinstall_chrome_stable_linux.sh
│   │   │   │       │       │       reinstall_chrome_stable_mac.sh
│   │   │   │       │       │       reinstall_chrome_stable_win.ps1
│   │   │   │       │       │       reinstall_msedge_beta_linux.sh
│   │   │   │       │       │       reinstall_msedge_beta_mac.sh
│   │   │   │       │       │       reinstall_msedge_beta_win.ps1
│   │   │   │       │       │       reinstall_msedge_dev_linux.sh
│   │   │   │       │       │       reinstall_msedge_dev_mac.sh
│   │   │   │       │       │       reinstall_msedge_dev_win.ps1
│   │   │   │       │       │       reinstall_msedge_stable_linux.sh
│   │   │   │       │       │       reinstall_msedge_stable_mac.sh
│   │   │   │       │       │       reinstall_msedge_stable_win.ps1
│   │   │   │       │       │
│   │   │   │       │       ├───lib
│   │   │   │       │       │   │   bootstrap.js
│   │   │   │       │       │   │   coreBundle.js
│   │   │   │       │       │   │   package.js
│   │   │   │       │       │   │   serverRegistry.js
│   │   │   │       │       │   │   serverRegistry.js.LICENSE
│   │   │   │       │       │   │   utilsBundle.js
│   │   │   │       │       │   │   utilsBundle.js.LICENSE
│   │   │   │       │       │   │   xdg-open
│   │   │   │       │       │   │
│   │   │   │       │       │   ├───entry
│   │   │   │       │       │   │       cliDaemon.js
│   │   │   │       │       │   │       dashboardApp.js
│   │   │   │       │       │   │       mcp.js
│   │   │   │       │       │   │       oopBrowserDownload.js
│   │   │   │       │       │   │
│   │   │   │       │       │   ├───server
│   │   │   │       │       │   │   │   deviceDescriptorsSource.json
│   │   │   │       │       │   │   │
│   │   │   │       │       │   │   ├───chromium
│   │   │   │       │       │   │   │       appIcon.png
│   │   │   │       │       │   │   │
│   │   │   │       │       │   │   └───electron
│   │   │   │       │       │   │           loader.js
│   │   │   │       │       │   │
│   │   │   │       │       │   ├───tools
│   │   │   │       │       │   │   ├───cli-client
│   │   │   │       │       │   │   │   │   channelSessions.js
│   │   │   │       │       │   │   │   │   cli.js
│   │   │   │       │       │   │   │   │   help.json
│   │   │   │       │       │   │   │   │   minimist.js
│   │   │   │       │       │   │   │   │   output.js
│   │   │   │       │       │   │   │   │   program.js
│   │   │   │       │       │   │   │   │   registry.js
│   │   │   │       │       │   │   │   │   session.js
│   │   │   │       │       │   │   │   │
│   │   │   │       │       │   │   │   └───skill
│   │   │   │       │       │   │   │       │   SKILL.md
│   │   │   │       │       │   │   │       │
│   │   │   │       │       │   │   │       └───references
│   │   │   │       │       │   │   │               element-attributes.md
│   │   │   │       │       │   │   │               playwright-tests.md
│   │   │   │       │       │   │   │               request-mocking.md
│   │   │   │       │       │   │   │               running-code.md
│   │   │   │       │       │   │   │               session-management.md
│   │   │   │       │       │   │   │               spec-driven-testing.md
│   │   │   │       │       │   │   │               storage-state.md
│   │   │   │       │       │   │   │               test-generation.md
│   │   │   │       │       │   │   │               tracing.md
│   │   │   │       │       │   │   │               video-recording.md
│   │   │   │       │       │   │   │
│   │   │   │       │       │   │   ├───dashboard
│   │   │   │       │       │   │   │       appIcon.png
│   │   │   │       │       │   │   │
│   │   │   │       │       │   │   ├───trace
│   │   │   │       │       │   │   │       SKILL.md
│   │   │   │       │       │   │   │
│   │   │   │       │       │   │   └───utils
│   │   │   │       │       │   │           extension.js
│   │   │   │       │       │   │           socketConnection.js
│   │   │   │       │       │   │
│   │   │   │       │       │   └───vite
│   │   │   │       │       │       ├───dashboard
│   │   │   │       │       │       │   │   index.html
│   │   │   │       │       │       │   │   playwright-logo.svg
│   │   │   │       │       │       │   │
│   │   │   │       │       │       │   └───assets
│   │   │   │       │       │       │           codicon-DCmgc-ay.ttf
│   │   │   │       │       │       │           firefox-1bWoP6pv.svg
│   │   │   │       │       │       │           firefox-beta-k3eOH_eK.svg
│   │   │   │       │       │       │           firefox-nightly-Cp5nfeDT.svg
│   │   │   │       │       │       │           index-BY2S1tHT.css
│   │   │   │       │       │       │           index-C_5TMfeg.js
│   │   │   │       │       │       │           safari-na3_-uQk.svg
│   │   │   │       │       │       │
│   │   │   │       │       │       ├───htmlReport
│   │   │   │       │       │       │       index.html
│   │   │   │       │       │       │       report.css
│   │   │   │       │       │       │       report.js
│   │   │   │       │       │       │
│   │   │   │       │       │       ├───recorder
│   │   │   │       │       │       │   │   index.html
│   │   │   │       │       │       │   │   playwright-logo.svg
│   │   │   │       │       │       │   │
│   │   │   │       │       │       │   └───assets
│   │   │   │       │       │       │           codeMirrorModule-DeBYQozu.js
│   │   │   │       │       │       │           codeMirrorModule-DYBRYzYX.css
│   │   │   │       │       │       │           codicon-DCmgc-ay.ttf
│   │   │   │       │       │       │           index-4ZiSSCmn.css
│   │   │   │       │       │       │           index-Bq-mQf8S.js
│   │   │   │       │       │       │
│   │   │   │       │       │       └───traceViewer
│   │   │   │       │       │           │   codeMirrorModule.DYBRYzYX.css
│   │   │   │       │       │           │   codicon.DCmgc-ay.ttf
│   │   │   │       │       │           │   defaultSettingsView.CjdS-WJx.css
│   │   │   │       │       │           │   index.CzXZzn5A.css
│   │   │   │       │       │           │   index.DMMX1gXU.js
│   │   │   │       │       │           │   index.html
│   │   │   │       │       │           │   manifest.webmanifest
│   │   │   │       │       │           │   playwright-logo.svg
│   │   │   │       │       │           │   snapshot.html
│   │   │   │       │       │           │   snapshot.v8KI4P3m.js
│   │   │   │       │       │           │   sw.bundle.js
│   │   │   │       │       │           │   uiMode.BZQ54Kgt.css
│   │   │   │       │       │           │   uiMode.html
│   │   │   │       │       │           │   uiMode.Ut8wwJNp.js
│   │   │   │       │       │           │   xtermModule.DYP7pi_n.css
│   │   │   │       │       │           │
│   │   │   │       │       │           └───assets
│   │   │   │       │       │                   codeMirrorModule-LEHpjmcn.js
│   │   │   │       │       │                   defaultSettingsView-BNmKHKpQ.js
│   │   │   │       │       │                   urlMatch-BYQrIQwR.js
│   │   │   │       │       │                   xtermModule-CsJ4vdCR.js
│   │   │   │       │       │
│   │   │   │       │       └───types
│   │   │   │       │               protocol.d.ts
│   │   │   │       │               structs.d.ts
│   │   │   │       │               types.d.ts
│   │   │   │       │
│   │   │   │       ├───cs
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───de
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───es
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───fr
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───it
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───ja
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───ko
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───Logs
│   │   │   │       │       automation-20260722.log
│   │   │   │       │       automation-20260723.log
│   │   │   │       │
│   │   │   │       ├───pl
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───pt-BR
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───ru
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───runtimes
│   │   │   │       │   └───browser
│   │   │   │       │       └───lib
│   │   │   │       │           └───net8.0
│   │   │   │       │                   System.Text.Encodings.Web.dll
│   │   │   │       │
│   │   │   │       ├───tr
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───zh-Hans
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       └───zh-Hant
│   │   │   │               Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │               Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │               Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │               Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │               Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │
│   │   │   └───Release
│   │   │       └───net8.0
│   │   │               CoverletSourceRootsMapping_RBP.Tests.Api
│   │   │
│   │   ├───Infrastructure
│   │   ├───obj
│   │   │   │   project.assets.json
│   │   │   │   project.nuget.cache
│   │   │   │   RBP.Tests.Api.csproj.nuget.dgspec.json
│   │   │   │   RBP.Tests.Api.csproj.nuget.g.props
│   │   │   │   RBP.Tests.Api.csproj.nuget.g.targets
│   │   │   │   RestfulBooker.Tests.csproj.nuget.dgspec.json
│   │   │   │   RestfulBooker.Tests.csproj.nuget.g.props
│   │   │   │   RestfulBooker.Tests.csproj.nuget.g.targets
│   │   │   │
│   │   │   ├───Debug
│   │   │   │   └───net8.0
│   │   │   │       │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│   │   │   │       │   RBP.Test.E154710D.Up2Date
│   │   │   │       │   RBP.Tests.Api.AssemblyInfo.cs
│   │   │   │       │   RBP.Tests.Api.AssemblyInfoInputs.cache
│   │   │   │       │   RBP.Tests.Api.assets.cache
│   │   │   │       │   RBP.Tests.Api.csproj.AssemblyReference.cache
│   │   │   │       │   RBP.Tests.Api.csproj.BuildWithSkipAnalyzers
│   │   │   │       │   RBP.Tests.Api.csproj.CoreCompileInputs.cache
│   │   │   │       │   RBP.Tests.Api.csproj.FileListAbsolute.txt
│   │   │   │       │   RBP.Tests.Api.dll
│   │   │   │       │   RBP.Tests.Api.GeneratedMSBuildEditorConfig.editorconfig
│   │   │   │       │   RBP.Tests.Api.genruntimeconfig.cache
│   │   │   │       │   RBP.Tests.Api.GlobalUsings.g.cs
│   │   │   │       │   RBP.Tests.Api.pdb
│   │   │   │       │   RBP.Tests.Api.sourcelink.json
│   │   │   │       │   ReportPortal.addins
│   │   │   │       │   RestfulB.E703FF74.Up2Date
│   │   │   │       │   RestfulBooker.Tests.AssemblyInfo.cs
│   │   │   │       │   RestfulBooker.Tests.AssemblyInfoInputs.cache
│   │   │   │       │   RestfulBooker.Tests.assets.cache
│   │   │   │       │   RestfulBooker.Tests.csproj.AssemblyReference.cache
│   │   │   │       │   RestfulBooker.Tests.csproj.BuildWithSkipAnalyzers
│   │   │   │       │   RestfulBooker.Tests.csproj.CoreCompileInputs.cache
│   │   │   │       │   RestfulBooker.Tests.csproj.FileListAbsolute.txt
│   │   │   │       │   RestfulBooker.Tests.dll
│   │   │   │       │   RestfulBooker.Tests.GeneratedMSBuildEditorConfig.editorconfig
│   │   │   │       │   RestfulBooker.Tests.genruntimeconfig.cache
│   │   │   │       │   RestfulBooker.Tests.GlobalUsings.g.cs
│   │   │   │       │   RestfulBooker.Tests.pdb
│   │   │   │       │
│   │   │   │       ├───ref
│   │   │   │       │       RBP.Tests.Api.dll
│   │   │   │       │       RestfulBooker.Tests.dll
│   │   │   │       │
│   │   │   │       └───refint
│   │   │   │               RBP.Tests.Api.dll
│   │   │   │               RestfulBooker.Tests.dll
│   │   │   │
│   │   │   └───Release
│   │   │       └───net8.0
│   │   │           │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│   │   │           │   RBP.Tests.Api.AssemblyInfo.cs
│   │   │           │   RBP.Tests.Api.AssemblyInfoInputs.cache
│   │   │           │   RBP.Tests.Api.assets.cache
│   │   │           │   RBP.Tests.Api.csproj.AssemblyReference.cache
│   │   │           │   RBP.Tests.Api.GeneratedMSBuildEditorConfig.editorconfig
│   │   │           │   RBP.Tests.Api.GlobalUsings.g.cs
│   │   │           │
│   │   │           ├───ref
│   │   │           └───refint
│   │   ├───Regression
│   │   ├───Smoke
│   │   │   │   LoginTestsFixture.cs
│   │   │   │
│   │   │   └───Api
│   │   │       └───Rooms
│   │   └───UI
│   ├───RBP.Tests.E2E
│   │   │   ParallelSettings.cs
│   │   │   RBP.Tests.E2E.csproj
│   │   │   ReportPortal.json
│   │   │
│   │   ├───Assertions
│   │   │       HomePageAssertions.cs
│   │   │       RoomDetailsPageAssertions.cs
│   │   │
│   │   ├───Base
│   │   │       BaseFixture.cs
│   │   │
│   │   ├───bin
│   │   │   ├───Debug
│   │   │   │   └───net8.0
│   │   │   │       │   AwesomeAssertions.dll
│   │   │   │       │   CoverletSourceRootsMapping_E2E
│   │   │   │       │   CoverletSourceRootsMapping_RBP.Tests.E2E
│   │   │   │       │   Microsoft.Bcl.AsyncInterfaces.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.Abstractions.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.Binder.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.EnvironmentVariables.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.FileExtensions.dll
│   │   │   │       │   Microsoft.Extensions.Configuration.Json.dll
│   │   │   │       │   Microsoft.Extensions.FileProviders.Abstractions.dll
│   │   │   │       │   Microsoft.Extensions.FileProviders.Physical.dll
│   │   │   │       │   Microsoft.Extensions.FileSystemGlobbing.dll
│   │   │   │       │   Microsoft.Extensions.Primitives.dll
│   │   │   │       │   Microsoft.Playwright.dll
│   │   │   │       │   Microsoft.TestPlatform.CommunicationUtilities.dll
│   │   │   │       │   Microsoft.TestPlatform.CoreUtilities.dll
│   │   │   │       │   Microsoft.TestPlatform.CrossPlatEngine.dll
│   │   │   │       │   Microsoft.TestPlatform.PlatformAbstractions.dll
│   │   │   │       │   Microsoft.TestPlatform.Utilities.dll
│   │   │   │       │   Microsoft.VisualStudio.CodeCoverage.Shim.dll
│   │   │   │       │   Microsoft.VisualStudio.TestPlatform.Common.dll
│   │   │   │       │   Microsoft.VisualStudio.TestPlatform.ObjectModel.dll
│   │   │   │       │   Newtonsoft.Json.dll
│   │   │   │       │   NuGet.Frameworks.dll
│   │   │   │       │   nunit.engine.api.dll
│   │   │   │       │   nunit.engine.core.dll
│   │   │   │       │   nunit.engine.dll
│   │   │   │       │   nunit.framework.dll
│   │   │   │       │   NUnit3.TestAdapter.dll
│   │   │   │       │   NUnit3.TestAdapter.pdb
│   │   │   │       │   nunit_random_seed.tmp
│   │   │   │       │   playwright.ps1
│   │   │   │       │   RBP.Business.Api.dll
│   │   │   │       │   RBP.Business.Api.pdb
│   │   │   │       │   RBP.Business.Ui.dll
│   │   │   │       │   RBP.Business.Ui.pdb
│   │   │   │       │   RBP.Core.dll
│   │   │   │       │   RBP.Core.pdb
│   │   │   │       │   RBP.Data.dll
│   │   │   │       │   RBP.Data.pdb
│   │   │   │       │   RBP.Tests.E2E.deps.json
│   │   │   │       │   RBP.Tests.E2E.dll
│   │   │   │       │   RBP.Tests.E2E.pdb
│   │   │   │       │   RBP.Tests.E2E.runtimeconfig.json
│   │   │   │       │   RBP.Tests.Ui.deps.json
│   │   │   │       │   RBP.Tests.Ui.dll
│   │   │   │       │   RBP.Tests.Ui.pdb
│   │   │   │       │   RBP.Tests.Ui.runtimeconfig.json
│   │   │   │       │   ReportPortal.addins
│   │   │   │       │   ReportPortal.Client.dll
│   │   │   │       │   ReportPortal.json
│   │   │   │       │   ReportPortal.NUnitExtension.dll
│   │   │   │       │   ReportPortal.NUnitExtension.LogHandler.dll
│   │   │   │       │   ReportPortal.NUnitExtension.pdb
│   │   │   │       │   ReportPortal.Serilog.dll
│   │   │   │       │   ReportPortal.Shared.dll
│   │   │   │       │   RestSharp.dll
│   │   │   │       │   Serilog.dll
│   │   │   │       │   Serilog.Sinks.Console.dll
│   │   │   │       │   Serilog.Sinks.File.dll
│   │   │   │       │   System.IO.Pipelines.dll
│   │   │   │       │   System.Text.Encodings.Web.dll
│   │   │   │       │   System.Text.Json.dll
│   │   │   │       │   testcentric.engine.metadata.dll
│   │   │   │       │   testhost.dll
│   │   │   │       │   testhost.exe
│   │   │   │       │
│   │   │   │       ├───.playwright
│   │   │   │       │   ├───node
│   │   │   │       │   │   │   LICENSE
│   │   │   │       │   │   │
│   │   │   │       │   │   └───win32_x64
│   │   │   │       │   │           node.exe
│   │   │   │       │   │
│   │   │   │       │   └───package
│   │   │   │       │       │   browsers.json
│   │   │   │       │       │   cli.js
│   │   │   │       │       │   index.d.ts
│   │   │   │       │       │   index.js
│   │   │   │       │       │   index.mjs
│   │   │   │       │       │   LICENSE
│   │   │   │       │       │   NOTICE
│   │   │   │       │       │   package.json
│   │   │   │       │       │   README.md
│   │   │   │       │       │   ThirdPartyNotices.txt
│   │   │   │       │       │
│   │   │   │       │       ├───bin
│   │   │   │       │       │       install_media_pack.ps1
│   │   │   │       │       │       install_webkit_wsl.ps1
│   │   │   │       │       │       reinstall_chrome_beta_linux.sh
│   │   │   │       │       │       reinstall_chrome_beta_mac.sh
│   │   │   │       │       │       reinstall_chrome_beta_win.ps1
│   │   │   │       │       │       reinstall_chrome_stable_linux.sh
│   │   │   │       │       │       reinstall_chrome_stable_mac.sh
│   │   │   │       │       │       reinstall_chrome_stable_win.ps1
│   │   │   │       │       │       reinstall_msedge_beta_linux.sh
│   │   │   │       │       │       reinstall_msedge_beta_mac.sh
│   │   │   │       │       │       reinstall_msedge_beta_win.ps1
│   │   │   │       │       │       reinstall_msedge_dev_linux.sh
│   │   │   │       │       │       reinstall_msedge_dev_mac.sh
│   │   │   │       │       │       reinstall_msedge_dev_win.ps1
│   │   │   │       │       │       reinstall_msedge_stable_linux.sh
│   │   │   │       │       │       reinstall_msedge_stable_mac.sh
│   │   │   │       │       │       reinstall_msedge_stable_win.ps1
│   │   │   │       │       │
│   │   │   │       │       ├───lib
│   │   │   │       │       │   │   bootstrap.js
│   │   │   │       │       │   │   coreBundle.js
│   │   │   │       │       │   │   package.js
│   │   │   │       │       │   │   serverRegistry.js
│   │   │   │       │       │   │   serverRegistry.js.LICENSE
│   │   │   │       │       │   │   utilsBundle.js
│   │   │   │       │       │   │   utilsBundle.js.LICENSE
│   │   │   │       │       │   │   xdg-open
│   │   │   │       │       │   │
│   │   │   │       │       │   ├───entry
│   │   │   │       │       │   │       cliDaemon.js
│   │   │   │       │       │   │       dashboardApp.js
│   │   │   │       │       │   │       mcp.js
│   │   │   │       │       │   │       oopBrowserDownload.js
│   │   │   │       │       │   │
│   │   │   │       │       │   ├───server
│   │   │   │       │       │   │   │   deviceDescriptorsSource.json
│   │   │   │       │       │   │   │
│   │   │   │       │       │   │   ├───chromium
│   │   │   │       │       │   │   │       appIcon.png
│   │   │   │       │       │   │   │
│   │   │   │       │       │   │   └───electron
│   │   │   │       │       │   │           loader.js
│   │   │   │       │       │   │
│   │   │   │       │       │   ├───tools
│   │   │   │       │       │   │   ├───cli-client
│   │   │   │       │       │   │   │   │   channelSessions.js
│   │   │   │       │       │   │   │   │   cli.js
│   │   │   │       │       │   │   │   │   help.json
│   │   │   │       │       │   │   │   │   minimist.js
│   │   │   │       │       │   │   │   │   output.js
│   │   │   │       │       │   │   │   │   program.js
│   │   │   │       │       │   │   │   │   registry.js
│   │   │   │       │       │   │   │   │   session.js
│   │   │   │       │       │   │   │   │
│   │   │   │       │       │   │   │   └───skill
│   │   │   │       │       │   │   │       │   SKILL.md
│   │   │   │       │       │   │   │       │
│   │   │   │       │       │   │   │       └───references
│   │   │   │       │       │   │   │               element-attributes.md
│   │   │   │       │       │   │   │               playwright-tests.md
│   │   │   │       │       │   │   │               request-mocking.md
│   │   │   │       │       │   │   │               running-code.md
│   │   │   │       │       │   │   │               session-management.md
│   │   │   │       │       │   │   │               spec-driven-testing.md
│   │   │   │       │       │   │   │               storage-state.md
│   │   │   │       │       │   │   │               test-generation.md
│   │   │   │       │       │   │   │               tracing.md
│   │   │   │       │       │   │   │               video-recording.md
│   │   │   │       │       │   │   │
│   │   │   │       │       │   │   ├───dashboard
│   │   │   │       │       │   │   │       appIcon.png
│   │   │   │       │       │   │   │
│   │   │   │       │       │   │   ├───trace
│   │   │   │       │       │   │   │       SKILL.md
│   │   │   │       │       │   │   │
│   │   │   │       │       │   │   └───utils
│   │   │   │       │       │   │           extension.js
│   │   │   │       │       │   │           socketConnection.js
│   │   │   │       │       │   │
│   │   │   │       │       │   └───vite
│   │   │   │       │       │       ├───dashboard
│   │   │   │       │       │       │   │   index.html
│   │   │   │       │       │       │   │   playwright-logo.svg
│   │   │   │       │       │       │   │
│   │   │   │       │       │       │   └───assets
│   │   │   │       │       │       │           codicon-DCmgc-ay.ttf
│   │   │   │       │       │       │           firefox-1bWoP6pv.svg
│   │   │   │       │       │       │           firefox-beta-k3eOH_eK.svg
│   │   │   │       │       │       │           firefox-nightly-Cp5nfeDT.svg
│   │   │   │       │       │       │           index-BY2S1tHT.css
│   │   │   │       │       │       │           index-C_5TMfeg.js
│   │   │   │       │       │       │           safari-na3_-uQk.svg
│   │   │   │       │       │       │
│   │   │   │       │       │       ├───htmlReport
│   │   │   │       │       │       │       index.html
│   │   │   │       │       │       │       report.css
│   │   │   │       │       │       │       report.js
│   │   │   │       │       │       │
│   │   │   │       │       │       ├───recorder
│   │   │   │       │       │       │   │   index.html
│   │   │   │       │       │       │   │   playwright-logo.svg
│   │   │   │       │       │       │   │
│   │   │   │       │       │       │   └───assets
│   │   │   │       │       │       │           codeMirrorModule-DeBYQozu.js
│   │   │   │       │       │       │           codeMirrorModule-DYBRYzYX.css
│   │   │   │       │       │       │           codicon-DCmgc-ay.ttf
│   │   │   │       │       │       │           index-4ZiSSCmn.css
│   │   │   │       │       │       │           index-Bq-mQf8S.js
│   │   │   │       │       │       │
│   │   │   │       │       │       └───traceViewer
│   │   │   │       │       │           │   codeMirrorModule.DYBRYzYX.css
│   │   │   │       │       │           │   codicon.DCmgc-ay.ttf
│   │   │   │       │       │           │   defaultSettingsView.CjdS-WJx.css
│   │   │   │       │       │           │   index.CzXZzn5A.css
│   │   │   │       │       │           │   index.DMMX1gXU.js
│   │   │   │       │       │           │   index.html
│   │   │   │       │       │           │   manifest.webmanifest
│   │   │   │       │       │           │   playwright-logo.svg
│   │   │   │       │       │           │   snapshot.html
│   │   │   │       │       │           │   snapshot.v8KI4P3m.js
│   │   │   │       │       │           │   sw.bundle.js
│   │   │   │       │       │           │   uiMode.BZQ54Kgt.css
│   │   │   │       │       │           │   uiMode.html
│   │   │   │       │       │           │   uiMode.Ut8wwJNp.js
│   │   │   │       │       │           │   xtermModule.DYP7pi_n.css
│   │   │   │       │       │           │
│   │   │   │       │       │           └───assets
│   │   │   │       │       │                   codeMirrorModule-LEHpjmcn.js
│   │   │   │       │       │                   defaultSettingsView-BNmKHKpQ.js
│   │   │   │       │       │                   urlMatch-BYQrIQwR.js
│   │   │   │       │       │                   xtermModule-CsJ4vdCR.js
│   │   │   │       │       │
│   │   │   │       │       └───types
│   │   │   │       │               protocol.d.ts
│   │   │   │       │               structs.d.ts
│   │   │   │       │               types.d.ts
│   │   │   │       │
│   │   │   │       ├───cs
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───de
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───es
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───fr
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───it
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───ja
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───ko
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───Logs
│   │   │   │       │       automation-20260729.log
│   │   │   │       │       automation-20260730.log
│   │   │   │       │       automation-20260731.log
│   │   │   │       │       automation-20260803.log
│   │   │   │       │       automation-20260804.log
│   │   │   │       │
│   │   │   │       ├───pl
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───pt-BR
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───ru
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───runtimes
│   │   │   │       │   └───browser
│   │   │   │       │       └───lib
│   │   │   │       │           └───net8.0
│   │   │   │       │                   System.Text.Encodings.Web.dll
│   │   │   │       │
│   │   │   │       ├───Screenshots
│   │   │   │       │       User_Should_Be_Able_To_Book_Room_20260804_135104.png
│   │   │   │       │
│   │   │   │       ├───tr
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       ├───zh-Hans
│   │   │   │       │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │       │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │       │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │       │
│   │   │   │       └───zh-Hant
│   │   │   │               Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│   │   │   │               Microsoft.TestPlatform.CoreUtilities.resources.dll
│   │   │   │               Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│   │   │   │               Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│   │   │   │               Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│   │   │   │
│   │   │   └───Release
│   │   │       └───net8.0
│   │   │               CoverletSourceRootsMapping_RBP.Tests.E2E
│   │   │
│   │   ├───obj
│   │   │   │   E2E.csproj.nuget.dgspec.json
│   │   │   │   E2E.csproj.nuget.g.props
│   │   │   │   E2E.csproj.nuget.g.targets
│   │   │   │   project.assets.json
│   │   │   │   project.nuget.cache
│   │   │   │   RBP.Tests.E2E.csproj.nuget.dgspec.json
│   │   │   │   RBP.Tests.E2E.csproj.nuget.g.props
│   │   │   │   RBP.Tests.E2E.csproj.nuget.g.targets
│   │   │   │
│   │   │   ├───Debug
│   │   │   │   └───net8.0
│   │   │   │       │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│   │   │   │       │   E2E.AssemblyInfo.cs
│   │   │   │       │   E2E.AssemblyInfoInputs.cache
│   │   │   │       │   E2E.assets.cache
│   │   │   │       │   E2E.csproj.AssemblyReference.cache
│   │   │   │       │   E2E.GeneratedMSBuildEditorConfig.editorconfig
│   │   │   │       │   E2E.GlobalUsings.g.cs
│   │   │   │       │   RBP.Test.2E0ADF20.Up2Date
│   │   │   │       │   RBP.Tests.E2E.AssemblyInfo.cs
│   │   │   │       │   RBP.Tests.E2E.AssemblyInfoInputs.cache
│   │   │   │       │   RBP.Tests.E2E.assets.cache
│   │   │   │       │   RBP.Tests.E2E.csproj.AssemblyReference.cache
│   │   │   │       │   RBP.Tests.E2E.csproj.BuildWithSkipAnalyzers
│   │   │   │       │   RBP.Tests.E2E.csproj.CoreCompileInputs.cache
│   │   │   │       │   RBP.Tests.E2E.csproj.FileListAbsolute.txt
│   │   │   │       │   RBP.Tests.E2E.dll
│   │   │   │       │   RBP.Tests.E2E.GeneratedMSBuildEditorConfig.editorconfig
│   │   │   │       │   RBP.Tests.E2E.genruntimeconfig.cache
│   │   │   │       │   RBP.Tests.E2E.GlobalUsings.g.cs
│   │   │   │       │   RBP.Tests.E2E.pdb
│   │   │   │       │   RBP.Tests.E2E.sourcelink.json
│   │   │   │       │   ReportPortal.addins
│   │   │   │       │
│   │   │   │       ├───ref
│   │   │   │       │       RBP.Tests.E2E.dll
│   │   │   │       │
│   │   │   │       └───refint
│   │   │   │               RBP.Tests.E2E.dll
│   │   │   │
│   │   │   └───Release
│   │   │       └───net8.0
│   │   │           │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│   │   │           │   RBP.Tests.E2E.AssemblyInfo.cs
│   │   │           │   RBP.Tests.E2E.AssemblyInfoInputs.cache
│   │   │           │   RBP.Tests.E2E.assets.cache
│   │   │           │   RBP.Tests.E2E.csproj.AssemblyReference.cache
│   │   │           │   RBP.Tests.E2E.GeneratedMSBuildEditorConfig.editorconfig
│   │   │           │   RBP.Tests.E2E.GlobalUsings.g.cs
│   │   │           │
│   │   │           ├───ref
│   │   │           └───refint
│   │   ├───TC01
│   │   │       RoomListDataMapping.cs
│   │   │
│   │   └───TC02
│   │           RoomBookingFixture.cs
│   │
│   └───RBP.Tests.Ui
│       │   ParallelSettings.cs
│       │   RBP.Tests.Ui.csproj
│       │   reportportal.json
│       │
│       ├───AdminPageFixtures
│       │       EditRoomFixture.cs
│       │
│       ├───Assertions
│       │       HomePageAssertions.cs
│       │       RoomCardAssertions.cs
│       │
│       ├───Base
│       │       BaseFixture.cs
│       │
│       ├───bin
│       │   └───Debug
│       │       └───net8.0
│       │           │   AwesomeAssertions.dll
│       │           │   CoverletSourceRootsMapping_RBP.Tests.Ui
│       │           │   FluentAssertions.dll
│       │           │   Microsoft.Bcl.AsyncInterfaces.dll
│       │           │   Microsoft.Extensions.Configuration.Abstractions.dll
│       │           │   Microsoft.Extensions.Configuration.Binder.dll
│       │           │   Microsoft.Extensions.Configuration.dll
│       │           │   Microsoft.Extensions.Configuration.EnvironmentVariables.dll
│       │           │   Microsoft.Extensions.Configuration.FileExtensions.dll
│       │           │   Microsoft.Extensions.Configuration.Json.dll
│       │           │   Microsoft.Extensions.FileProviders.Abstractions.dll
│       │           │   Microsoft.Extensions.FileProviders.Physical.dll
│       │           │   Microsoft.Extensions.FileSystemGlobbing.dll
│       │           │   Microsoft.Extensions.Primitives.dll
│       │           │   Microsoft.Playwright.dll
│       │           │   Microsoft.TestPlatform.CommunicationUtilities.dll
│       │           │   Microsoft.TestPlatform.CoreUtilities.dll
│       │           │   Microsoft.TestPlatform.CrossPlatEngine.dll
│       │           │   Microsoft.TestPlatform.PlatformAbstractions.dll
│       │           │   Microsoft.TestPlatform.Utilities.dll
│       │           │   Microsoft.VisualStudio.CodeCoverage.Shim.dll
│       │           │   Microsoft.VisualStudio.TestPlatform.Common.dll
│       │           │   Microsoft.VisualStudio.TestPlatform.ObjectModel.dll
│       │           │   Newtonsoft.Json.dll
│       │           │   NuGet.Frameworks.dll
│       │           │   nunit.engine.api.dll
│       │           │   nunit.engine.core.dll
│       │           │   nunit.engine.dll
│       │           │   nunit.framework.dll
│       │           │   NUnit3.TestAdapter.dll
│       │           │   NUnit3.TestAdapter.pdb
│       │           │   nunit_random_seed.tmp
│       │           │   playwright.ps1
│       │           │   RBP.Business.Ui.dll
│       │           │   RBP.Business.Ui.pdb
│       │           │   RBP.Core.dll
│       │           │   RBP.Core.pdb
│       │           │   RBP.Data.dll
│       │           │   RBP.Data.pdb
│       │           │   RBP.Tests.Ui.deps.json
│       │           │   RBP.Tests.Ui.dll
│       │           │   RBP.Tests.Ui.pdb
│       │           │   RBP.Tests.Ui.runtimeconfig.json
│       │           │   ReportPortal.addins
│       │           │   ReportPortal.Client.dll
│       │           │   reportportal.json
│       │           │   ReportPortal.NUnitExtension.dll
│       │           │   ReportPortal.NUnitExtension.LogHandler.dll
│       │           │   ReportPortal.NUnitExtension.pdb
│       │           │   ReportPortal.Serilog.dll
│       │           │   ReportPortal.Shared.dll
│       │           │   Serilog.dll
│       │           │   Serilog.Sinks.Console.dll
│       │           │   Serilog.Sinks.File.dll
│       │           │   System.IO.Pipelines.dll
│       │           │   System.Text.Encodings.Web.dll
│       │           │   System.Text.Json.dll
│       │           │   testcentric.engine.metadata.dll
│       │           │   testhost.dll
│       │           │   testhost.exe
│       │           │
│       │           ├───.playwright
│       │           │   ├───node
│       │           │   │   │   LICENSE
│       │           │   │   │
│       │           │   │   └───win32_x64
│       │           │   │           node.exe
│       │           │   │
│       │           │   └───package
│       │           │       │   browsers.json
│       │           │       │   cli.js
│       │           │       │   index.d.ts
│       │           │       │   index.js
│       │           │       │   index.mjs
│       │           │       │   LICENSE
│       │           │       │   NOTICE
│       │           │       │   package.json
│       │           │       │   README.md
│       │           │       │   ThirdPartyNotices.txt
│       │           │       │
│       │           │       ├───bin
│       │           │       │       install_media_pack.ps1
│       │           │       │       install_webkit_wsl.ps1
│       │           │       │       reinstall_chrome_beta_linux.sh
│       │           │       │       reinstall_chrome_beta_mac.sh
│       │           │       │       reinstall_chrome_beta_win.ps1
│       │           │       │       reinstall_chrome_stable_linux.sh
│       │           │       │       reinstall_chrome_stable_mac.sh
│       │           │       │       reinstall_chrome_stable_win.ps1
│       │           │       │       reinstall_msedge_beta_linux.sh
│       │           │       │       reinstall_msedge_beta_mac.sh
│       │           │       │       reinstall_msedge_beta_win.ps1
│       │           │       │       reinstall_msedge_dev_linux.sh
│       │           │       │       reinstall_msedge_dev_mac.sh
│       │           │       │       reinstall_msedge_dev_win.ps1
│       │           │       │       reinstall_msedge_stable_linux.sh
│       │           │       │       reinstall_msedge_stable_mac.sh
│       │           │       │       reinstall_msedge_stable_win.ps1
│       │           │       │
│       │           │       ├───lib
│       │           │       │   │   bootstrap.js
│       │           │       │   │   coreBundle.js
│       │           │       │   │   package.js
│       │           │       │   │   serverRegistry.js
│       │           │       │   │   serverRegistry.js.LICENSE
│       │           │       │   │   utilsBundle.js
│       │           │       │   │   utilsBundle.js.LICENSE
│       │           │       │   │   xdg-open
│       │           │       │   │
│       │           │       │   ├───entry
│       │           │       │   │       cliDaemon.js
│       │           │       │   │       dashboardApp.js
│       │           │       │   │       mcp.js
│       │           │       │   │       oopBrowserDownload.js
│       │           │       │   │
│       │           │       │   ├───server
│       │           │       │   │   │   deviceDescriptorsSource.json
│       │           │       │   │   │
│       │           │       │   │   ├───chromium
│       │           │       │   │   │       appIcon.png
│       │           │       │   │   │
│       │           │       │   │   └───electron
│       │           │       │   │           loader.js
│       │           │       │   │
│       │           │       │   ├───tools
│       │           │       │   │   ├───cli-client
│       │           │       │   │   │   │   channelSessions.js
│       │           │       │   │   │   │   cli.js
│       │           │       │   │   │   │   help.json
│       │           │       │   │   │   │   minimist.js
│       │           │       │   │   │   │   output.js
│       │           │       │   │   │   │   program.js
│       │           │       │   │   │   │   registry.js
│       │           │       │   │   │   │   session.js
│       │           │       │   │   │   │
│       │           │       │   │   │   └───skill
│       │           │       │   │   │       │   SKILL.md
│       │           │       │   │   │       │
│       │           │       │   │   │       └───references
│       │           │       │   │   │               element-attributes.md
│       │           │       │   │   │               playwright-tests.md
│       │           │       │   │   │               request-mocking.md
│       │           │       │   │   │               running-code.md
│       │           │       │   │   │               session-management.md
│       │           │       │   │   │               spec-driven-testing.md
│       │           │       │   │   │               storage-state.md
│       │           │       │   │   │               test-generation.md
│       │           │       │   │   │               tracing.md
│       │           │       │   │   │               video-recording.md
│       │           │       │   │   │
│       │           │       │   │   ├───dashboard
│       │           │       │   │   │       appIcon.png
│       │           │       │   │   │
│       │           │       │   │   ├───trace
│       │           │       │   │   │       SKILL.md
│       │           │       │   │   │
│       │           │       │   │   └───utils
│       │           │       │   │           extension.js
│       │           │       │   │           socketConnection.js
│       │           │       │   │
│       │           │       │   └───vite
│       │           │       │       ├───dashboard
│       │           │       │       │   │   index.html
│       │           │       │       │   │   playwright-logo.svg
│       │           │       │       │   │
│       │           │       │       │   └───assets
│       │           │       │       │           codicon-DCmgc-ay.ttf
│       │           │       │       │           firefox-1bWoP6pv.svg
│       │           │       │       │           firefox-beta-k3eOH_eK.svg
│       │           │       │       │           firefox-nightly-Cp5nfeDT.svg
│       │           │       │       │           index-BY2S1tHT.css
│       │           │       │       │           index-C_5TMfeg.js
│       │           │       │       │           safari-na3_-uQk.svg
│       │           │       │       │
│       │           │       │       ├───htmlReport
│       │           │       │       │       index.html
│       │           │       │       │       report.css
│       │           │       │       │       report.js
│       │           │       │       │
│       │           │       │       ├───recorder
│       │           │       │       │   │   index.html
│       │           │       │       │   │   playwright-logo.svg
│       │           │       │       │   │
│       │           │       │       │   └───assets
│       │           │       │       │           codeMirrorModule-DeBYQozu.js
│       │           │       │       │           codeMirrorModule-DYBRYzYX.css
│       │           │       │       │           codicon-DCmgc-ay.ttf
│       │           │       │       │           index-4ZiSSCmn.css
│       │           │       │       │           index-Bq-mQf8S.js
│       │           │       │       │
│       │           │       │       └───traceViewer
│       │           │       │           │   codeMirrorModule.DYBRYzYX.css
│       │           │       │           │   codicon.DCmgc-ay.ttf
│       │           │       │           │   defaultSettingsView.CjdS-WJx.css
│       │           │       │           │   index.CzXZzn5A.css
│       │           │       │           │   index.DMMX1gXU.js
│       │           │       │           │   index.html
│       │           │       │           │   manifest.webmanifest
│       │           │       │           │   playwright-logo.svg
│       │           │       │           │   snapshot.html
│       │           │       │           │   snapshot.v8KI4P3m.js
│       │           │       │           │   sw.bundle.js
│       │           │       │           │   uiMode.BZQ54Kgt.css
│       │           │       │           │   uiMode.html
│       │           │       │           │   uiMode.Ut8wwJNp.js
│       │           │       │           │   xtermModule.DYP7pi_n.css
│       │           │       │           │
│       │           │       │           └───assets
│       │           │       │                   codeMirrorModule-LEHpjmcn.js
│       │           │       │                   defaultSettingsView-BNmKHKpQ.js
│       │           │       │                   urlMatch-BYQrIQwR.js
│       │           │       │                   xtermModule-CsJ4vdCR.js
│       │           │       │
│       │           │       └───types
│       │           │               protocol.d.ts
│       │           │               structs.d.ts
│       │           │               types.d.ts
│       │           │
│       │           ├───cs
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───de
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───es
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───fr
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───it
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───ja
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───ko
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───Logs
│       │           │       automation-20260728.log
│       │           │       automation-20260731.log
│       │           │       automation-20260803.log
│       │           │
│       │           ├───pl
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───pt-BR
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───ru
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───runtimes
│       │           │   └───browser
│       │           │       └───lib
│       │           │           └───net8.0
│       │           │                   System.Text.Encodings.Web.dll
│       │           │
│       │           ├───tr
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           ├───zh-Hans
│       │           │       Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │           │       Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │           │       Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │           │
│       │           └───zh-Hant
│       │                   Microsoft.TestPlatform.CommunicationUtilities.resources.dll
│       │                   Microsoft.TestPlatform.CoreUtilities.resources.dll
│       │                   Microsoft.TestPlatform.CrossPlatEngine.resources.dll
│       │                   Microsoft.VisualStudio.TestPlatform.Common.resources.dll
│       │                   Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll
│       │
│       ├───Builders
│       │   └───Room
│       │           EditRoomDataBuilder.cs
│       │
│       ├───HomePageFixtures
│       │       HomePageFixture.cs
│       │
│       └───obj
│           │   project.assets.json
│           │   project.nuget.cache
│           │   RBP.Tests.Ui.csproj.nuget.dgspec.json
│           │   RBP.Tests.Ui.csproj.nuget.g.props
│           │   RBP.Tests.Ui.csproj.nuget.g.targets
│           │
│           └───Debug
│               └───net8.0
│                   │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│                   │   RBP.Test.FDB7AEB1.Up2Date
│                   │   RBP.Tests.Ui.AssemblyInfo.cs
│                   │   RBP.Tests.Ui.AssemblyInfoInputs.cache
│                   │   RBP.Tests.Ui.assets.cache
│                   │   RBP.Tests.Ui.csproj.AssemblyReference.cache
│                   │   RBP.Tests.Ui.csproj.BuildWithSkipAnalyzers
│                   │   RBP.Tests.Ui.csproj.CoreCompileInputs.cache
│                   │   RBP.Tests.Ui.csproj.FileListAbsolute.txt
│                   │   RBP.Tests.Ui.dll
│                   │   RBP.Tests.Ui.GeneratedMSBuildEditorConfig.editorconfig
│                   │   RBP.Tests.Ui.genruntimeconfig.cache
│                   │   RBP.Tests.Ui.GlobalUsings.g.cs
│                   │   RBP.Tests.Ui.pdb
│                   │   RBP.Tests.Ui.sourcelink.json
│                   │   ReportPortal.addins
│                   │
│                   ├───ref
│                   │       RBP.Tests.Ui.dll
│                   │
│                   └───refint
│                           RBP.Tests.Ui.dll
│
└───UI
    └───obj
            project.assets.json
            project.nuget.cache
            RBP.Tests.Ui.csproj.nuget.dgspec.json
            RBP.Tests.Ui.csproj.nuget.g.props
            RBP.Tests.Ui.csproj.nuget.g.targets

# DESIGN PATTERNS

This framework uses:

- Page Object
- Component Object
- Builder
- DTO
- Factory
- Fluent API
- Strategy (future)
- Composition over inheritance

Page Object represents an entire page.

Component represents a reusable part of a page.

Builder creates test data.

# Design Principles
SOLID

DRY

KISS

YAGNI

Composition over inheritance

Explicit is better than implicit

Small reusable abstractions

Single responsibility per class

High cohesion

Low coupling

# FLUENT API

Methods that modify UI state should return

this

Methods that navigate should return another Page Object.

Correct

page.OpenLogin()

returns

LoginPage

Correct

editor
    .SetDescription(...)
    .SetPrice(...)
    .SaveAsync();

Wrong

editor.SetDescription();

editor.SetPrice();

editor.SetImage();

editor.Save();

Page Objects contain ONLY

- locators
- navigation
- atomic actions

Page Objects NEVER contain

- assertions
- test logic
- business logic
- waits unrelated to UI stability

# Component Rules
Components represent reusable UI fragments.

Examples

BookingFormComponent

RoomCardComponent

EditRoomComponent

Components

MAY

contain fluent methods.

Components

MUST NOT

navigate between pages.

# Assertion Rules
Assertions belong ONLY inside Assertions project.

Never use

Assert.AreEqual

Assert.True

Assert.False

inside tests.

Create extension methods instead.

Example: 
    Correct : 
    
        room.ShouldMatch(expected);
        
        bookings.ShouldContainBooking(request);
    
    Wrong :
    
        Assert.AreEqual(...)


# Builder Rules

Builders must provide

- sensible defaults

- fluent methods

- immutable DTO output

Builders should not expose constructors of DTOs.

# DTO Rules
DTO must remain immutable.

Prefer

init

properties.

Avoid setters.

# Logging Rules
Every user action must be logged.

Examples

Opening page

Clicking button

Submitting form

Calling API

Receiving response

Creating browser

Closing browser

# Wait Rules
Never use

Thread.Sleep

WaitForTimeout

Prefer

WaitForURL

Expect()

Locator.WaitForAsync()

WaitForLoadState()

WaitForResponse()

# Locator Rules
Preferred locator priority

1. getByTestId()

2. getByRole()

3. getByLabel()

4. getByPlaceholder()

5. css

6. xpath

Never use XPath if another locator is possible.

# Async Rules
Everything must be async.

Never block async code.

Never use

.Result

.Wait()

Task.Run()

# Test Rules
A test should:

- read like documentation

- have Arrange Act Assert

- be under 15 lines whenever possible

- use builders

- use DTO

- use assertions

- avoid primitive values

- avoid duplication

Example

await homePage
    .OpenRoomAsync(3)
    .ReserveNowAsync();

await bookingForm
    .FillAsync(request)
    .SubmitAsync();

await roomDetailsPage
    .ShouldHaveSuccessfulBooking();

Never compare primitive values inside tests.

Always compare DTOs.

Correct

actual.ShouldMatch(expected);

Wrong

Assert.AreEqual(expected.Price, actual.Price);
Assert.AreEqual(expected.Description, actual.Description);
Assert.AreEqual(...)

# Browser Rules
Browser configuration must always come from

environment

or

configuration.

Tests must never choose browser.

# ReportPortal Rules
On failure attach

- screenshot

- logs

- exception

Never save temporary files from tests.

# Code Style
Prefer

file-scoped namespaces

collection expressions

primary constructors when appropriate

target-typed new

expression-bodied members

readonly fields

# AI Forbidden Actions

NEVER

- rewrite architecture

- duplicate classes

- rename public APIs without request

- change project structure

- introduce new libraries

- add static state

- create Helpers if Component already exists

- create new DTO if similar DTO exists

- create another Builder for same DTO

## AI FLAGS

Architecture Stability: HIGH

Backward Compatibility: REQUIRED

Refactoring Aggressiveness: LOW

Duplicate Tolerance: ZERO

Code Quality: PRODUCTION

Test Readability: HIGH

Performance Priority: MEDIUM

Maintainability Priority: MAXIMUM

Framework Consistency: STRICT

Breaking Changes: FORBIDDEN


# When creating UI tests:

DO:

Reuse existing Page Objects

Reuse Components

Reuse Builders

Reuse DTOs

Reuse Assertions

Add logging

Keep test under 10 lines

Use async/await everywhere

Return Page Objects from navigation

Return Components from page sections

Use records / DTOs instead of primitive arguments


DON'T:

Don't use raw locators in tests

Don't create duplicate Page Objects

Don't assert inside Page Objects

Don't use Thread.Sleep

Don't use WaitForTimeout

Don't create test data inline

Don't use Assert.AreEqual

Don't use magic strings

Don't instantiate Playwright directly

Don't duplicate API clients


# Page Objects must contain ONLY:

- locators

- atomic UI actions

- navigation

No assertions.

No business logic.

No orchestration.

# How to implement a new Jira Test Case
1. Read Jira.

2. Determine layer.

API

UI

E2E

3. Search existing:

Page

Component

Builder

DTO

Assertion

Client

4. Reuse.

5. Create only missing pieces.

6. Implement test.

7. Run test.

8. Fix.

9. Commit.

# Error Handling
Do not swallow exceptions.

Do not retry silently.

Every failure should explain

what

where

why

# AI Workflow
Understand

↓

Search

↓

Reuse

↓

Generate

↓

Compile

↓

Run

↓

Fix

↓

Repeat

↓

Stop only when green

# Definition of Done
Task is complete only if

architecture preserved

code compiles

tests pass

no duplication

logging exists

builders reused

assertions reused

pages remain atomic

components reusable

# Test Naming

## Test class naming

Every test fixture must end with `Fixture`.

Examples:

- RoomBookingFixture
- EditRoomFixture
- RoomListDataMappingFixture

Do not use:

- Tests
- Test
- Spec

---

## Test method naming

Always use descriptive names with underscores.

Format:

<Action>_Should_<ExpectedResult>

Examples:

User_Should_Be_Able_To_Book_Room

Room_List_Should_Match_Api_Data

Edit_Room_Should_Update_Public_Room_Data

User_Should_Not_Be_Able_To_Book_Reserved_Room

Booking_Should_Be_Visible_In_Admin_Panel

Avoid:

ShouldBookRoom()

BookRoom()

Test1()

BookingTest()

---

## NUnit metadata

Every test must contain:

```csharp
[Test]
[Category("Smoke")]        // or Regression
[Category("UI")]           // UI / API / E2E
[Property("JiraKey", "RBP-45")]
```

Category is used for filtering.

Property is used only for traceability.

Never use Property instead of Category.

---

## Test structure

Every test follows Arrange / Act / Assert.

Example:

```csharp
RoomCardDto expectedRoom =
    new EditRoomDataBuilder()
        .WithDescription("Updated")
        .Build();

AdminRoomsPage adminRooms =
    await LoginAsAdminAsync();

RoomManagementSteps roomSteps =
    new(adminRooms);

await roomSteps.EditRoomAsync(
    roomId: 1,
    room: expectedRoom);

RoomCardDto actualRoom =
    await homePage.GetRoomAsync(1);

actualRoom.ShouldMatch(expectedRoom);
```

Tests should read like documentation.

# Steps

## Purpose

Steps encapsulate business workflows.

They orchestrate multiple UI/API actions into one meaningful operation.

Examples:

- BookRoom
- EditRoom
- LoginAsAdmin
- CreateBooking
- CancelBooking

---

## Responsibilities

Steps MAY:

- orchestrate multiple Pages and Components
- perform business workflows
- return the resulting Page Object
- log workflow execution

Steps MUST NOT:

- contain assertions
- compare values
- verify business results
- create test data
- know expected values

Assertions always stay inside test methods.

---

## When to create a Step

Create a Step when:

- the workflow requires 3+ actions
- the workflow is reused
- the workflow represents one business action

Example:

Book room

↓

Fill dates

↓

Open room

↓

Reserve room

↓

Fill booking form

↓

Submit booking

↓

Return RoomDetailsPage

---

Do NOT create a Step for:

- reading UI data
- opening one page
- calling one method
- making assertions

Wrong:

OpenHomePageStep

ClickSaveButtonStep

GetRoomsStep

Correct:

BookingSteps

RoomManagementSteps

AuthenticationSteps

---

## Step design

A Step should expose one business action.

Correct:

```csharp
RoomDetailsPage room =
    await bookingSteps.BookRoomAsync(request);
```

Correct:

```csharp
await roomManagementSteps.EditRoomAsync(
    roomId,
    room);
```

Wrong:

```csharp
await bookingSteps.ClickReserveButton();

await bookingSteps.FillGuestName();

await bookingSteps.ClickSubmit();
```

Those belong to Page Objects or Components.

---

## Dependencies

Steps may depend on:

- Page Objects
- Components
- DTOs
- Builders

Steps must never depend on:

- Assertions
- NUnit
- TestContext
- ReportPortal

Steps belong to the RPB.Business layer.

Tests orchestrate Steps and perform assertions.