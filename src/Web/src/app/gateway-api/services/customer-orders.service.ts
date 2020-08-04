/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { GatewayApiConfiguration as __Configuration } from '../gateway-api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { CustomerOrdersViewModel } from '../models/customer-orders-view-model';
import { CustomerViewModel } from '../models/customer-view-model';
@Injectable({
  providedIn: 'root',
})
class CustomerOrdersService extends __BaseService {
  static readonly GetCustomerOrdersPath = '/api/CustomerOrders/customers/{customerId}/orders';
  static readonly GetCustomerListPath = '/api/CustomerOrders/customers';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `CustomerOrdersService.GetCustomerOrdersParams` containing the following parameters:
   *
   * - `customerId`:
   *
   * @return Success
   */
  GetCustomerOrdersResponse(params: CustomerOrdersService.GetCustomerOrdersParams): __Observable<__StrictHttpResponse<CustomerOrdersViewModel>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/CustomerOrders/customers/${params.customerId}/orders`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<CustomerOrdersViewModel>;
      })
    );
  }
  /**
   * @param params The `CustomerOrdersService.GetCustomerOrdersParams` containing the following parameters:
   *
   * - `customerId`:
   *
   * @return Success
   */
  GetCustomerOrders(params: CustomerOrdersService.GetCustomerOrdersParams): __Observable<CustomerOrdersViewModel> {
    return this.GetCustomerOrdersResponse(params).pipe(
      __map(_r => _r.body as CustomerOrdersViewModel)
    );
  }

  /**
   * @return Success
   */
  GetCustomerListResponse(): __Observable<__StrictHttpResponse<Array<CustomerViewModel>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/CustomerOrders/customers`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<CustomerViewModel>>;
      })
    );
  }
  /**
   * @return Success
   */
  GetCustomerList(): __Observable<Array<CustomerViewModel>> {
    return this.GetCustomerListResponse().pipe(
      __map(_r => _r.body as Array<CustomerViewModel>)
    );
  }
}

module CustomerOrdersService {

  /**
   * Parameters for GetCustomerOrders
   */
  export interface GetCustomerOrdersParams {
    customerId: number;
  }
}

export { CustomerOrdersService }
