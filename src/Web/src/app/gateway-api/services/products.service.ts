/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { GatewayApiConfiguration as __Configuration } from '../gateway-api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { Product } from '../models/product';
import { IdResult } from '../models/id-result';
@Injectable({
  providedIn: 'root',
})
class ProductsService extends __BaseService {
  static readonly GetProductsByIdsPath = '/api/Products';
  static readonly CreateProductPath = '/api/Products';
  static readonly GetProductByIdPath = '/api/Products/{id}';
  static readonly UpdateProductPath = '/api/Products/{id}';
  static readonly DeleteProductPath = '/api/Products/{id}';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `ProductsService.GetProductsByIdsParams` containing the following parameters:
   *
   * - `ids`:
   *
   * @return Success
   */
  GetProductsByIdsResponse(params: ProductsService.GetProductsByIdsParams): __Observable<__StrictHttpResponse<Array<Product>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    (params.ids || []).forEach(val => {if (val != null) __params = __params.append('ids', val.toString())});
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Products`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<Product>>;
      })
    );
  }
  /**
   * @param params The `ProductsService.GetProductsByIdsParams` containing the following parameters:
   *
   * - `ids`:
   *
   * @return Success
   */
  GetProductsByIds(params: ProductsService.GetProductsByIdsParams): __Observable<Array<Product>> {
    return this.GetProductsByIdsResponse(params).pipe(
      __map(_r => _r.body as Array<Product>)
    );
  }

  /**
   * @param params The `ProductsService.CreateProductParams` containing the following parameters:
   *
   * - `product`:
   *
   * @return Success
   */
  CreateProductResponse(params: ProductsService.CreateProductParams): __Observable<__StrictHttpResponse<IdResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.product;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Products`,
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
   * @param params The `ProductsService.CreateProductParams` containing the following parameters:
   *
   * - `product`:
   *
   * @return Success
   */
  CreateProduct(params: ProductsService.CreateProductParams): __Observable<IdResult> {
    return this.CreateProductResponse(params).pipe(
      __map(_r => _r.body as IdResult)
    );
  }

  /**
   * @param params The `ProductsService.GetProductByIdParams` containing the following parameters:
   *
   * - `id`:
   *
   * @return Success
   */
  GetProductByIdResponse(params: ProductsService.GetProductByIdParams): __Observable<__StrictHttpResponse<Product>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Products/${encodeURIComponent(params.id)}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Product>;
      })
    );
  }
  /**
   * @param params The `ProductsService.GetProductByIdParams` containing the following parameters:
   *
   * - `id`:
   *
   * @return Success
   */
  GetProductById(params: ProductsService.GetProductByIdParams): __Observable<Product> {
    return this.GetProductByIdResponse(params).pipe(
      __map(_r => _r.body as Product)
    );
  }

  /**
   * @param params The `ProductsService.UpdateProductParams` containing the following parameters:
   *
   * - `product`:
   *
   * - `id`:
   */
  UpdateProductResponse(params: ProductsService.UpdateProductParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.product;

    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/Products/${encodeURIComponent(params.id)}`,
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
   * @param params The `ProductsService.UpdateProductParams` containing the following parameters:
   *
   * - `product`:
   *
   * - `id`:
   */
  UpdateProduct(params: ProductsService.UpdateProductParams): __Observable<null> {
    return this.UpdateProductResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `ProductsService.DeleteProductParams` containing the following parameters:
   *
   * - `id`:
   */
  DeleteProductResponse(params: ProductsService.DeleteProductParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/Products/${encodeURIComponent(params.id)}`,
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
   * @param params The `ProductsService.DeleteProductParams` containing the following parameters:
   *
   * - `id`:
   */
  DeleteProduct(params: ProductsService.DeleteProductParams): __Observable<null> {
    return this.DeleteProductResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }
}

module ProductsService {

  /**
   * Parameters for GetProductsByIds
   */
  export interface GetProductsByIdsParams {
    ids?: Array<number>;
  }

  /**
   * Parameters for CreateProduct
   */
  export interface CreateProductParams {
    product: Product;
  }

  /**
   * Parameters for GetProductById
   */
  export interface GetProductByIdParams {
    id: number;
  }

  /**
   * Parameters for UpdateProduct
   */
  export interface UpdateProductParams {
    product: Product;
    id: number;
  }

  /**
   * Parameters for DeleteProduct
   */
  export interface DeleteProductParams {
    id: number;
  }
}

export { ProductsService }
