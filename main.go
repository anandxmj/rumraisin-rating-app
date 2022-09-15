package main

import (
	"fmt"
	"log"
	"net/http"
	"os"
	"rumraisin-rating-app/rating"
	"rumraisin-rating-app/ratingsdb"

	"github.com/gin-gonic/gin"
)

var USER_BY_ID_URL string = "https://serverlessohapi.azurewebsites.net/api/GetUser?userId=%s"
var PRODUCT_BY_ID_URL string = "https://serverlessohapi.azurewebsites.net/api/GetProduct?productId=%s"
var USER = 0
var PRODUCT = 1

func main() {
	if err := ratingsdb.New(); err != nil {
		log.Fatal("Can not connect to DB", err)
		return
	}

	port := ":8080"
	if portParam, ok := os.LookupEnv("FUNCTIONS_CUSTOMHANDLER_PORT"); ok {
		port = ":" + portParam
	}
	r := gin.Default()
	r.POST("/api/rating", HandleCreateRating)
	r.Run(port)
}

func HandleCreateRating(c *gin.Context) {
	var request *rating.CreateRatingRequest
	if err := c.BindJSON(&request); err != nil {
		c.JSON(http.StatusBadRequest, err.Error())
	} else {
		if !Exists(USER, request.UserId) {
			c.String(http.StatusBadRequest, "User does not exist "+request.UserId)
		} else if !Exists(PRODUCT, request.ProductId) {
			c.String(http.StatusBadRequest, "Product does not exist "+request.ProductId)
		} else {
			c.JSON(http.StatusOK, rating.Create(request))
		}
	}
}

func Exists(vtype int, value string) bool {
	url := ""
	if vtype == USER {
		url = fmt.Sprintf(USER_BY_ID_URL, value)
	} else if vtype == PRODUCT {
		url = fmt.Sprintf(PRODUCT_BY_ID_URL, value)
	}
	resp, err := http.Get(url)
	return err == nil && resp.StatusCode == 200
}
