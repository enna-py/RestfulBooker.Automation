# RestfulBooker Automation Framework

Automation framework for API and UI testing using .NET 8.

## Technology Stack

- .NET 8
- NUnit
- RestSharp
- Playwright
- Serilog

## Architecture

```
RBP.TAF.sln 

├── src/ 
│   ├── RBP.Core/         
│   ├── RBP.Business.Api/       
│   ├── RBP.Business.Ui/        
│   ├── RBP.Data/             
├── tests/ 
│   ├── RBP.Tests.Api/    
│   ├── RBP.Tests.Ui/              
│   └── RBP.Tests.E2E/          
└── config/ 
    ├── appsettings.json 
    ├── appsettings.Local.json 
    └── appsettings.CI.json
```

## Current Status

- Configuration   - Done
- Logging		  - Done
- Authentication  - Done
- API Clients	  - Done
- Request Factory - Done
- Base Fixtures	  - Done

## In Progress

- Smoke API Tests
- Builders
- Extensions
- UI Framework
