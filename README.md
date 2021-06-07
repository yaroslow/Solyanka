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
  | Solyanka.Cqrs                                                         | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Core.Solyanka.Cqrs.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Core.Solyanka.Cqrs.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Cqrs.Abstractions)](https://www.nuget.org/packages/Solyanka.Cqs.Abstractions) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Cqrs.Abstractions)](https://www.nuget.org/packages/Solyanka.Cqs.Abstractions) |
  | Solyanka.Ddd                                                          | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Core.Solyanka.Ddd.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Core.Solyanka.Ddd.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Ddd)](https://www.nuget.org/packages/Solyanka.Ddd) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Ddd)](https://www.nuget.org/packages/Solyanka.Ddd) |
  | Solyanka.Expressions                                                  | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Core.Solyanka.Expressions.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Core.Solyanka.Expressions.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Expressions)](https://www.nuget.org/packages/Solyanka.Expressions) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Expressions)](https://www.nuget.org/packages/Solyanka.Expressions) |
  | Solyanka.Utils                                                        | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Core.Solyanka.Utils.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Core.Solyanka.Utils.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Utils)](https://www.nuget.org/packages/Solyanka.Utils) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Utils)](https://www.nuget.org/packages/Solyanka.Utils) |
  |                               `Projects`                              |  |
  | Solyanka.Dispatcher                                                   | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Solyanka.Dispatcher.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Solyanka.Dispatcher.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Dispatcher)](https://www.nuget.org/packages/Solyanka.Dispatcher) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Dispatcher)](https://www.nuget.org/packages/Solyanka.Dispatcher) |
  | Solyanka.ServiceBus                                                   | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Solyanka.ServiceBus.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Solyanka.ServiceBus.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.ServiceBus)](https://www.nuget.org/packages/Solyanka.ServiceBus) [![NuGet](https://img.shields.io/nuget/v/Solyanka.ServiceBus)](https://www.nuget.org/packages/Solyanka.ServiceBus) |
  | Solyanka.ServiceBus.Abstractions                                      | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Solyanka.ServiceBus.Abstractions.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Solyanka.ServiceBus.Abstractions.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.ServiceBus.Abstractions)](https://www.nuget.org/packages/Solyanka.ServiceBus.Abstractions) [![NuGet](https://img.shields.io/nuget/v/Solyanka.ServiceBus.Abstractions)](https://www.nuget.org/packages/Solyanka.ServiceBus.Abstractions) |
  | Solyanka.Validator                                                    | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Solyanka.Validator.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Solyanka.Validator.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Validator)](https://www.nuget.org/packages/Solyanka.Validator) [![NuGet](https://img.shields.io/nuget/v/Solyanka.ServiceBus.Abstractions)](https://www.nuget.org/packages/Solyanka.ServiceBus.Abstractions) |
  |                           `Projects.Microsoft`                        |  |
  | Solyanka.Dispatcher.Microsoft.DependencyInjection                     | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Microsoft.Solyanka.Dispatcher.Microsoft.DependencyInjection.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Microsoft.Solyanka.Dispatcher.Microsoft.DependencyInjection.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.Dispatcher.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.Dispatcher.Microsoft.DependencyInjection) [![NuGet](https://img.shields.io/nuget/v/Solyanka.Dispatcher.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.Dispatcher.Microsoft.DependencyInjection) |
  | Solyanka.ServiceBus.Microsoft.DependencyInjection                     | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Microsoft.Solyanka.ServiceBus.Microsoft.DependencyInjection.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Microsoft.Solyanka.ServiceBus.Microsoft.DependencyInjection.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.ServiceBus.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.ServiceBus.Microsoft.DependencyInjection) [![NuGet](https://img.shields.io/nuget/v/Solyanka.ServiceBus.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.ServiceBus.Microsoft.DependencyInjection) |
  | Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection            | [![Build status](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Microsoft.Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection.yml/badge.svg)](https://github.com/yaroslow/Solyanka/actions/workflows/Projects.Microsoft.Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection.yml) [![NuGet](https://img.shields.io/nuget/dt/Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection) [![NuGet](https://img.shields.io/nuget/v/Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection) |

### In mind
- Update documentation
- Validation with attributes
- Logging
- Errors handling
