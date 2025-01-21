package main

import (
	"context"
	"fmt"
	"log"

	"github.com/philippseith/signalr"
)

type ChatClient struct{}

func (c *ChatClient) ReceiveMessage(user, message string) {
	fmt.Printf("%s: %s\n", user, message)
}

func main() {
	ctx := context.Background()

	conn, err := signalr.NewHTTPConnection(ctx, "https://localhost:7046/chat")
	if err != nil {
		log.Fatalf("Ошибка создания подключения: %v", err)
	}

	client, err := signalr.NewClient(
		ctx,
		signalr.WithConnection(conn),
		signalr.WithReceiver(&ChatClient{}))

	if err != nil {
		log.Fatalf("Ошибка создания клиента: %v", err)
	}

	client.Start()
	if err != nil {
		log.Fatalf("Ошибка подключения: %v", err)
	}
	fmt.Println("Подключено к серверу SignalR")

	select {}
}
