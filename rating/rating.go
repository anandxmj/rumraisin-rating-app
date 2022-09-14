package rating

import (
	"rumraisin-rating-app/ratingsdb"
	"time"

	"github.com/beevik/guid"
)

type CreateRatingRequest struct {
	UserId       string `json:"userId"`
	ProductId    string `json:"productId"`
	LocationName string `json:"locationName"`
	Rating       int    `json:"rating"`
	UserNotes    string `json:"userNotes"`
}

type CreateRatingResponse struct {
	Id           string `json:"id"`
	TimeStamp    string `json:"timestamp"`
	UserId       string `json:"userId"`
	ProductId    string `json:"productId"`
	LocationName string `json:"locationName"`
	Rating       int    `json:"rating"`
	UserNotes    string `json:"userNotes"`
}

func Create(crr *CreateRatingRequest) *CreateRatingResponse {
	guid := guid.New().String()
	timestamp := time.UTC.String()
	response := &CreateRatingResponse{
		guid,
		timestamp,
		crr.UserId,
		crr.ProductId,
		crr.LocationName,
		crr.Rating,
		crr.UserNotes,
	}

	rating := &ratingsdb.Rating{
		Id:           response.Id,
		TimeStamp:    response.TimeStamp,
		UserId:       response.UserId,
		ProductId:    response.ProductId,
		LocationName: response.LocationName,
		Rating:       response.Rating,
		UserNotes:    response.UserNotes,
	}

	ratingsdb.Create(rating)
	return response
}
