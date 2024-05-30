# SFinance

# SFinanceAPI
SFinanceAPI is a .NET 8.0 web API that uses OpenAI's GPT-4o model to process receipts. The API receives an image of a receipt, sends it to OpenAI for processing, and then stores the processed data in a database.

## Features
- Receipt Processing: The API can process an image of a receipt and extract information such as the store name, store address, date, cashier, items, total amount, payment method, and receipt number. Then, it stores it in the destined folder, and sends info about it to the database.
- Database Storage: The processed data is stored in a database for future reference.
- OpenAI Integration: The API uses OpenAI's GPT-4o model to process the receipt images.
  
## How It Works
The main service in the API is the OpenAiService. This service has a ProcessReceiptAsync method that takes an image file as input, converts it to a base64 string, and sends it to the OpenAI API. The OpenAI API processes the image and returns a JSON object with the extracted data. The ProcessReceiptAsync method then deserializes this JSON object into a Receipt object and stores it in the database.

## Setup
To set up the API, you need to:
1.	Clone the repository.
2.	Set up a database and update the connection string in the appsettings.json file.
3.	Get an API key from OpenAI and add it to the appsettings.json file.
4.	Run the API.

## Usage
To use the API, you can send a POST request to the /api/receipts endpoint with an image file in the request body. The API will process the image and return a Receipt object in the response.

## Future Improvements
- Error Handling: Improve error handling to provide more detailed error messages.
- Testing: Add unit tests and integration tests to ensure the API works as expected.
- Performance: Optimize the performance of the API, especially the image processing part.
- Security: Add authentication and authorization to secure the API.
- Online banking: Planned in the future to add more info about our spending and synchronizing with our receipts.
