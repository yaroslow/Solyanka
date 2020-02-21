Solyanka
===

This project was started owing to (big thanks to):
  - @[jbogard](https://github.com/jbogard) and his ©[MediatR](https://github.com/jbogard/MediatR)
  - @[marshinov](https://habr.com/ru/users/marshinov/) and his [report](https://habr.com/ru/company/jugru/blog/447308/)

### Description:
Solyanka is a simple CQS/CQRS framework, supporting:
  - commands, queries, handlers and pipelines;
  - events (domain, application, integration) handling;
  - domain structures (DDD);
  - sending integration events over service bus

### Documentation:
  - [Handling queries, commands and events](https://github.com/yaroslow/Solyanka/blob/master/docs/1.%20Handling%20queries%2C%20commands%20and%20events.md)
  - [Building queries and commands pipelines](https://github.com/yaroslow/Solyanka/blob/master/docs/2.%20Building%20queries%20and%20commands%20pipelines.md)
  - [Using IEventContainer to handle events](https://github.com/yaroslow/Solyanka/blob/master/docs/3.%20Using%20IEventContainer%20to%20handle%20events.md)
  - [Using IServiceBus to push events to another app](https://github.com/yaroslow/Solyanka/blob/master/docs/4.%20Using%20IServiceBus%20to%20push%20events%20to%20another%20app.md)

### Packages:
  Core
    - Solyanka.Cqs.Abstractions
    - Solyanka.Events.Abstractions
    - Solyanka.Utils
    - Projects
  Dispatcher
    - Solyanka.Dispatcher
    - Solyanka.Dispatcher.EventsPushing
  ServiceBus
    - Solyanka.ServiceBus.Abstractions
    - Solyanka.ServiceBus
  
  Projects.Microsoft - using Microsoft DI and other technologies:
  Dispatcher
    - Solyanka.Dispatcher.Microsoft.DependencyInjection
  ServiceBus
    - Solyanka.ServiceBus.Microsoft.DependencyInjection
    - Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection

### Samples
 will be someday

### In mind
- Validation
- Logging
- Errors handling
