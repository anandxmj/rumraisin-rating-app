package ratingsdb

import (
	"context"
	"encoding/json"
	"errors"
	"log"
	"os"

	"github.com/Azure/azure-sdk-for-go/sdk/data/aztables"
)

type Rating struct {
	Id           string `json:"id"`
	TimeStamp    string `json:"timestamp"`
	UserId       string `json:"userId"`
	ProductId    string `json:"productId"`
	LocationName string `json:"locationName"`
	Rating       int    `json:"rating"`
	UserNotes    string `json:"userNotes"`
}

var dbClient *aztables.Client

func New() error {
	if ratingsDBConnectionString, ok := os.LookupEnv("ratingsdb"); !ok {
		return errors.New("DB Conection not defined.")
	} else {
		svcClient, err := aztables.NewServiceClientFromConnectionString(ratingsDBConnectionString, nil)
		if err != nil {
			panic(err)
		}
		dbClient = svcClient.NewClient("ratings")
		if err != nil {
			panic(err)
		}
		return nil
	}
}

func Create(args *Rating) {
	rating := aztables.EDMEntity{
		Entity: aztables.Entity{
			PartitionKey: args.UserId,
			RowKey:       args.Id,
		},
		Properties: map[string]interface{}{
			"rating":       args.Rating,
			"locationName": args.LocationName,
			"productId":    args.ProductId,
			"userId":       args.UserId,
			"userNotes":    args.UserNotes,
			"timestamp":    args.TimeStamp,
			"id":           args.Id,
		},
	}
	b, err := json.Marshal(rating)
	if err != nil {
		log.Println("Marshal error ", err)
	}
	resp, err := dbClient.AddEntity(context.Background(), b, nil)
	if err != nil {
		log.Println("Call to AddEntity Error ", err)
	}
	log.Println("Done ", resp)
}
