# API Specification: Referral Service Contract

This folder contains the definition of the API using **OpenAPI Specification (OAS)**. This YAML file serves as a contract for code generation, validation, and documentation.

### Specification Details

* **File Name:** `openapi.yaml`
* **Description:** The source of truth. updated through the swagger hub app. it is a backup of the specification for this API. it is recommended to update it through the Swagger hub as it has real time syntax validation and a preview option on the same UI, link given bellow

### Cloud solution for OpenAPI integrated with this repo

Please review the online documentation:
[https://app.swaggerhub.com/apis/notapplicable-afc/carton-caps-referral-api-definition/v1](https://app.swaggerhub.com/apis/notapplicable-afc/carton-caps-referral-api-definition/v1)

### Why `/beta` vs `/api` on servers?
Although there is no really a "beta" specific version endpoint or resource, a beta API is defined to set the future changes to the API that either will be applied to `/api/v1` or an eventual `/api/v2` 

### How to replicate the setup?
If the documentation setup is to be replicated, follow the next instructions:

* Create a public repo in which to save the documentation on GitHub
* Go to [Swagger HUB ](https://app.swaggerhub.com/notapplicable-afc/home)
* Login either to a paid subscription or start a new Free 30 days trial
* In this case I used the SSO with the GitHub account in which this repo resides 
* Click on Desing
* On the left Nav Bar, click on new, either create a new one from scratch or click on Import. I recommend to create a new one
* Fill the form with the required fields, make sure you create a public solution.
* Once the project is created, click on Sync Button on the top of the editor, then click on setup integration, follow the steps that the integration popup gives
* This will create a new branch that will be updated each time the user clicks on Sync, this help us keep track of the changes done to the API and allows the team to review before moving them to main.
#### Is there a free version?
Yes! there is a free option if the free trial is not accessible or not available in the future. You can either install the on-prem solution for Swagger or use the cloud open editor version ([link here](https://editor.swagger.io)).
Either way, changes to the YAML file must be copy from and to the repo manually as there is no direct integration as of now.