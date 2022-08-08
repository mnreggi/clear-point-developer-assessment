# Developer Assessment

As you can see, this project only has the backend solution.

- Has been structured for re-usability in some parts of the code.
- Unit and Integration tests have been added.
- Summary of the methods have been added.
- TODO comments added as well for reference to stories.
- InMemory DB has been used.

Things to do in the future:
- Add a Core project to decouple the Controller from the Data layer. This will help us to have all the business logic away from the controller and from the repositories.
- Add Unit and Integration tests to have better coverage.
- Add ViewModels on the Controller. And entities on the Core.
- Add versioning on the API.
- Add a middleware where it can catch any known exception.
- Add custom errors, so we can have a better control on what we want to log and what to show to the consumer.
- Retry policy on the DB if we spot transient errors.
- Define infra configuration.
- Add protected resources. Our endpoints are not secured right now.

For the frontend.
- This can easily be done on different JIRA cases in parallel with the API work. We just need an API Spec where we agree and "sign a contract" between the API and the consumers.
- If the UI want to be on sync with what other user is doing, we can have a firebase that can interact.
- Or if we want to make it more complex, we can have some sort of publish and subscribe from the backend that will publish any update and it will post that on a queue where the UI is listening.
- Handling exceptions on the UI.
- Request to the backend a Token to communicate to the endpoints.

Appreciate the time, and sorry for the delay. If you need me to do something extra, let me know.