/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { GatewayApiConfiguration as __Configuration } from '../gateway-api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { Order } from '../models/order';
@Injectable({
  providedIn: 'root',
})
class OrdersService extends __BaseService {
  static readonly GetOrderPath = '/api/Orders/{id}';
  static readonly UpdateOrderPath = '/api/Orders/{id}';
  static readonly DeleteOrderPath = '/api/Orders/{id}';
  static readonly GetOrdersByCustomerIdPath = '/api/customers/{customerId}/orders';
  static readonly CreateOrderPath = '/api/Orders';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `OrdersService.GetOrderParams` containing the following parameters:
   *
   * - `id`:
   *
   * @return Success
   */
  GetOrderResponse(params: OrdersService.GetOrderParams): __Observable<__StrictHttpResponse<Order>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Orders/${params.id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Order>;
      })
    );
  }
  /**
   * @param params The `OrdersService.GetOrderParams` containing the following parameters:
   *
   * - `id`:
   *
   * @return Success
   */
  GetOrder(params: OrdersService.GetOrderParams): __Observable<Order> {
    return this.GetOrderResponse(params).pipe(
      __map(_r => _r.body as Order)
    );
  }

  /**
   * @param params The `OrdersService.UpdateOrderParams` containing the following parameters:
   *
   * - `id`:
   *
   * - `order`:
   */
  UpdateOrderResponse(params: OrdersService.UpdateOrderParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    __body = params.order;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/Orders/${params.id}`,
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
   * @param params The `OrdersService.UpdateOrderParams` containing the following parameters:
   *
   * - `id`:
   *
   * - `order`:
   */
  UpdateOrder(params: OrdersService.UpdateOrderParams): __Observable<null> {
    return this.UpdateOrderResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `OrdersService.DeleteOrderParams` containing the following parameters:
   *
   * - `id`:
   */
  DeleteOrderResponse(params: OrdersService.DeleteOrderParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/Orders/${params.id}`,
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
   * @param params The `OrdersService.DeleteOrderParams` containing the following parameters:
   *
   * - `id`:
   */
  DeleteOrder(params: OrdersService.DeleteOrderParams): __Observable<null> {
    return this.DeleteOrderResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `OrdersService.GetOrdersByCustomerIdParams` containing the following parameters:
   *
   * - `customerId`:
   *
   * @return Success
   */
  GetOrdersByCustomerIdResponse(params: OrdersService.GetOrdersByCustomerIdParams): __Observable<__StrictHttpResponse<Array<Order>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/customers/${params.customerId}/orders`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<Order>>;
      })
    );
  }
  /**
   * @param params The `OrdersService.GetOrdersByCustomerIdParams` containing the following parameters:
   *
   * - `customerId`:
   *
   * @return Success
   */
  GetOrdersByCustomerId(params: OrdersService.GetOrdersByCustomerIdParams): __Observable<Array<Order>> {
    return this.GetOrdersByCustomerIdResponse(params).pipe(
      __map(_r => _r.body as Array<Order>)
    );
  }

  /**
   * @param params The `OrdersService.CreateOrderParams` containing the following parameters:
   *
   * - `order`:
   *
   * @return Success
   */
  CreateOrderResponse(params: OrdersService.CreateOrderParams): __Observable<__StrictHttpResponse<number>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.order;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Orders`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'text'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return (_r as HttpResponse<any>).clone({ body: parseFloat((_r as HttpResponse<any>).body as string) }) as __StrictHttpResponse<number>
      })
    );
  }
  /**
   * @param params The `OrdersService.CreateOrderParams` containing the following parameters:
   *
   * - `order`:
   *
   * @return Success
   */
  CreateOrder(params: OrdersService.CreateOrderParams): __Observable<number> {
    return this.CreateOrderResponse(params).pipe(
      __map(_r => _r.body as number)
    );
  }
}

module OrdersService {

  /**
   * Parameters for GetOrder
   */
  export interface GetOrderParams {
    id: number;
  }

  /**
   * Parameters for UpdateOrder
   */
  export interface UpdateOrderParams {
    id: number;
    order?: Order;
  }

  /**
   * Parameters for DeleteOrder
   */
  export interface DeleteOrderParams {
    id: number;
  }

  /**
   * Parameters for GetOrdersByCustomerId
   */
  export interface GetOrdersByCustomerIdParams {
    customerId: number;
  }

  /**
   * Parameters for CreateOrder
   */
  export interface CreateOrderParams {
    order?: Order;
  }
}

export { OrdersService }
