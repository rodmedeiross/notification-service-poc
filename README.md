
# Notification

A POC using WebSockets and RabbitMQ to send client notifications



## API

#### Send Item

```http
  POST /api/send
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `message` | `json` | **Required**. Example Message `{ "name": "...", "status": "..."}`|

#### Get item (WebSocket)

```http
  WS /api/notification
```



## Usage

```bash
  dotnet restore
  dotnet run
```
    
## Dependencies

- Serilog
- SignalR
- MassTransit

