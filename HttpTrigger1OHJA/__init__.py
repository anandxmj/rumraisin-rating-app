import logging

import azure.functions as func


def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    productID = req.params.get('productID')
    if not productID:
        try:
            req_body = req.get_json()
        except ValueError:
            pass
        else:
            productID = req_body.get('productID')

    if productID:
        return func.HttpResponse(f"The product name for your product id {productID} is Starfruit Explosion")
    else:
        return func.HttpResponse(
             "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.",
             status_code=200
        )
