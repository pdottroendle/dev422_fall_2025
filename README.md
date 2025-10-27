HTTPS microservices client server seem to be up and running adding a book went allright - could not retrieve or list

Possible Issues


In-Memory Storage Reset
If your BookInventoryService stores books in memory (e.g., a static list or dictionary), restarting the service or running the client separately might cause the data to be lost.


gRPC Endpoint Mismatch
The client might be calling a different endpoint or port than the one the server is listening on (https://localhost:7068 and http://localhost:5112).


Book ID Handling
There might be a mismatch in how IDs are stored or retrieved—e.g., string vs. int, or case sensitivity.


Serialization Issues
If the gRPC contract (proto file) has mismatched field names or types between client and server, the data might not be transmitted correctly.
