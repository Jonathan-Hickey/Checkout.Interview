Going to outline my ideas and development process here.
It won't be an essay but more notes and ideas.
This is all subject to change as I develop the applicaiton.

Understanding of specifaction 
 - I will need an endpoint that a merchant can call to process a payment
    - Storing card information
        - PCI
        - Logging? 
    - This will call the Acquiring Bank
    
    - Request Model 
        - card number
        - expiry month/date
        - amount
        - currency
        - cvv
        - Billing Address (bonus)?
        - First name Last Name? 
        - Which MerchantId?
    - Response 
        - Payment unique identifier (GUID?)
        - ~PaymentStatus (successful/unsuccessful (Reason for failing?))~
        - Payment Status Codes

 - A merchant should be able to retrieve the details of a previously made payment
    - Request Model
        - Merchant Id
        - Payment unique identifier
    - Response Model 
        - mask the card number
        - Payment unique identifier
        - Merchant Id
        - reconciliation and reporting means we will need 
            - amount 
            - currency 
        - Payment status code

---------------------------------------------------------------------------------
Implementation ideas + design 

From reading the "Extra mile bonus points" section I will be addressing the following with the implementation
    - Application logging (Console logging)
    - Authentication (in-memory store set up using identity server 4)
    - API client (will allow you to easly call API)
    - Encryption (PAN will be be one way hashed)
    - Data storage (In memory)
    ~- Swagger for easy testing~ 
        -I have set up intergation tests so might not need this. Down side will be no auto generation of documentation. Maybe revisit once everything else is done

I will also hard code some values in a class called SecretKeyService which in real life would be calling a key 
store so that we never be storing any sensitive passwords or other data on disk.   

Since I am planning on adding a API client I will be Request and Response models thier own class library targeting .net standard.
This will allow us to compile to .net framework or .net core on a build server

We probably dont want to return the Acquiring Banks Payment unique identifier to the merchant.
    - This could cause issues in the feature when we are integrating with more Acquiring Banks
    - I think we store the response and then create our own
    - This way the merchant is locked into our system 
    - If we change acquiring banks the merchant wont be affected allowing us to market the product better

---------------------------------------------------------------------------------
Flows

Create Payment Request Flow
    Request -> Valid data -> Storing Data -> Call Acquiring Bank Service ->  Map Request to Acquiring Bank Model -> Acquiring Bank Client Send Request
Create Payment Response Flow
    Acquiring Bank Client Send Response ->  Map Response Acquiring Bank Model -> Store unique identifier and generate a new merchant payment unique identifier -> Map to response 

Get Merchant Payment Information
    Request -> Check data store based on Merchat payment unique identifier and merchant Id - > return repsone

---------------------------------------------------------------------------------
Lets talk security

- Merchant Ip address should be whitelisted 
- Merchant should have access tokens that expire after X amount of time
- I will only be implementing an authentication that ckecking if a value is in a list of valids

---------------------------------------------------------------------------------
Tickets and Task (Breaking down the project)

    - Reading and Understanding of Spec (Done)
    - Set up a blank api (Hello world style), include unit tests (Assert.Pass()  style) (Done)
    - build and deployment (I'm not doing this step, but this is the time I would getting the CI/CD working to test and production enviroments. Along with verifying unit tests are running)
    - Basic logging (Done)
    - Adding swagger
    - Adding Auth (Done)
        - Change auth not to use JWT Tokens, instead use reference tokens (Done)
    - Added Integration Project
        -  These tests will show the merchant interaction 
    - Adding Payment Endpoint
        - 
    - Adding Getting Payment Details Endpoint
    - 

---------------------------------------------------------------------------------
Notes
1. Shopper: Individual who is buying the product online.
2. Merchant: The seller of the product. For example, Apple or Amazon.
3. Payment Gateway: Responsible for validating requests, storing card information and forwarding
payment requests and accepting payment responses to and from the acquiring bank.
4. Acquiring Bank: Allows us to do the actual retrieval of money from the shopper’s card and payout to the
merchant. It also performs some validation of the card information and then sends the payment details
to the appropriate 3rd party organization for processing.

PCI Storing of Data
    https://www.pcisecuritystandards.org/documents/PCI%20Data%20Storage%20Dos%20and%20Donts.pdf?agreement=true&time=1589649484634

One way Hashing
    https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.hashalgorithm?view=netcore-3.1
    https://stackoverflow.com/questions/2117732/reasons-why-sha512-is-superior-to-md5
        https://stackoverflow.com/a/2117808


Interesting blog about logging
https://blog.rsuter.com/logging-with-ilogger-recommendations-and-best-practices/

IdentityServer4 QuickStart 
https://identityserver4.readthedocs.io/en/latest/quickstarts/1_client_credentials.html

I might be using the wrong type of tokens 
http://docs.identityserver.io/en/release/topics/reference_tokens.html
https://leastprivilege.com/2015/11/25/reference-tokens-and-introspection/


Maybe look into  this later
https://saijogeorge.com/dummy-credit-card-generator/
which leads me to https://www.geeksforgeeks.org/luhn-algorithm/
https://money.howstuffworks.com/personal-finance/debt-management/credit-card1.htm


hash-and-salt-passwords-in-c-sharp
https://stackoverflow.com/questions/2138429/hash-and-salt-passwords-in-c-sharp
Idea being that each hash should have its own salt... but then we would end up storing the same card number over and over again... good or bad?

