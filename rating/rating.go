package rating

import (
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
	response := &CreateRatingResponse{
		guid.New().String(),
		time.UTC.String(),
		crr.UserId,
		crr.ProductId,
		crr.LocationName,
		crr.Rating,
		crr.UserNotes,
	}
	return response
}
