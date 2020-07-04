/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { GatewayApiConfiguration as __Configuration } from '../gateway-api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { AppFeatures } from '../models/app-features';
@Injectable({
  providedIn: 'root',
})
class FeaturesService extends __BaseService {
  static readonly GetFeaturesPath = '/api/Features/features';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @return Success
   */
  GetFeaturesResponse(): __Observable<__StrictHttpResponse<AppFeatures>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Features/features`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<AppFeatures>;
      })
    );
  }
  /**
   * @return Success
   */
  GetFeatures(): __Observable<AppFeatures> {
    return this.GetFeaturesResponse().pipe(
      __map(_r => _r.body as AppFeatures)
    );
  }
}

module FeaturesService {
}

export { FeaturesService }
