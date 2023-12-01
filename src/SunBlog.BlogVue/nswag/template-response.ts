import moment from "moment";

class ErrorInfo {
  message: any;
  init(_data?: any) {
    if (_data) {
      this.message = _data["message"];
    }
  }

  static fromJS(data: any): ErrorInfo {
    data = typeof data === "object" ? data : {};
    let result = new ErrorInfo();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === "object" ? data : {};
    data["message"] = this.message;
    return data;
  }
}
class ZResponseBase {
  statusCode: number;
  error: ErrorInfo;
  unAuthorizedRequest: boolean;
  extras: any;
  success: boolean;

  init(_data?: any) {
    if (_data) {
      this.statusCode = _data["statusCode"];
      this.error = ErrorInfo.fromJS(_data["error"]);
      this.unAuthorizedRequest = _data["unAuthorizedRequest"];
      this.extras = _data["extras"];
      this.success = _data["success"];
    }
  }

  static fromJS(data: any): ZResponseBase {
    data = typeof data === "object" ? data : {};
    let result = new ZResponseBase();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === "object" ? data : {};
    data["statusCode"] = this.statusCode;
    data["error"] = this.error;
    data["unAuthorizedRequest"] = this.unAuthorizedRequest;
    data["extras"] = this.extras;
    data["success"] = this.success;
    return data;
  }
}
export class ZEngineResponse<T = any> extends ZResponseBase {
  result: T;
}
