export class ApiResult {
  statusCode: ApiStatusCode;
  data: any;

  constructor(response: any) {
    Object.assign(this, response);
  }
}

export class ApiErrorResult {
  statusCode: ApiStatusCode;
  message: string;

  constructor(response: any) {
    Object.assign(this, response);
  }
}

export enum ApiStatusCode {
  EndpointOffline = 0,
  Success = 200,
  BadRequest = 400,
  NotFound = 404,
  BlockedRequest = 407,
  ExpirationDateData = 409,
  UniqRowDuplicate = 410,
  CanNotAdd = 418,
  CanNotDelete = 420,
  Error = 500
}
