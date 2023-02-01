# AppsCommunication
## Note before merge: If you check project before finish, please switch to branch `solutionB`.

In the project, we have 2 applications:

 * `App A` which send an encrypted message to the database and key to the App B to decrypt a message.
 * `App B` creates database using method code first, get message from the database, decrypt a messsage and send status with decrypted message to App A.

###Task List

- [x] Application A logic
- [x] Application B logic
- [x] Unit tests
- [x] Some documentations (Swagger UI)
- [ ] Fix issue with N calls to AppB - develop callback for `AppB`.
- [ ] Docker 
