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
  static readonly UpdateFeaturesPath = '/api/Features/features';
  static readonly CreateFeaturesPath = '/api/Features/features';
  static readonly DeleteFeaturesPath = '/api/Features/features/{id}';

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

  /**
   * @param params The `FeaturesService.UpdateFeaturesParams` containing the following parameters:
   *
   * - `features`:
   *
   * @return Success
   */
  UpdateFeaturesResponse(params: FeaturesService.UpdateFeaturesParams): __Observable<__StrictHttpResponse<AppFeatures>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.features;
    let req = new HttpRequest<any>(
      'PUT',
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
   * @param params The `FeaturesService.UpdateFeaturesParams` containing the following parameters:
   *
   * - `features`:
   *
   * @return Success
   */
  UpdateFeatures(params: FeaturesService.UpdateFeaturesParams): __Observable<AppFeatures> {
    return this.UpdateFeaturesResponse(params).pipe(
      __map(_r => _r.body as AppFeatures)
    );
  }

  /**
   * @param params The `FeaturesService.CreateFeaturesParams` containing the following parameters:
   *
   * - `features`:
   *
   * @return Success
   */
  CreateFeaturesResponse(params: FeaturesService.CreateFeaturesParams): __Observable<__StrictHttpResponse<AppFeatures>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.features;
    let req = new HttpRequest<any>(
      'POST',
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
   * @param params The `FeaturesService.CreateFeaturesParams` containing the following parameters:
   *
   * - `features`:
   *
   * @return Success
   */
  CreateFeatures(params: FeaturesService.CreateFeaturesParams): __Observable<AppFeatures> {
    return this.CreateFeaturesResponse(params).pipe(
      __map(_r => _r.body as AppFeatures)
    );
  }

  /**
   * @param params The `FeaturesService.DeleteFeaturesParams` containing the following parameters:
   *
   * - `id`:
   *
   * @return Success
   */
  DeleteFeaturesResponse(params: FeaturesService.DeleteFeaturesParams): __Observable<__StrictHttpResponse<AppFeatures>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/Features/features/${encodeURIComponent(params.id)}`,
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
   * @param params The `FeaturesService.DeleteFeaturesParams` containing the following parameters:
   *
   * - `id`:
   *
   * @return Success
   */
  DeleteFeatures(params: FeaturesService.DeleteFeaturesParams): __Observable<AppFeatures> {
    return this.DeleteFeaturesResponse(params).pipe(
      __map(_r => _r.body as AppFeatures)
    );
  }
}

module FeaturesService {

  /**
   * Parameters for UpdateFeatures
   */
  export interface UpdateFeaturesParams {
    features?: AppFeatures;
  }

  /**
   * Parameters for CreateFeatures
   */
  export interface CreateFeaturesParams {
    features?: AppFeatures;
  }

  /**
   * Parameters for DeleteFeatures
   */
  export interface DeleteFeaturesParams {
    id: string;
  }
}

export { FeaturesService }
