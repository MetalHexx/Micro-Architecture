import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, concatMap, mergeMap } from 'rxjs/operators';
import { EMPTY, of } from 'rxjs';

import * as CustomerOrderActions from './customer-order.actions';
import { CustomersService, CustomerOrdersService } from 'src/app/gateway-api/services';



@Injectable()
export class CustomerOrderEffects {  

  loadCustomers$ = createEffect(() => {
    return this.actions$.pipe( 

      ofType(CustomerOrderActions.loadCustomers),
      concatMap(() =>
        this.customersService.GetCustomers().pipe(
          map(data => CustomerOrderActions.loadCustomersSuccess({data})),
          catchError(error => of(CustomerOrderActions.loadCustomersFailure())))
      )
    );
  });

  loadOrders$ = createEffect(() => {
    return this.actions$.pipe( 

      ofType(CustomerOrderActions.loadCustomerOrders),
      mergeMap((action) =>
        this.customerOrdersService.GetCustomerOrders({ customerId: action.data }).pipe(
          map(data => CustomerOrderActions.loadCustomerOrdersSuccess({ data })),
          catchError(error => of(CustomerOrderActions.loadCustomersOrdersFailure())))
      )
    );
  });

  constructor(
    private actions$: Actions,
    private customersService: CustomersService,
    private customerOrdersService: CustomerOrdersService
) {
    this.customersService.rootUrl = "http://localhost:5000";
    this.customerOrdersService.rootUrl = "http://localhost:5000";
}
}
