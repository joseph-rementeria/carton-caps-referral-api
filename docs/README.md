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