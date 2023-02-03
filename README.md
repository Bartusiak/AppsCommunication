# AppsCommunication

In the project, we have 2 applications:

 * `App A` which send an encrypted message to the database and key to the App B to decrypt a message.
 * `App B` creates database using method code first, get message from the database, decrypt a messsage and send status with decrypted message to App A.

### Task List

- [x] Application A logic
- [x] Application B logic
- [x] Unit tests
- [x] Some documentations (Swagger UI)
- [ ] Fix issue with N calls to AppB

      EDIT: After circa 86 App A, I had problem with get a good lastmessage from the database.
            In the exercise, I didn't have information about how should I get the message from the database via App B, so I uderstand, I need get a last message and                 remove it because will be unnecessary (App A is sending direct to App B key and symetric alghoritm where was used direct injection).
- [ ] Docker 

      EDIT: I had problem with connect Application B with SQL Server Docker cointainer. I don't know why all the time it tried to connect to database by Guest when                   Connection String was set for 'sa'.


Instruction how to run applications.

1. First of all you need to create Microsoft SQL Server LocalDb
2. Run application B (App should create new database and Messages table).
3. If you have problem to create databas, open command-prompt and enter:
      - dotnet ef migrations InitialCreate
      - dotnet ef database upadte
4. Run application A.
