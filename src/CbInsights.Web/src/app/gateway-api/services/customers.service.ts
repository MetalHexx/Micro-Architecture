/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { GatewayApiConfiguration as __Configuration } from '../gateway-api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { Customer } from '../models/customer';
import { IdResult } from '../models/id-result';
@Injectable({
  providedIn: 'root',
})
class CustomersService extends __BaseService {
  static readonly GetCustomerPath = '/api/Customers/{id}';
  static readonly UpdateCustomerPath = '/api/Customers/{id}';
  static readonly DeleteCustomerPath = '/api/Customers/{id}';
  static readonly GetCustomersPath = '/api/Customers';
  static readonly CreateCustomerPath = '/api/Customers';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `CustomersService.GetCustomerParams` containing the following parameters:
   *
   * - `id`:
   *
   * @return Success
   */
  GetCustomerResponse(params: CustomersService.GetCustomerParams): __Observable<__StrictHttpResponse<Customer>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Customers/${params.id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Customer>;
      })
    );
  }
  /**
   * @param params The `CustomersService.GetCustomerParams` containing the following parameters:
   *
   * - `id`:
   *
   * @return Success
   */
  GetCustomer(params: CustomersService.GetCustomerParams): __Observable<Customer> {
    return this.GetCustomerResponse(params).pipe(
      __map(_r => _r.body as Customer)
    );
  }

  /**
   * @param params The `CustomersService.UpdateCustomerParams` containing the following parameters:
   *
   * - `id`:
   *
   * - `customer`:
   */
  UpdateCustomerResponse(params: CustomersService.UpdateCustomerParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    __body = params.customer;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/Customers/${params.id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * @param params The `CustomersService.UpdateCustomerParams` containing the following parameters:
   *
   * - `id`:
   *
   * - `customer`:
   */
  UpdateCustomer(params: CustomersService.UpdateCustomerParams): __Observable<null> {
    return this.UpdateCustomerResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `CustomersService.DeleteCustomerParams` containing the following parameters:
   *
   * - `id`:
   *
   * @return Success
   */
  DeleteCustomerResponse(params: CustomersService.DeleteCustomerParams): __Observable<__StrictHttpResponse<Customer>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/Customers/${params.id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Customer>;
      })
    );
  }
  /**
   * @param params The `CustomersService.DeleteCustomerParams` containing the following parameters:
   *
   * - `id`:
   *
   * @return Success
   */
  DeleteCustomer(params: CustomersService.DeleteCustomerParams): __Observable<Customer> {
    return this.DeleteCustomerResponse(params).pipe(
      __map(_r => _r.body as Customer)
    );
  }

  /**
   * @return Success
   */
  GetCustomersResponse(): __Observable<__StrictHttpResponse<Array<Customer>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Customers`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<Customer>>;
      })
    );
  }
  /**
   * @return Success
   */
  GetCustomers(): __Observable<Array<Customer>> {
    return this.GetCustomersResponse().pipe(
      __map(_r => _r.body as Array<Customer>)
    );
  }

  /**
   * @param params The `CustomersService.CreateCustomerParams` containing the following parameters:
   *
   * - `customer`:
   *
   * @return Success
   */
  CreateCustomerResponse(params: CustomersService.CreateCustomerParams): __Observable<__StrictHttpResponse<IdResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.customer;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Customers`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<IdResult>;
      })
    );
  }
  /**
   * @param params The `CustomersService.CreateCustomerParams` containing the following parameters:
   *
   * - `customer`:
   *
   * @return Success
   */
  CreateCustomer(params: CustomersService.CreateCustomerParams): __Observable<IdResult> {
    return this.CreateCustomerResponse(params).pipe(
      __map(_r => _r.body as IdResult)
    );
  }
}

module CustomersService {

  /**
   * Parameters for GetCustomer
   */
  export interface GetCustomerParams {
    id: number;
  }

  /**
   * Parameters for UpdateCustomer
   */
  export interface UpdateCustomerParams {
    id: number;
    customer?: Customer;
  }

  /**
   * Parameters for DeleteCustomer
   */
  export interface DeleteCustomerParams {
    id: number;
  }

  /**
   * Parameters for CreateCustomer
   */
  export interface CreateCustomerParams {
    customer?: Customer;
  }
}

export { CustomersService }
