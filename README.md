# AppsCommunication
## Note before merge: If you check project before finish, please switch to branch `solutionB`.

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
