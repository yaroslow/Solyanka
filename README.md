Solyanka
===

This project was started owing to (big thanks to):
  - @[jbogard](https://github.com/jbogard) and his ©[MediatR](https://github.com/jbogard/MediatR)
  - @[marshinov](https://habr.com/ru/users/marshinov/) and his [report](https://habr.com/ru/company/jugru/blog/447308/)

### Description:
Solyanka is a simple CQS/CQRS framework, supporting:
  - commands, queries, handlers and pipelines;
  - events (domain, application, integration) handling;
  - sending integration events over service bus

### Documentation:
  - [Handling queries, commands and events](https://github.com/yaroslow/Solyanka/blob/master/docs/1.%20Handling%20queries%2C%20commands%20and%20events.md)
  - [Building queries and commands pipelines](https://github.com/yaroslow/Solyanka/blob/master/docs/2.%20Building%20queries%20and%20commands%20pipelines.md)
  - [Using IEventContainer to handle events](https://github.com/yaroslow/Solyanka/blob/master/docs/3.%20Using%20IEventContainer%20to%20handle%20events.md)
  - [Using IServiceBus to push events to another app](https://github.com/yaroslow/Solyanka/blob/master/docs/4.%20Using%20IServiceBus%20to%20push%20events%20to%20another%20app.md)

### Packages:

  |                             Package name                              | Pipelines |
  | --------------------------------------------------------------------- | --------- |
  |                                 `Core`                                |  |
  | Solyanka.Cqs.Abstractions                                             | [![Build status](https://dev.azure.com/yaroslow/Solyanka/_apis/build/status/Core/Solyanka.Cqs.Abstractions)](https://dev.azure.com/yaroslow/Solyanka/_build/latest?definitionId=4) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Cqs.Abstractions)](https://www.nuget.org/packages/Solyanka.Cqs.Abstractions) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Cqs.Abstractions)](https://www.nuget.org/packages/Solyanka.Cqs.Abstractions) |
  | Solyanka.Events.Abstractions                                          | [![Build status](https://dev.azure.com/yaroslow/Solyanka/_apis/build/status/Core/Solyanka.Events.Abstractions)](https://dev.azure.com/yaroslow/Solyanka/_build/latest?definitionId=5) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Events.Abstractions)](https://www.nuget.org/packages/Solyanka.Events.Abstractions) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Events.Abstractions)](https://www.nuget.org/packages/Solyanka.Events.Abstractions) |
  | Solyanka.Utils                                                        | [![Build status](https://dev.azure.com/yaroslow/Solyanka/_apis/build/status/Core/Solyanka.Utils)](https://dev.azure.com/yaroslow/Solyanka/_build/latest?definitionId=3) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Utils)](https://www.nuget.org/packages/Solyanka.Utils) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Utils)](https://www.nuget.org/packages/Solyanka.Utils) |
  |                               `Projects`                              |  |
  | Solyanka.Dispatcher                                                   | [![Build status](https://dev.azure.com/yaroslow/Solyanka/_apis/build/status/Projects/Dispatcher/Solyanka.Dispatcher)](https://dev.azure.com/yaroslow/Solyanka/_build/latest?definitionId=6) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Dispatcher)](https://www.nuget.org/packages/Solyanka.Dispatcher) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Dispatcher)](https://www.nuget.org/packages/Solyanka.Dispatcher) |
  | Solyanka.Dispatcher.EventsPushing                                     | [![Build status](https://dev.azure.com/yaroslow/Solyanka/_apis/build/status/Projects/Dispatcher/Solyanka.Dispatcher.EventsPushing)](https://dev.azure.com/yaroslow/Solyanka/_build/latest?definitionId=7) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Dispatcher.EventsPushing)](https://www.nuget.org/packages/Solyanka.Dispatcher.EventsPushing) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Dispatcher.EventsPushing)](https://www.nuget.org/packages/Solyanka.Dispatcher.EventsPushing) |
  | Solyanka.ServiceBus.Abstractions                                      | [![Build status](https://dev.azure.com/yaroslow/Solyanka/_apis/build/status/Projects/ServiceBus/Solyanka.ServiceBus.Abstractions)](https://dev.azure.com/yaroslow/Solyanka/_build/latest?definitionId=8) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.ServiceBus.Abstractions)](https://www.nuget.org/packages/Solyanka.ServiceBus.Abstractions) [![NuGet](https://img.shields.io/nuget/v/Solyanka.ServiceBus.Abstractions)](https://www.nuget.org/packages/Solyanka.ServiceBus.Abstractions) |
  | Solyanka.ServiceBus                                                   | [![Build status](https://dev.azure.com/yaroslow/Solyanka/_apis/build/status/Projects/ServiceBus/Solyanka.ServiceBus)](https://dev.azure.com/yaroslow/Solyanka/_build/latest?definitionId=9) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.ServiceBus)](https://www.nuget.org/packages/Solyanka.ServiceBus) [![NuGet](https://img.shields.io/nuget/v/Solyanka.ServiceBus)](https://www.nuget.org/packages/Solyanka.ServiceBus) |
  |                           `Projects.Microsoft`                        |  |
  | Solyanka.Dispatcher.Microsoft.DependencyInjection                     | [![Build status](https://dev.azure.com/yaroslow/Solyanka/_apis/build/status/Projects.Microsoft/Dispatcher/Solyanka.Dispatcher.Microsoft.DependencyInjection)](https://dev.azure.com/yaroslow/Solyanka/_build/latest?definitionId=10) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Dispatcher.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.Dispatcher.Microsoft.DependencyInjection) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Dispatcher.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.Dispatcher.Microsoft.DependencyInjection) |
  | Solyanka.ServiceBus.Microsoft.DependencyInjection                     | [![Build status](https://dev.azure.com/yaroslow/Solyanka/_apis/build/status/Projects.Microsoft/ServiceBus/Solyanka.ServiceBus.Microsoft.DependencyInjection)](https://dev.azure.com/yaroslow/Solyanka/_build/latest?definitionId=11) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.ServiceBus.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.ServiceBus.Microsoft.DependencyInjection) [![NuGet](https://img.shields.io/nuget/v/Solyanka.ServiceBus.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.ServiceBus.Microsoft.DependencyInjection) |
  | Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection            | [![Build status](https://dev.azure.com/yaroslow/Solyanka/_apis/build/status/Projects.Microsoft/ServiceBus/Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection)](https://dev.azure.com/yaroslow/Solyanka/_build/latest?definitionId=12) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection) [![NuGet](https://img.shields.io/nuget/v/Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection) |

### In mind
- Validation
- Logging
- Errors handling